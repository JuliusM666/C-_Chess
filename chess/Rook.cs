using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Rook : Figure
    {
        private bool FirstTurn;
        public Rook(Char color,bool FirstTurn)
        {
            this.FirstTurn = FirstTurn;
            this.Color = color;

        }
        public override void Moves(int x, int y, Figure[,] Board, List<int[]> list, int[] King)
        {
            int[] stepsX = { 1, 0, -1, 0 };
            int[] stepsY = { 0, 1, 0, -1};
            for (int i = 0; i < 4; i++)
            {
                int tempX = x;
                int tempY = y;
                while (true)
                {
                    tempX += stepsX[i];
                    tempY += stepsY[i];
                    if (Validate.Validate_(tempX, tempY) == true)
                    {
                        if (Board[tempX, tempY] != null)
                        {
                            if (Board[tempX, tempY].getColor() != this.Color)
                            {
                                if (Validate.isCheck(x, y, tempX,tempY, this.Color, Board, King) == false)
                                {
                                    list.Add(new int[] { x, y, tempX, tempY });
                                }
                            }
                            break;
                        }
                        
                        else if(Validate.isCheck(x, y, tempX, tempY, this.Color, Board, King) == false)
                        {
                        
                            list.Add(new int[] { x, y, tempX, tempY });
                        }
                       
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
        public override bool CanReach(int x, int y, Figure[,] Board, int[] King)
        {
           
            int stepX = 0;
            int stepY = 0;

            if (x > King[0])
            {
                stepX = -1;
            }
            else if (x < King[0])
            {
                stepX = 1;
            }
            if (y > King[1])
            {
                stepY = -1;
            }
            else if (y < King[1])
            {
                stepY = 1;
            }

           
            if (stepX==0  ||  stepY == 0)
            {
               

                while (true)
                {
                    
                    if(x+stepX==King[0] && y + stepY == King[1])
                    {
                       
                        return true;
                    }
                    x += stepX;
                    y += stepY;
                    
                    if (Board[x, y] != null)
                    {
                        
                        
                        
                        return false;
                    }
                }
            }


                return false;
        }
        public override void ChangeFirstTurn()
        {
            this.FirstTurn = false;
        }
        public override bool getFirstTurn()
        {
            return this.FirstTurn;
        }
        public override Figure Copy()
        {
            return new Rook(this.Color, this.FirstTurn);
        }
    }
}
