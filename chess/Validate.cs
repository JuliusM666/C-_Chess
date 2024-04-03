using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Validate
    {
        public static bool Validate_(int x, int y) {

            if(x>7 || x<0 || y>7 || y < 0)
            {
                return false;
            }
            return true;
        }
        public static bool isCheck(int x,int y,int Nx,int Ny,char color,Figure[,] Board,int[] King)
        {
           
            Figure[,] tmp = Board.Clone() as Figure[,];
           
            tmp[Nx, Ny] = tmp[x, y];
            
            tmp[x, y] = null;
            
            for (int i=0;i<8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if (tmp[i, j] != null)
                    {
                        if (tmp[i, j].getColor() != color)
                        {
                            if (tmp[i, j].CanReach(i, j,tmp, King) == true)
                            {
                                
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public static List<int[]> GenerateMoves(char color, Figure[,] board,int[] King)
        {
            List<int[]> tmp = new List<int[]>();

            


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null)
                    {
                        if (board[i, j].getColor() == color)
                        {

                            board[i, j].Moves(i, j, board, tmp, King);
                        }
                    }
                }
            }
            return tmp;




        }


    }
}
