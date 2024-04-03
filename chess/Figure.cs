using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    abstract class Figure
    {

        protected Char Color;
        public Figure()
        {


        }
        public char getColor()
        {
            return this.Color;
        }
        public virtual void Moves(int x, int y, Figure[,] Board, List<int[]> list, int[] King)
        {
            
        }
        public virtual bool CanReach(int x, int y, Figure[,] Board, int[] King)
        {

            return false;
        }
        public virtual void ChangeFirstTurn()
        {

        }
        public virtual bool getFirstTurn()
        {
            return true;
        }
        public virtual Figure Copy()
        {
            return null;
        }
    }
}
