using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
     class Game
    {
        private  Figure[,] Board ;
        private List<int[]> AvailableMoves;
        private int[] BKing;
        private int[] WKing;
        private AI com1;
        private bool IsGameOver;

        public Game()
        {
            this.IsGameOver = false;
            this.BKing = new int[]{ 0, 4 };
            this.WKing = new int[]{ 7, 4 };
            this.com1 = new AI('b');


            this.Board = new Figure[,]
            
            {
                {new Rook('b',true),new Knight('b'),new Bishop('b'),new Queen('b'),new King('b',true),new Bishop('b'),new Knight('b'),new Rook('b',true) },
                {new Pawn('b',true), new Pawn('b',true),new Pawn('b',true),new Pawn('b',true),new Pawn('b',true),new Pawn('b',true),new Pawn('b',true),new Pawn('b',true)},
                {null,null,null,null,null,null,null,null },
                {null,null,null,null,null,null,null,null },
                {null,null,null,null,null,null,null,null },
                {null,null,null,null,null,null,null,null },
               
                 {new Pawn('w',true), new Pawn('w',true),new Pawn('w',true),new Pawn('w',true),new Pawn('w',true),new Pawn('w',true),new Pawn('w',true),new Pawn('w',true)},
                {new Rook('w',true),new Knight('w'),new Bishop('w'),new Queen('w'),new King('w',true),new Bishop('w'),new Knight('w'),new Rook('w',true)  }
            };
           
        }
        public void Turn(char color)
        {
            int[] King = { 0, 0 };
            if (color == 'w')
            {
                King = this.WKing;
                
            }
            else if (color == 'b')
            {
                King = this.BKing;
            }
            this.AvailableMoves = Validate.GenerateMoves(color, this.Board, King);
            if (AvailableMoves.Count == 0)
            {
                this.IsGameOver = true;
               
            }
            if (IsGameOver == false)
            {
                if (color == 'b')
                {
                    ComTurn(com1);
                }
               
            }
        }
       
        public void ComTurn(AI com)
        {
            
            int[] move = com.ChooseMove(this.BKing,this.WKing, this.Board);
            if (Board[move[0], move[1]].GetType().ToString() == "chess.Pawn")
            {
                this.Board[move[0], move[1]].ChangeFirstTurn();
                
            }
            else if(Board[move[0], move[1]].GetType().ToString() == "chess.King")
            {
                if (move[1] + 2 == move[3])
                {
                    this.Board[0, 7].ChangeFirstTurn();
                    this.Board[0, 5] = this.Board[0, 7];
                    this.Board[0, 7] = null;
                }
                else if (move[1] - 2 == move[3])
                {
                    this.Board[0, 0].ChangeFirstTurn();
                    this.Board[0, 3] = this.Board[0, 0];
                    this.Board[0, 0] = null;
                }
               
                this.Board[move[0], move[1]].ChangeFirstTurn();
                if (com.getColor() == 'b')
                {
                   
                    this.BKing =new int[]{ move[2],move[3]};
                }
                else if(com.getColor() == 'w')
                {
                    this.WKing = new int[] { move[2], move[3] };

                }
                
            }
            else if (Board[move[0], move[1]].GetType().ToString() == "chess.Rook")
            {

                this.Board[move[0], move[1]].ChangeFirstTurn();
            }

            this.Board[move[2], move[3]] = this.Board[move[0], move[1]];
            this.Board[move[0], move[1]] = null;
            if(move[2]==7 && this.Board[move[2], move[3]].GetType().ToString() == "chess.Pawn")
            {
                this.Board[move[2], move[3]] = new Queen('b');
            }
            
            Turn('w');
        }
        public int[]  PlayerTurn(int x,int y,int Nx,int Ny)
        {

            bool tmp = false;
            foreach(int [] move in this.AvailableMoves)
            {
                if(move[0]==x && move[1] == y)
                {
                    tmp = true;
                    if(move[2]==Nx && move[3] == Ny)
                    {
                        bool isPromotion = false;
                        if (Board[move[0], move[1]].GetType().ToString() == "chess.Pawn")
                        {
                            this.Board[move[0], move[1]].ChangeFirstTurn();
                            if (move[2] == 0)
                            {
                                isPromotion = true;

                            }
                        }
                        else if (Board[move[0], move[1]].GetType().ToString() == "chess.King")
                        {
                           
                            if (move[1] + 2 == move[3])
                            {
                                this.Board[7, 7].ChangeFirstTurn();
                                this.Board[7,5] = this.Board[7,7];
                                this.Board[7,7] = null;
                            }
                            else if(move[1] - 2 == move[3])
                            {
                                this.Board[7, 0].ChangeFirstTurn();
                                this.Board[7, 3] = this.Board[7, 0];
                                this.Board[7, 0] = null;
                            }
                            this.Board[move[0], move[1]].ChangeFirstTurn();
                            this.WKing = new int[] { move[2], move[3] };
                            
                        }
                        else if(Board[move[0], move[1]].GetType().ToString() == "chess.Rook")
                        {

                            this.Board[move[0], move[1]].ChangeFirstTurn();
                        }
                        this.Board[move[2], move[3]] = this.Board[move[0], move[1]];
                        this.Board[move[0], move[1]] = null;
                        this.AvailableMoves = new List<int[]>();
                        if (isPromotion == false)
                        {
                           Turn('b');
                           
                        }
                        else
                        {
                            
                            return new int[] { move[2], move[3] };
                            
                           
                        }

                        
                    }
                }
                else
                {
                    if (tmp == true)
                    {
                        break;
                    }
                }
               
            }
            return new int[] {0};

        }
        public Figure[,] getBoard()
        {
            
            return this.Board;
        }
        public bool getGameOver()
        {
            return this.IsGameOver;
        }
        public void Promote(int x, int y, int newPiece,char color)
        {
            if (newPiece == 1)
            {
                this.Board[x, y] = new Knight(color);
            }
            else if (newPiece == 2)
            {
                this.Board[x, y] = new Queen(color);
            }
            Turn('b');
        }

        
    }
}
