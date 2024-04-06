using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class King : Figure
    {
        private bool FirstTurn;
        public King(Char color,bool FirstTurn)
        {
            this.FirstTurn = FirstTurn;
            this.Color = color;
        }
        public override void Moves(int x, int y, Figure[,] Board, List<int[]> list, int[] King)
        {
            int[] stepX = { 1, 1, 1, 0, 0, -1, -1, -1 };
            int[] stepY = { 0, -1, 1, -1, 1, 0, -1, 1 };
            for (int i = 0; i < 8; i++)
            {

                if (Validate.Validate_(x + stepX[i], y + stepY[i]) == true)
                {

                    if (Board[x + stepX[i], y + stepY[i]] != null)
                    {
                        if (Board[x + stepX[i], y + stepY[i]].getColor() != this.Color)
                        {


                            if (Validate.isCheck(x, y, x + stepX[i], y + stepY[i], this.Color, Board, new int[] { x + stepX[i], y + stepY[i] }) == false)
                            {
                                list.Add(new int[] { x, y, x + stepX[i], y + stepY[i] });
                            }
                        }
                    }
                    else
                    {


                        if (Validate.isCheck(x, y, x + stepX[i], y + stepY[i], this.Color, Board, new int[] { x + stepX[i], y + stepY[i] }) == false)
                        {
                            list.Add(new int[] { x, y, x + stepX[i], y + stepY[i] });
                        }
                    }

                }
            }
            Castling(x, y, Board, list, King);
           


        }
        public override bool CanReach(int x, int y, Figure[,] Board, int[] King)
        {
            int[] stepX = { 1, 1, 1, 0, 0, -1, -1, -1 };
            int[] stepY = { 0, -1, 1, -1, 1, 0, -1, 1 };
            bool check = false;
            if(x+1==King[0] || x - 1 == King[0] || y + 1 == King[1] || y -1  == King[1])
            {
                check = true;
            }
            if (check == false)
            {
                return false;
            }
            for (int i = 0; i < 8; i++)
            {
                if(x+stepX[i]==King[0] && y + stepY[i] == King[1])
                {
                    return true;
                }
            }

                return false;
        }
        public override void ChangeFirstTurn()
        {
            
            this.FirstTurn = false;
        }
      
        public void Castling(int x, int y, Figure[,] Board, List<int[]> list, int[] King)
        {
           
            if (this.FirstTurn == true)
            {

                if (Validate.isCheck(x, y, x, y, this.Color, Board, King) == false)
                {
                   
                    if (Board[x, y + 1] == null && Board[x, y + 2] == null)
                    {
                        
                        if (Board[x, 7] != null && Board[x, 7].GetType().ToString() == "chess.Rook" && Board[x, 7].getFirstTurn() == true && Board[x,7].getColor()==this.Color)
                        {
                            
                            if (Validate.isCheck(x, y, x, y + 1, this.Color, Board, new int[] { x, y + 1 }) == false && Validate.isCheck(x, y, x, y + 2, this.Color, Board, new int[] { x, y + 2 }) == false)
                            {
                                list.Add(new int[] { x, y, x, y + 2 });
                            }
                        }
                    }
                    if (Board[x, y -1] == null && Board[x, y -2] == null && Board[x, y - 3]==null)
                    {
                       
                        if (Board[x, 0] != null && Board[x, 0].GetType().ToString() == "chess.Rook" && Board[x, 0].getFirstTurn() == true && Board[x, 0].getColor() == this.Color)
                        {

                            if (Validate.isCheck(x, y, x, y - 1, this.Color, Board, new int[] { x, y - 1 }) == false && Validate.isCheck(x, y, x, y - 2, this.Color, Board, new int[] { x, y - 2 }) == false)
                            {
                                list.Add(new int[] { x, y, x, y - 2 });
                            }
                        }
                    }
                }
            }
        }
        public override Figure Copy()
        {
            return new King(this.Color,this.FirstTurn);
        }


    }
}