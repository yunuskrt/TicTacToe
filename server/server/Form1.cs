using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form1 : Form
    {
        Socket socketPlayerX;
        Socket socketPlayerO;
        String usernameX;
        String usernameO;
        char user; // O or X
        bool matchStarted = false;
        string game = "1|2|3\n4|5|6\n7|8|9\n";
        Dictionary<char, int> moveIndexes = new Dictionary<char, int>
        {
            {'1', 0},
            {'2', 2},
            {'3', 4},
            {'4', 6},
            {'5', 8},
            {'6', 10},
            {'7', 12},
            {'8', 14},
            {'9', 16}
        };
        // initialize socket
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<String> usernamesInServer = new List<String>();
        List<String> usernamesInLobby = new List<String>();

        bool terminating = false;
        bool listening = false;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void buttonListen_Click(object sender, EventArgs e)
        {
            int serverPort;
            if (Int32.TryParse(textBoxPort.Text, out serverPort))
            {
                // create and bind the endpoint so that clients connect
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(5);

                listening = true;
                buttonListen.Enabled = false;

                // connect the client socket
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Check the port number!\n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {                  
                    Socket newClient = serverSocket.Accept();
                    // add socket to keep track of clients
                    clientSockets.Add(newClient);
                    logs.AppendText("A client is connected.\n");

                    // get messages from clients
                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start();

                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("Socket stopped working!\n");
                    }
                }

            }
        }

        private void Receive(Socket thisClient)
        {
            bool connected = true;
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    thisClient.Receive(buffer);

                    string clientMessage = Encoding.Default.GetString(buffer);
                    clientMessage = clientMessage.Substring(0, clientMessage.IndexOf("\0"));
                    logs.AppendText("Client: " + clientMessage + "\n");

                    //handle client messages

                    int operationIndex = clientMessage.IndexOf("Operation");           
                    // Find the index of the colon (:) after "Operation"
                    int colonIndex = clientMessage.IndexOf("Operation:") + "Operation:".Length;
                    // Find the index of the next space or hyphen (-) after the colon
                    int nextSpaceIndex = clientMessage.IndexOfAny(new char[] { ' ', '-' }, colonIndex);

                    // Extract the substring between the colon and the next space/hyphen
                    string operation = clientMessage.Substring(colonIndex, nextSpaceIndex - colonIndex);
                    
                    if (operation == "Connection") 
                    {
                        // check the username
                        int usernameIndex = clientMessage.IndexOf("Username");
                        string username = clientMessage.Substring(usernameIndex + "Username:".Length).Trim();
                        // search username in usernameList
                        int unameIndex = usernamesInServer.IndexOf(username);
                        // get lastly connectedSocket since socket already connected to send message.
                        Socket lastlyConnectedSocket = clientSockets[clientSockets.Count - 1];
                       
                        // check the limit 
                        if (clientSockets.Count > 4)
                        {        
                            // send appropriate message to client         
                            string message = "Could not connect to server since maximum number of clients connected.";
                            Byte[] message_buffer = Encoding.Default.GetBytes(message);
                            lastlyConnectedSocket.Send(message_buffer);
                            // close the socket
                            //lastlyConnectedSocket.Close();

                            // remove the socket
                            clientSockets.Remove(lastlyConnectedSocket);
                        }
                        // there is limit but user exists
                        else if (unameIndex != -1)
                        {                                           
                            // send appropriate message to client
                            string message = "Could not connect to server since " + username + " exists.";
                            Byte[] message_buffer = Encoding.Default.GetBytes(message);
                            lastlyConnectedSocket.Send(message_buffer);

                            // close the socket
                            //lastlyConnectedSocket.Close();

                            // remove the socket
                            clientSockets.Remove(lastlyConnectedSocket);
                        }
                        // add to username list if username not found 
                        else
                        {
                            usernamesInServer.Add(username);
                            richTextBoxServer.AppendText(username + "\n");
                            // send appropriate message to client
                            string message = "Connected to server successfully.";
                            Byte[] message_buffer = Encoding.Default.GetBytes(message);
                            lastlyConnectedSocket.Send(message_buffer);
                        }

                    }
                    else if (operation == "Disconnect")
                    {
                        // check the username
                        int usernameIndex = clientMessage.IndexOf("Username");
                        string username = clientMessage.Substring(usernameIndex + "Username:".Length).Trim();
                        // search username in usernameList
                        int unameIndex = usernamesInServer.IndexOf(username);
                        // remove socket from socket list
                        Socket socketToRemove = clientSockets[unameIndex];                      
                        // remove username from server list
                        usernamesInServer.Remove(username);
                        richTextBoxServer.Clear();
                        foreach (string name in usernamesInServer)
                        {
                            richTextBoxServer.AppendText(name + "\n");
                        }
                        // remove username from lobby list
                        if (usernamesInLobby.Contains(username))
                        {
                            usernamesInLobby.Remove(username);
                            richTextBoxLobby.Clear();
                            foreach (string name in usernamesInLobby)
                            {
                                richTextBoxLobby.AppendText(name + "\n");
                            }
                        }
                        // log text
                        logs.AppendText("A client has disconnected\n");
                        // send appropriate message
                        string message = "Disconnected from server.";
                        Byte[] message_buffer = Encoding.Default.GetBytes(message);
                        socketToRemove.Send(message_buffer);
                        clientSockets.Remove(socketToRemove);

                        if (username == usernameX)
                        {
                            if (usernamesInLobby.Count > 0)
                            {
                                string userNameToAdd = usernamesInLobby[0];
                                int index = usernamesInServer.IndexOf(userNameToAdd);
                                socketPlayerX = clientSockets[index];
                                usernameX = userNameToAdd;
                                string messageX = "You are assigned to X.";
                                Byte[] message_bufferX = Encoding.Default.GetBytes(messageX);
                                socketPlayerX.Send(message_bufferX);
                                if (user == 'O')
                                {
                                    string messageDX = "The player disconnected. Your turn.";
                                    Byte[] message_bufferDX = Encoding.Default.GetBytes(messageDX);
                                    socketPlayerX.Send(message_bufferDX);
                                }
                                usernamesInLobby.Remove(userNameToAdd);
                                richTextBoxLobby.Clear();
                                foreach (string name in usernamesInLobby)
                                {
                                    richTextBoxLobby.AppendText(name + "\n");
                                }
                            }
                            else
                            {
                                string disconnectMsg = "Your opponent left the match. There is no one waiting in the lobby. You win!";
                                Byte[] message_bufferdisc = Encoding.Default.GetBytes(disconnectMsg);
                                socketPlayerO.Send(message_bufferdisc);
                            }
                        
                        }

                        if (username == usernameO)
                        {
                            if (usernamesInLobby.Count > 0)
                            {
                                string userNameToAdd = usernamesInLobby[0];
                                int index = usernamesInServer.IndexOf(userNameToAdd);
                                socketPlayerO = clientSockets[index];
                                usernameO = userNameToAdd;
                                string messageO = "You are assigned to O.";
                                Byte[] message_bufferO = Encoding.Default.GetBytes(messageO);
                                socketPlayerO.Send(message_bufferO);
                                if (user == 'X')
                                {
                                    string messageDO = "The player disconnected. Your turn.";
                                    Byte[] message_bufferDO = Encoding.Default.GetBytes(messageDO);
                                    socketPlayerO.Send(message_bufferDO);
                                }
                                usernamesInLobby.Remove(userNameToAdd);
                                richTextBoxLobby.Clear();
                                foreach (string name in usernamesInLobby)
                                {
                                    richTextBoxLobby.AppendText(name + "\n");
                                }
                            }
                            else
                            { 
                                                          
                            
                                string disconnectMsg = "Your opponent left the match. There is no one waiting in the lobby. You win!";
                                Byte[] message_bufferdisc = Encoding.Default.GetBytes(disconnectMsg);
                                socketPlayerX.Send(message_bufferdisc);
                          
                            }
                        
                        }

                    }
                    else if (operation == "Join")
                    {
                        // check the username
                        int usernameIndex = clientMessage.IndexOf("Username");
                        string username = clientMessage.Substring(usernameIndex + "Username:".Length).Trim();
                        // search username in usernameList
                        int unameIndex = usernamesInServer.IndexOf(username);
                        // add user to lobby list
                        usernamesInLobby.Add(username);

                        if (!matchStarted && usernamesInLobby.Count >= 2) // there is no match playing and 2 players waiting to play
                        {
                            // assign usernames
                            usernameX = usernamesInLobby[0];
                            usernameO = usernamesInLobby[1];
                            int indexX = usernamesInServer.IndexOf(usernameX);
                            int indexO = usernamesInServer.IndexOf(usernameO);

                            // remove users since they will play
                            usernamesInLobby.Remove(usernameX);
                            usernamesInLobby.Remove(usernameO);
                            richTextBoxLobby.Clear();
                            foreach (string name in usernamesInLobby)
                            {
                                richTextBoxLobby.AppendText(name + "\n");
                            }

                            // assign sockets
                            socketPlayerX = clientSockets[indexX];
                            socketPlayerO = clientSockets[indexO];

                            // start the game by sending the game to player X
                            string messageX = "You are assigned to X.";
                            string messageO = "You are assigned to O.";
                            Byte[] messageX_buffer = Encoding.Default.GetBytes(messageX);
                            Byte[] messageO_buffer = Encoding.Default.GetBytes(messageO);

                            Byte[] message_buffer = Encoding.Default.GetBytes(game);
                            int index = 0;
                            foreach(string name in usernamesInServer)
                            {

                                if (name != usernameX && name != usernameO)
                                {
                                    Socket toSend = clientSockets[index];
                                    string messageV = "You are the viewer." + game;
                                    Byte[] messageV_buffer = Encoding.Default.GetBytes(messageV);
                                    toSend.Send(messageV_buffer);
                                   
                                }
                                index++;

                            }
                            socketPlayerX.Send(messageX_buffer);
                            socketPlayerO.Send(messageO_buffer);
                            socketPlayerX.Send(message_buffer);
                            
                            matchStarted = true;
                        }
                        else
                        {
                            // add player to lobby                        
                            richTextBoxLobby.AppendText(username + "\n");
                        }
                    }
                    else if (operation == "Move")
                    {
                        // extract the username and move;
                        int usernameIndex = clientMessage.IndexOf("Username");
                        string usernameAndMove = clientMessage.Substring(usernameIndex + "Username:".Length).Trim();
                        string username = usernameAndMove.Substring(0, usernameAndMove.Length - 1);
                        char move = usernameAndMove[usernameAndMove.Length - 1];

                        // find the socket to send message
                        Socket playerSocket;
                        Socket otherPlayerSocket;
                       


                        

                        if (username == usernameX)
                        {
                            playerSocket = socketPlayerX;
                            otherPlayerSocket = socketPlayerO;
                            user = 'X';                                                                                
                        }
                        else
                        {
                            playerSocket = socketPlayerO;
                            otherPlayerSocket = socketPlayerX;
                            user = 'O';                         
                        }

                        // check the move
                        int movePlaceIndex = moveIndexes[move];

                        if (game[movePlaceIndex] == 'X' || game[movePlaceIndex] == 'O')
                        {
                            // place is already filled, send appropriate message
                            string message = "You cannot play this move,  the place is already filled.";
                            Byte[] message_buffer = Encoding.Default.GetBytes(message);
                            playerSocket.Send(message_buffer);




                        }
                        else
                        {
                            // place is not filled, move is legal

                            //replace the game;
                            char[] gameArray = game.ToCharArray();
                            gameArray[movePlaceIndex] = user;
                            game = new string(gameArray);                            

                            // control the game
                            char m1 = game[0];
                            char m2 = game[2];
                            char m3 = game[4];
                            char m4 = game[6];
                            char m5 = game[8];
                            char m6 = game[10];
                            char m7 = game[12];
                            char m8 = game[14];
                            char m9 = game[16];
                            char winningChar = '-';
                             // 1-2-3
                                // 4-5-6
                                // 7-8-9
                                // 1-5-9
                                // 3-5-7
                                // 1-4-7
                                // 2-5-8
                                // 3-6-9
                            if (m1 == m2 && m1== m3 && m2 == m3){
                                winningChar = m2;
                                matchStarted = false;
                                logs.AppendText("Win by Player");  
                            }
                            else if (m4 == m5 && m4 == m6 && m5 == m6)
                            {
                                winningChar = m5;
                                matchStarted = false;
                                logs.AppendText("Win by Player");
                            }
                            else if (m7 == m8 && m7 == m9 && m8 == m9)
                            {
                                winningChar = m8;
                                matchStarted = false;
                                logs.AppendText("Win by Player");
                            }
                            else if (m1 == m5 && m1 == m9 && m5 == m9)
                            {
                                winningChar = m5;
                                matchStarted = false;
                                logs.AppendText("Win by Player");
                            }
                            else if (m3 == m5 && m3 == m7 && m5 == m7)
                            {
                                winningChar = m5;
                                matchStarted = false;
                                logs.AppendText("Win by Player");
                            }
                            else if (m1 == m4 && m1 == m7 && m4 == m7)
                            {
                                winningChar = m4;
                                matchStarted = false;
                                logs.AppendText("Win by Player");
                            }
                            else if (m2 == m5 && m2 == m8 && m5 == m8)
                            {
                                winningChar = m5;
                                matchStarted = false;
                                logs.AppendText("Win by Player");
                            }
                            else if (m3 == m6 && m3 == m9 && m6 == m9)
                            {
                                winningChar = m6;
                                matchStarted = false;
                                logs.AppendText("Win by Player");
                            }
                            else if ((m1 == 'X' || m1 == 'O') && (m2 == 'X' || m2 == 'O') && (m3 == 'X' || m3 == 'O') && (m4 == 'X' || m4 == 'O') && (m5 == 'X' || m5 == 'O') && (m6 == 'X' || m6 == 'O') && (m7 == 'X' || m7 == 'O') && (m8 == 'X' || m8 == 'O') && (m9 == 'X' || m9 == 'O'))
                            {                                
                                logs.AppendText("Draw between two players");
                                matchStarted = false;
                            }
                            string messageTurn = "Other player's turn, You played " + move.ToString() + ".";
                            Byte[] messageTurn_buffer = Encoding.Default.GetBytes(messageTurn);
                            playerSocket.Send(messageTurn_buffer);
                            if (!matchStarted)
                            {
                                // match is finished
                                // send players appropriate messages
                                if (winningChar == 'X') // X wins
                                {
                                    string message = "Game finished. Win by X.";
                                    Byte[] message_buffer = Encoding.Default.GetBytes(message);
                                    socketPlayerX.Send(message_buffer);
                                    socketPlayerO.Send(message_buffer);
                                }
                                else if (winningChar == 'O') // O wins
                                {
                                    string message = "Game finished. Win by O.";
                                    Byte[] message_buffer = Encoding.Default.GetBytes(message);
                                    socketPlayerX.Send(message_buffer);
                                    socketPlayerO.Send(message_buffer);
                                }
                                else // Draw
                                {
                                    string message = "Game finished. Draw.";
                                    Byte[] message_buffer = Encoding.Default.GetBytes(message);
                                    socketPlayerX.Send(message_buffer);
                                    socketPlayerO.Send(message_buffer);
                                }
                                // return to initial conditions
                                game = "1|2|3\n4|5|6\n7|8|9\n";
                            }
                            else
                            {
                                // game is not finished
                                // send game to other player
                                Byte[] game_buffer = Encoding.Default.GetBytes(game);
                                otherPlayerSocket.Send(game_buffer);
                                int index = 0;
                                foreach (string name in usernamesInServer)
                                {

                                    if (name != usernameX && name != usernameO)
                                    {
                                        Socket toSend = clientSockets[index];
                                        string messageV = "You are the viewer." + game;
                                        Byte[] messageV_buffer = Encoding.Default.GetBytes(messageV);
                                        toSend.Send(messageV_buffer);

                                    }
                                    index++;

                                }

                                // send appropriate message to player
                                //string message = "Other player's turn, You played " + move.ToString() + ".";
                                //Byte[] message_buffer = Encoding.Default.GetBytes(message);
                                //playerSocket.Send(message_buffer);
                            }

                        }
                        //logs.AppendText("Username:" + username + "\n");
                        //logs.AppendText("Move:" + move.ToString() + "\n");
                    }
                    else if (operation == "Leave")
                    {
                        // check the username
                        int usernameIndex = clientMessage.IndexOf("Username");
                        string username = clientMessage.Substring(usernameIndex + "Username:".Length).Trim();
                        // search username in usernameList
                        int unameIndex = usernamesInServer.IndexOf(username);
                        // remove user from lobby if user in lobby
                        if (usernamesInLobby.Contains(username))
                        {
                            usernamesInLobby.Remove(username);
                            richTextBoxLobby.Clear();
                            foreach (string name in usernamesInLobby)
                            {
                                richTextBoxLobby.AppendText(name + "\n");
                            }
                        }
                        else
                        {
                            // user in match
                            // reset to default settings
                            game = "1|2|3\n4|5|6\n7|8|9\n";
                            matchStarted = false;

                            // inform other player
                            if (username == usernameX)
                            {
                                usernamesInLobby.Add(usernameO);
                                string message = "Other player left the match. You are in the lobby now.";
                                Byte[] message_buffer = Encoding.Default.GetBytes(message);
                                socketPlayerO.Send(message_buffer);

                            }
                            else if (username == usernameO)
                            {
                                usernamesInLobby.Add(usernameX);
                                string message = "Other player left the match. You are in the lobby now.";
                                Byte[] message_buffer = Encoding.Default.GetBytes(message);
                                socketPlayerX.Send(message_buffer);
                            }

                            richTextBoxLobby.Clear();
                            foreach (string name in usernamesInLobby)
                            {
                                richTextBoxLobby.AppendText(name + "\n");
                            }
                        }
                    }
                   
                                                       
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("A client has disconnected\n");            
                    }
                    
                    int indexClient = clientSockets.IndexOf(thisClient);
                    string uname = usernamesInServer[indexClient];
                    thisClient.Close();

                    // remove from sockets
                    clientSockets.Remove(thisClient);

                    // remove from usernamesInServer
                    usernamesInServer.RemoveAt(indexClient);
                    richTextBoxServer.Clear();
                    foreach (string name in usernamesInServer)
                    {
                        richTextBoxServer.AppendText(name + "\n");
                    }

                    // remove from usernamesInLobby
                    if (usernamesInLobby.Contains(uname))
                    {
                        usernamesInLobby.Remove(uname);
                        richTextBoxLobby.Clear();
                        foreach (string name in usernamesInLobby)
                        {
                            richTextBoxLobby.AppendText(name + "\n");
                        }
                    }
                    
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_debug_Click(object sender, EventArgs e)
        {
            logs.AppendText("Socket List Length: " + clientSockets.Count + "\n");
            logs.AppendText("Usernames In Server:\n");
            foreach (string name in usernamesInServer)
            {
                logs.AppendText(name + "\n" + "---------------------\n");
            }
            logs.AppendText(game);

            logs.AppendText("Usernames In Lobby:\n");
            foreach (string name in usernamesInLobby)
            {
                logs.AppendText(name + "\n" + "---------------------\n");
            }
                                            
        }
    }
}
