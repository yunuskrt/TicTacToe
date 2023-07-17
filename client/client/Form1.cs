using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {
        char playingChar = '-';
        int win = 0;
        int loss = 0;
        int draw = 0;
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
        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        bool firstTimeConnecting = true;
        
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            // initialize socket
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // get fields
            string IP = textBoxIP.Text;
            string username = textBoxUsername.Text;

            if (IP == "" || username == "")
            { // invalid input
                logs.AppendText("Could not connect to the server !\n");
            }
            else
            { // check port field
                int portNum;
                if (Int32.TryParse(textBoxPort.Text, out portNum))
                {
                    try
                    {
                        clientSocket.Connect(IP, portNum);
                        // get usernames from server and check if username exists or not
                        connected = true;
                        string sendMessage = "Operation:Connection - Username:" + username;
                        Byte[] buffer = Encoding.Default.GetBytes(sendMessage);
                        clientSocket.Send(buffer);


                        // start the theread to receive messages from server.
                        Thread receiveThread = new Thread(Receive);
                        receiveThread.Start();
                    }
                    catch
                    {
                        // an error occured
                        logs.AppendText("Could not connect to the server!\n");
                    }
                }
                else
                {
                    // port field is not integer
                    logs.AppendText("Invalid port field!\n");
                }
            }
        }

        private void Receive()
        {
            // thread function to handle incoming messages from server
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[128];
                    clientSocket.Receive(buffer); // receive the byte array from server

                    string serverMessage = Encoding.Default.GetString(buffer);
                    serverMessage = serverMessage.Substring(0, serverMessage.IndexOf("\0"));

                    if(serverMessage.StartsWith("You are the viewer."))
                    {
                        string prefix = "You are the viewer.";

                        int startIndex = serverMessage.IndexOf(prefix) + prefix.Length;
                        string result = serverMessage.Substring(startIndex);

                        Game.Clear();
                        Game.AppendText(result);
                    }
                    else if (serverMessage.Count(c => c == '|') == 6)
                    {
                        // game sent by server

                        // display the game in game richtextbox
                        Game.Clear();
                        Game.AppendText(serverMessage);

                        // enable move button
                        buttonMove.Enabled = true;
                        textBoxMove.Enabled = true;
                    }
                    else
                    {
                        // don't log game screen in logs part.
                        logs.AppendText("Server: " + serverMessage + "\n");
                    }

                    if (serverMessage.StartsWith("Could not connect to server")){
                        // close the connection in case of limit excess or username check
                        //clientSocket.Close();
                        connected = false;                   
                    }
                    else if (serverMessage == "Connected to server successfully.")
                    {
                        buttonConnect.Enabled = false;

                        buttonDisconnect.Enabled = true;
                        buttonJoin.Enabled = true;
                        //buttonLeave.Enabled = true;
                    }
                    else if (serverMessage == "Disconnected from server.")
                    {
                        // clientSocket.Close();
                        connected = false;

                        buttonDisconnect.Enabled = false;
                        buttonJoin.Enabled = false;
                        buttonLeave.Enabled = false;
                        buttonMove.Enabled = false;
                        textBoxMove.Enabled = false;
                        buttonConnect.Enabled = true;
                    }
                    else if (serverMessage == "The player disconnected. Your turn.")
                    {
                        buttonMove.Enabled = true;
                        textBoxMove.Enabled = true;
                    
                    }
                    /*
                else if (serverMessage.Count(c => c == '|') == 6)
                {
                    // game sent by server
                        
                    // display the game in game richtextbox
                    Game.Clear();
                    Game.AppendText(serverMessage);

                    // enable move button
                    buttonMove.Enabled = true;
                    textBoxMove.Enabled = true;
                }
                     */
                    else if (serverMessage.StartsWith("Other player's turn"))
                    {
                        buttonMove.Enabled = false;
                        textBoxMove.Enabled = false;
                        // update the game with current move
                        string gameBoard = Game.Text;
                        char move = serverMessage[serverMessage.Length - 2];
                        char[] gameArray = gameBoard.ToCharArray();
                        gameArray[moveIndexes[move]] = playingChar;
                        gameBoard = new string(gameArray);
                        Game.Clear();
                        Game.AppendText(gameBoard);
                    }
                    else if (serverMessage.StartsWith("You are assigned to"))
                    {
                        // assign X or O to playingChar
                        playingChar = serverMessage[serverMessage.Length - 2];
                    }
                    else if (serverMessage.StartsWith("Game finished."))
                    {
                        buttonJoin.Enabled = true;
                        buttonMove.Enabled = false;
                        textBoxMove.Enabled = false;
                        char result = serverMessage[serverMessage.Length - 2];
                        if (result == 'X')
                        {
                            if (playingChar == 'X')
                            {
                                win += 1;
                                labelWin.Text = "Win: " + win.ToString();
                            }
                            else
                            {
                                loss += 1;
                                labelLoss.Text = "Loss: " + loss.ToString();
                            }
                        }
                        else if (result == 'O')
                        {
                            if (playingChar == 'X')
                            {
                                loss += 1;
                                labelLoss.Text = "Loss: " + loss.ToString();
                            }
                            else
                            {
                                win += 1;
                                labelWin.Text = "Win: " + win.ToString();
                            }
                        }
                        else
                        {
                            draw += 1;
                            labelDraw.Text = "Draw: " + draw.ToString();
                        }
                    }
                    else if (serverMessage == "Other player left the match. You are in the lobby now.")
                    {
                        //buttonLeave.Enabled = false;
                        buttonMove.Enabled = false;
                        textBoxMove.Enabled = false;
                        //buttonJoin.Enabled = true;
                    }
                    else if (serverMessage == "Your opponent left the match. There is no one waiting in the lobby. You win!")
                    {
                        win += 1;
                        labelWin.Text = "Win: " + win.ToString();                        
                    }
                   
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected!\n");
                    }

                    clientSocket.Close();
                    connected = false;
             
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string sendMessage = "Operation:Disconnect - Username:" + username;
            Byte[] buffer = Encoding.Default.GetBytes(sendMessage);
            clientSocket.Send(buffer);
        }

        private void buttonJoin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string sendMessage = "Operation:Join - Username:" + username;
            Byte[] buffer = Encoding.Default.GetBytes(sendMessage);
            clientSocket.Send(buffer);
            buttonJoin.Enabled = false;
            buttonLeave.Enabled = true;
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            string move = textBoxMove.Text;
            int number;
            // input check of move
            if (int.TryParse(move, out number) && number >= 1 && number <= 9)
            {
                // number between 1 and 9 is entered, send the move to server
                string username = textBoxUsername.Text;
                string sendMessage = "Operation:Move - Username:" + username + move;
                Byte[] buffer = Encoding.Default.GetBytes(sendMessage);
                clientSocket.Send(buffer);
            }
            else
            {
                // input not between 1 and 9
                logs.AppendText("You should enter a number between 1 and 9 for the move.\n");
            }
            
        }

        private void buttonLeave_Click(object sender, EventArgs e)
        {
            buttonLeave.Enabled = false;
            buttonMove.Enabled = false;
            textBoxMove.Enabled = false;
            buttonJoin.Enabled = true;

            string username = textBoxUsername.Text;
            string sendMessage = "Operation:Leave - Username:" + username;
            Byte[] buffer = Encoding.Default.GetBytes(sendMessage);
            clientSocket.Send(buffer);           
        }
    }
}
