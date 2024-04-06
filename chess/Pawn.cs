using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Pawn:Figure
    {
        private bool FirstTurn;
        
        public Pawn(Char color,bool FirstTurn)
        {
            this.Color = color;
            this.FirstTurn = FirstTurn;
        }
        public override  void Moves(int x, int y, Figure[,] Board, List<int[]> list, int[] King)
        {
            int step = 0;
            if (this.Color == 'b')
            {
                step = 1;
            }
            else if(this.Color == 'w')
            {
                step = -1;
            }
            
            if (Validate.Validate_(x + step, y) == true)
            {
                if (Board[x + step, y] == null)
                {
                    
                    if (Validate.isCheck(x, y, x + step, y , this.Color, Board, King) == false)
                    {
                        
                        list.Add(new int[] { x, y, x + step, y });
                    }
                    
                    if (this.FirstTurn == true)
                    {
                        if (Board[x + step*2, y] == null)
                        {
                            if (Validate.isCheck(x, y, x + step*2, y, this.Color, Board, King) == false)
                            {
                                list.Add(new int[] { x, y, x + step * 2, y });
                            }
                            
                        }
                    }
                }
            }
            if (Validate.Validate_(x + step, y + 1) == true)
            {
                if (Board[x + step, y+1] != null)
                {   
                    if(Board[x + step, y+1].getColor() != this.Color)
                    {
                        if (Validate.isCheck(x, y, x + step, y + 1, this.Color, Board, King) == false)
                        {
                            list.Add(new int[] { x, y, x + step, y + 1 });
                        }
                    }
                    
                }
            }
            if (Validate.Validate_(x + step, y - 1) == true)
            {
                if (Board[x + step, y-1] != null)
                {
                    if (Board[x + step, y-1].getColor() != this.Color)
                    {
                        if (Validate.isCheck(x, y, x + step, y - 1, this.Color, Board, King) == false)
                        {
                            list.Add(new int[] { x, y, x + step, y - 1 });
                        }
                    }

                }
            }
            


        }
        public override bool CanReach(int x, int y, Figure[,] Board, int[] King)
        {
            int step = 0;
            if (this.Color == 'b')
            {
                step = 1;
            }
            else if (this.Color == 'w')
            {
                step = -1;
            }
            if (x + step == King[0] && y + 1 == King[1] || x + step == King[0] && y - 1 == King[1])
            {
                return true;
            }
            return false;
        }
        public override void ChangeFirstTurn() { 
            this.FirstTurn = false;
        }
        public override Figure Copy()
        {
            return new Pawn(this.Color,this.FirstTurn);
        }
    }
}
