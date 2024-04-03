using System;
using System.Collections.Generic;
using System.Text;


namespace chess
{
    class Knight : Figure
    {
        public Knight(Char color)
        {

            this.Color = color;
        }
        public override void Moves(int x, int y, Figure[,] Board, List<int[]> list,int[] King)
        {
            int[] stepsX = { 2, 2, -2, -2, -1, 1, -1, 1 };
            int[] stepsY = { -1, 1, -1, 1, 2, 2, -2, -2 };
            for (int i = 0; i < 8; i++)
            {
                if (Validate.Validate_(x + stepsX[i], y + stepsY[i]) == true)
                {
                    if (Board[x + stepsX[i], y + stepsY[i]] != null)
                    {
                        if (Board[x + stepsX[i], y + stepsY[i]].getColor() != this.Color)
                        {
                            if (Validate.isCheck(x, y, x + stepsX[i], y + stepsY[i], this.Color, Board, King) == false)
                            {
                                list.Add(new int[] { x, y, x + stepsX[i], y + stepsY[i] });
                            }
                            
                        }
                    }
                    else
                    {
                        if (Validate.isCheck(x, y, x + stepsX[i], y + stepsY[i], this.Color, Board, King) == false)
                        {
                            list.Add(new int[] { x, y, x + stepsX[i], y + stepsY[i] });
                        }
                    }

                }
            }
        }
        public override bool CanReach(int x, int y, Figure[,] Board, int[] King)
        {
            int stepX = x - King[0];
            int stepY = y - King[1];
            if (stepX < 0)
            {
                stepX *= -1;
            }
            if (stepY < 0)
            {
                stepY *= -1;
            }
            if(stepY==2 && stepX==1 || stepY == 1 && stepX == 2)
            {
                return true;
            }
            return false;
        }
        public override Figure Copy()
        {
            return new Knight(this.Color);
        }
    }
}