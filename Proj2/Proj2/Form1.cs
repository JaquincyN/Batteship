using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Proj2
{
    public partial class Form1 : Form
    {
        List<int[]> destroyer = new List<int[]>();
        List<int[]> submarine = new List<int[]>();
        List<int[]> cruiser = new List<int[]>();
        List<int[]> hit = new List<int[]>();

        List<List<Button>> buttons = new List<List<Button>>();
        List<List<Button>> enemybuttons = new List<List<Button>>();

        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private String textReceived;

        private bool myTurn = false;
        private bool host = false;
        private bool gameover = true;
        private bool opponentready = false;

        private int myShips = 3;
        private int enemyShips = 3;
        public Form1()
        {
            InitializeComponent();

            List<Button> thisRow;
            foreach (int i in Enumerable.Range(0, 10))
            {
                thisRow = new List<Button>();
                foreach (int j in Enumerable.Range(0, 10))
                {
                    Button b = new Button();

                    b.Size = new System.Drawing.Size(30, 30);
                    b.Location = new Point((i * 30) + 40, (j * 30) + 40);
                    //b.Text = i.ToString() + ", " + j.ToString();
                    b.Click += new EventHandler(anyButton1_Click);
                    this.Controls.Add(b);
                    thisRow.Add(b);
                }
                buttons.Add(thisRow);
            }

            foreach (int i in Enumerable.Range(0, 10))
            {
                thisRow = new List<Button>();
                foreach (int j in Enumerable.Range(0, 10))
                {
                    Button Enemyb = new Button();

                    Enemyb.Size = new System.Drawing.Size(30, 30);
                    Enemyb.Location = new Point((i * 30) + 440, (j * 30) + 40);
                    //Enemyb.Text = i.ToString() + ", " + j.ToString();
                    Enemyb.Click += new EventHandler(anyButton2_Click);
                    this.Controls.Add(Enemyb);
                    thisRow.Add(Enemyb);
                }
                enemybuttons.Add(thisRow);
            }
        }


        private void anyButton1_Click(object sender, EventArgs e)
        {
            if (!gameover)
            {
                var button = (sender as Button);

                int i = 0;
                foreach (List<Button> buttonlist in buttons)
                {
                    if (buttonlist.Contains(button))
                    {
                        break;
                    }
                    i++;
                }
                int[] point = new int[2] { i, buttons[i].IndexOf(button) };
                setShip(point);
            }
            else
            {
                MessageBox.Show("Please Host or Join a Game");
            }
        }

        private void anyButton2_Click(object sender, EventArgs e)
        {
            if (myTurn)
            {
                var button = (sender as Button);

                int i = 0;
                foreach (List<Button> buttonlist in enemybuttons)
                {
                    if (buttonlist.Contains(button))
                    {
                        break;
                    }
                    i++;
                }
                int[] point = new int[] { i, enemybuttons[i].IndexOf(button) };

                string data = i + "," + enemybuttons[i].IndexOf(button);
                dataSender(data);
            }
            else
            {
                if (gameover)
                {
                    MessageBox.Show("Please Host or Join a Game");
                }
                else if (cruiser.Count < 4)
                    MessageBox.Show("Please place all of your ships first.");
                else
                    MessageBox.Show("Wait your turn!");
            }
        }

        private void setShip(int[] point)
        {
           if (destroyer.Count() < 2)
           {
               checkPoint(destroyer,point);
               if (destroyer.Count == 2) label1.Text = "Place your submarine (3 points)";
           }
           else if (submarine.Count() < 3)
           {
               checkPoint(submarine, point);
               if (submarine.Count == 3) label1.Text = "Place your cruiser (4 points)";
           }
           else if (cruiser.Count() < 4)
           {
               checkPoint(cruiser, point);
               if (cruiser.Count == 4)
               {
                   label1.Text = "DONE!";
                   if(!host)
                   {
                       dataSender("done");
                   }
                   if(opponentready)
                   {
                       myTurn = true;
                   }
               }
           }
           else
           {
               
           }
        }
        private void checkPoint(List<int[]> ship, int[]point)
        {

            bool samerow = true;
            bool samecolumn = true;

            foreach(int[] shippoint in ship)
            {
                if (shippoint[0] == point[0])
                {
                    samerow = true;
                }
                else
                {
                    samerow = false;
                    break;
                }
            }

            foreach (int[] shippoint in ship)
            {
                if (shippoint[1] == point[1])
                {
                    samecolumn = true;
                }
                else
                {
                    samecolumn = false;
                    break;
                }
            }

            foreach (int[] shippoint in ship)
            {
                if (Math.Abs(shippoint[0]-point[0])==1 & !ship.Contains(point) & samecolumn & checkHit(point) == "miss")
                {
                    ship.Add(point);
                    buttons[point[0]][point[1]].BackColor = Color.Aqua;
                    break;
                }

                if (Math.Abs(shippoint[1] - point[1]) == 1 & !ship.Contains(point) & samerow & checkHit(point) == "miss")
                {
                    ship.Add(point);
                    buttons[point[0]][point[1]].BackColor = Color.Aqua;
                    break;
                }
            }



            if (ship.Count() == 0 & checkHit(point) == "miss")
            {
                ship.Add(point);
                buttons[point[0]][point[1]].BackColor = Color.Aqua;
            }
        }
        private bool isEqualTo(int[] one, int[] two)
        {
            if (one[0] == two[0])
                if (one[1] == two[1])
                    return true;
            return false;
        }
        private string checkHit(int[] point)
        {
            foreach (int[] shippoint in hit)
            {
                if (isEqualTo(point, shippoint))
                {
                    return "hit";
                }
            }

            string status = "miss";
            foreach(int[] shippoint in destroyer)
            {
                if (isEqualTo(point, shippoint))
                {
                    status = "destroyer";
                }
            }
            foreach (int[] shippoint in submarine)
            {
                if (isEqualTo(point, shippoint))
                {
                    status = "submarine";
                }
            }
            foreach (int[] shippoint in cruiser)
            {
                if (isEqualTo(point, shippoint))
                {
                    status = "cruiser";
                }
            }

            List<int[]> newHit = new List<int[]>();
            List<int[]> checklist = new List<int[]>();
            foreach (int[] hitpoint in hit)
            {
                newHit.Add(hitpoint);
            }
            newHit.Add(point);

            switch(status)
            {
                case "destroyer":
                    foreach (int[] point1 in newHit)
                    {
                        foreach (int[] point2 in destroyer)
                        {
                            if (isEqualTo(point1, point2))
                            {
                                checklist.Add(point2);
                            }
                        }
                    }
                    if (checklist.Count() < 2) status = "hit";
                    break;
                case "submarine":
                    foreach (int[] point1 in newHit)
                    {
                        foreach (int[] point2 in submarine)
                        {
                            if (isEqualTo(point1, point2))
                            {
                                checklist.Add(point2);
                            }
                        }
                    }
                    if (checklist.Count() < 3) status = "hit";
                    break;
                case "cruiser":
                    foreach (int[] point1 in newHit)
                    {
                        foreach (int[] point2 in cruiser)
                        {
                            if (isEqualTo(point1, point2))
                            {
                                checklist.Add(point2);
                            }
                        }
                    }
                    if (checklist.Count() < 4) status = "hit";
                    break;
            }

            return status;
        }
        private void resetGame()
        {
            foreach (List<Button> row in buttons)
            {
                foreach (Button button in row)
                {
                    button.BackColor = SystemColors.Control;
                    button.UseVisualStyleBackColor = true;
                }
            }

            foreach (List<Button> row in enemybuttons)
            {
                foreach (Button button in row)
                {
                    button.BackColor = SystemColors.Control;
                    button.UseVisualStyleBackColor = true;
                }
            }

            destroyer = new List<int[]>();
            submarine = new List<int[]>();
            cruiser = new List<int[]>();
            hit = new List<int[]>();

            myShips = 3;
            enemyShips = 3;

            label1.Text = "Place your destroyer(2 points)";
            myTurn = false;

        }

        private async void dataSender(String toSend)
        {
            await writer.WriteLineAsync(toSend);
        }

        private void Host_Button_Click(object sender, EventArgs e)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 51111);
            listener.Start();
            client = listener.AcceptTcpClient();
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
            writer.AutoFlush = true;

            resetGame();
            host = true;
            gameover = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void Client_Button_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            IPEndPoint IpEnd = new IPEndPoint(IPAddress.Parse(textBox1.Text), 51111);

            try
            {
                client.Connect(IpEnd);
                if (client.Connected)
                {
                    writer = new StreamWriter(client.GetStream());
                    reader = new StreamReader(client.GetStream());
                    writer.AutoFlush = true;

                    resetGame();
                    host = false;
                    gameover = false;

                    backgroundWorker1.RunWorkerAsync();  // start receiving data in the background
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (client.Connected)
            {
                try
                {
                  
                    textReceived = reader.ReadLine();
                    if (myTurn)
                    {
                            string[] textArray = textReceived.Split(',');
                            string hitStatus = textArray[2];
                            switch (hitStatus)
                            {
                                case "miss":
                                    enemybuttons[Int32.Parse(textArray[0])][Int32.Parse(textArray[1])].BackColor = Color.Blue;
                                    break;
                                case "hit":
                                    enemybuttons[Int32.Parse(textArray[0])][Int32.Parse(textArray[1])].BackColor = Color.Red;
                                    break;
                                default:
                                    enemybuttons[Int32.Parse(textArray[0])][Int32.Parse(textArray[1])].BackColor = Color.Green;
                                    MessageBox.Show("You sunk your enemy's " + hitStatus);
                                    enemyShips--;
                                    break;
                            }
                            myTurn = false;
                            if (enemyShips == 0)
                            {
                                //I lose
                                MessageBox.Show("All your enemy's ships are sunk. You Win!");
                                resetGame();
                            }
                    }
                    else
                    {
                        if (host & textReceived == "done" & cruiser.Count == 4)
                        {
                            myTurn = true;
                        }
                        else if(host & textReceived == "done")
                        {
                            opponentready = true;
                        }
                        else
                        {
                            string[] textArray = textReceived.Split(',');

                            int[] point = new int[] { Int32.Parse(textArray[0]), Int32.Parse(textArray[1]) };
                            string hitStatus = checkHit(point);

                            switch (hitStatus)
                            {
                                case "miss":
                                    buttons[Int32.Parse(textArray[0])][Int32.Parse(textArray[1])].BackColor = Color.Blue;
                                    break;
                                case "hit":
                                    hit.Add(point);
                                    buttons[Int32.Parse(textArray[0])][Int32.Parse(textArray[1])].BackColor = Color.Red;
                                    break;
                                default:
                                    hit.Add(point);
                                    myShips--;
                                    MessageBox.Show("Your enemy sunk your " + hitStatus);
                                    buttons[Int32.Parse(textArray[0])][Int32.Parse(textArray[1])].BackColor = Color.Green;
                                    break;
                            }

                            myTurn = true;
                            if (myShips == 0)
                            {
                                //I lose
                                MessageBox.Show("All your ships are sunk. You lose!");
                                resetGame();
                            }
                            string data = textReceived + "," + hitStatus.ToString();
                            dataSender(data);
                        }
                    }
                    //buttons[Int32.Parse(textArray[0])][Int32.Parse(textArray[1])].BackColor = Color.Red;
                    textReceived = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
                
    }

}
