using System;
using System.Collections.Generic;
using System.Text;

namespace chess
{
    class Queen : Figure
    {
        public Queen(Char color)
        {
            this.Color = color;
        }
        public override void Moves(int x, int y, Figure[,] Board, List<int[]> list, int[] King)
        {
            Rook rook = new Rook(this.Color,false);
            Bishop bishop = new Bishop(this.Color);
            rook.Moves(x, y, Board, list, King);
            bishop.Moves(x, y, Board, list, King);
        }
        public override bool CanReach(int x, int y, Figure[,] Board, int[] King)
        {
            Rook rook = new Rook(this.Color,false);
            if (rook.CanReach(x, y, Board, King) == true)
            {
                return true;
            }
            Bishop bishop = new Bishop(this.Color);
            if (bishop.CanReach(x, y, Board, King) == true)
            {
                return true;
            }
            return false;
        }
        public override Figure Copy()
        {
            return new Queen(this.Color);
        }
    }
}
