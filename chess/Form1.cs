using System;

using System.Drawing;

using System.Windows.Forms;


namespace chess
{
    public partial class Form1 : Form
    {
        private PictureBox[,] figures;
        
        
        private int CordX;
        private int CordY;
        private Game game;
        private int[] StartLoc;
       

        public Form1()
        {
           
            this.CordX = 70;
            this.CordY = 70;
            this.game = new Game();
           
            this.figures = new PictureBox[8,8];
            

           
            InitializeComponent();
            
           
            ShowBoard();
            game.Turn('w');

        }




        public void ShowBoard()
        {
            
            
            Figure[,] board = game.getBoard();

            int y = this.CordY;
            for (int i = 0; i < 8; i++)
            {
                int x = this.CordX;
                for(int j = 0; j < 8; j++)
                {
                    if (figures[i, j] != null)
                    {
                        figures[i, j].Dispose();
                    }
                    if (board[i, j] != null)
                    {




                        char color = board[i, j].getColor();
                        if (color == 'b')
                        {
                            PictureBox tmp = new PictureBox();

                            this.Controls.Add(tmp);
                            tmp.Parent = pictureBox1;
                            tmp.Image = Image.FromFile(ReturnImage(board[i, j].GetType().ToString(), color));
                            tmp.Size = new System.Drawing.Size(70, 70);
                            tmp.Location = new Point(x, y);



                            tmp.BackColor = Color.Transparent;
                            tmp.BringToFront();

                            figures[i, j] = tmp;

                        }

                        else if (color == 'w')
                    {
                        NewPictureBox tmp = new NewPictureBox();

                        this.Controls.Add(tmp);
                        tmp.Parent = pictureBox1;
                        tmp.Image = Image.FromFile(ReturnImage(board[i, j].GetType().ToString(), color));
                        tmp.Size = new System.Drawing.Size(70, 70);
                        tmp.Location = new Point(x, y);



                        tmp.BackColor = Color.Transparent;
                        tmp.BringToFront();

                        figures[i, j] = tmp;
                        int tmpI = i;
                        int tmpJ = j;
                        figures[i, j].MouseDown += (sender, args) => MouseKlyken(tmpI, tmpJ);
                        figures[i, j].MouseUp += (sender, args) => Move(tmpI, tmpJ);






                    }

                    }
                   
                    x += 70;
                }
                y += 70;
            }
            

           
                    
                
        }
            

        
        public string ReturnImage(string type, char color)
        {
            String path = AppDomain.CurrentDomain.BaseDirectory+ "\\img\\";
            
            if (type == "chess.Pawn")
            {
                if (color == 'w')
                {
                    return path + "PawnW.png";
                }
                else if (color == 'b')
                {
                    return path + "PawnB.png";
                }
            }
            else if (type == "chess.Knight")
            {
                if (color == 'w')
                {
                    return path + "KnightW.png";
                }
                else if (color == 'b')
                {
                    return path + "KnightB.png";
                }
            }
            else if (type == "chess.Bishop")
            {
                if (color == 'w')
                {
                    return path + "BishopW.png";
                }
                else if (color == 'b')
                {
                    return path + "BishopB.png";
                }
            }
            else if (type == "chess.Rook")
            {
                if (color == 'w')
                {
                    return path + "RookW.png";
                }
                else if (color == 'b')
                {
                    return path + "RookB.png";
                }
            }
            else if (type == "chess.Queen")
            {
                if (color == 'w')
                {
                    return path + "QueenW.png";
                }
                else if (color == 'b')
                {
                    return path + "QueenB.png";
                }


            }
            else if (type == "chess.King")
            {
                if (color == 'w')
                {
                    return path + "KingW.png";
                }
                else if (color == 'b')
                {
                    return path + "KingB.png";
                }
            }
            return null;

        }
        public void MouseKlyken(int x, int y)
        {
            this.StartLoc = new int[] { x, y };
            this.figures[x, y].BringToFront();


        }
        public void GameOver()
        {

            ShowBoard();
                var result = MessageBox.Show("Checkmate", "Checkmate",
                                             MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
               }
            
        }
       
        public void Move(int x, int y)
        {
             
            int LocX = (this.PointToClient(Cursor.Position).X - 70)/70;
            int LocY=(this.PointToClient(Cursor.Position).Y - 70) / 70;
           

            if (this.StartLoc != null)
            {

                int[] z=game.PlayerTurn(this.StartLoc[0], this.StartLoc[1], LocY, LocX);
                if (game.getGameOver() == true)
                {
                    GameOver();
                }
                if (z.Length>1)
                {
                    PromotionForm pf = new PromotionForm(this,this.game, z[0], z[1], 'w');
                    pf.Show();
                }
                
            }

            this.StartLoc = null;
           
            ShowBoard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

