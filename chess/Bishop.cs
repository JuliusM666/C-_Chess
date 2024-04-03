using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Bishop : Figure
    {
        public Bishop(Char color)
        {
            this.Color = color;
        }
        public override void Moves(int x, int y, Figure[,] Board, List<int[]> list, int[] King)
        {
            int[] stepsX = { 1, 1, -1, -1 };
            int[] stepsY = { 1, -1, 1, -1 };
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
                                if (Validate.isCheck(x, y, tempX, tempY, this.Color, Board, King) == false)
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
           
            int stepX = x-King[0];
            int stepY = y-King[1];

            int moveX = -1;
            int moveY = -1;
            
            if (stepX < 0)
            {
                stepX *= -1;
                moveX = 1;
            }
            if (stepY < 0)
            {
                stepY *= -1;
                moveY = 1;
            }
            if (stepX == stepY)
            {
               
                while (true)
                {
                    if(x+moveX==King[0] && y + moveY == King[1])
                    {
                        return true;
                    }

                    x += moveX;
                    y += moveY;
                    
                    if (Board[x, y] != null)
                    {
                        return false;
                    }
                }
            }
            
            
            return false;
        }
        public override Figure Copy()
        {
            return new Bishop(this.Color);
        }
    }
}