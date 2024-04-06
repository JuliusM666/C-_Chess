using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace chess
{
    class AI
    {
        private char color;
        private Dictionary<string,int[,]> BoardPoints;
        private Dictionary<string, int> PiecePoints;
        private Random rd;
        public AI(char color)
        {
            this.color = color;
            this.BoardPoints = new Dictionary<string, int[,]>();
            this.PiecePoints= new Dictionary<string, int>();
            this.rd = new Random();
            int [,] Pawn=new int[,]
            {
                { 0,  0,  0,  0,  0,  0,  0,  0 },
        { 50, 50, 50, 50, 50, 50, 50, 50},
        { 10, 10, 20, 30, 30, 20, 10, 10},
        { 5,  5, 10, 25, 25, 10,  5,  5},
        { 0,  0,  0, 20, 20,  0,  0,  0},
        { 5, -5,-10,  0,  0,-10, -5,  5},
        { 5, 10, 10,-20,-20, 10, 10,  5},
        { 0,  0,  0,  0,  0,  0,  0,  0}
            };

            int[,] Knight = new int[,] { {-50,-40,-30,-30,-30,-30,-40,-50},
            {-40,-20,  0,  0,  0,  0,-20,-40 },
            { -30,  0, 10, 15, 15, 10,  0,-30,},
            { -30,  5, 15, 20, 20, 15,  5,-30,},
            { -30,  0, 15, 20, 20, 15,  0,-30,},
            {-30,  5, 10, 15, 15, 10,  5,-30, },
             {-40,-20,  0,  5,  5,  0,-20,-40 },
             {-50,-40,-30,-30,-30,-30,-40,-50 }};

            int[,] Bishop =new int[,] {
                {-20,-10,-10,-10,-10,-10,-10,-20},
            { -10,  0,  0,  0,  0,  0,  0,-10 },
            { -10,  0,  5, 10, 10,  5,  0,-10},
            {-10,  5,  5, 10, 10,  5,  5,-10},
            {-10,  0, 10, 10, 10, 10,  0,-10},
            { -10, 10, 10, 10, 10, 10, 10,-10 },
             { -10,  5,  0,  0,  0,  0,  5,-10 },
             { -20,-10,-10,-10,-10,-10,-10,-20 }
            };
            int[,] Rook= new int[,] {
                { 0,  0,  0,  0,  0,  0,  0,  0},
            {  5, 10, 10, 10, 10, 10, 10,  5},
            { -5,  0,  0,  0,  0,  0,  0, -5},
            { -5,  0,  0,  0,  0,  0,  0, -5},
            {-5,  0,  0,  0,  0,  0,  0, -5},
            { -5,  0,  0,  0,  0,  0,  0, -5 },
             { -5,  0,  0,  0,  0,  0,  0, -5 },
             { 0,  0,  0,  5,  5,  0,  0,  0 }
            };
            int[,] Queen= new int[,] {
                { -20,-10,-10, -5, -5,-10,-10,-20},
            { -10,  0,  0,  0,  0,  0,  0,-10 },
            { -10,  0,  5,  5,  5,  5,  0,-10},
            { -5,  0,  5,  5,  5,  5,  0, -5},
            { 0,  0,  5,  5,  5,  5,  0, -5},
            { -10,  5,  5,  5,  5,  5,  0,-10 },
             { -10,  0,  5,  0,  0,  0,  0,-10 },
             { -20,-10,-10, -5, -5,-10,-10,-20 }
            };
            int[,]  King = new int[,] {
                { -30,-40,-40,-50,-50,-40,-40,-30},
            { -30,-40,-40,-50,-50,-40,-40,-30 },
            { -30,-40,-40,-50,-50,-40,-40,-30},
            { -30,-40,-40,-50,-50,-40,-40,-30},
            {-20,-30,-30,-40,-40,-30,-30,-20},
            { -10,-20,-20,-20,-20,-20,-20,-10},
             { 20, 20,  0,  0,  0,  0, 20, 20 },
             { 20, 30, 10,  0,  0, 10, 30, 20 }
            };
            BoardPoints.Add("chess.Pawn", Pawn);
            BoardPoints.Add("chess.Knight", Knight);
            BoardPoints.Add("chess.Bishop",Bishop);
            BoardPoints.Add("chess.Rook", Rook);
            BoardPoints.Add("chess.Queen", Queen);
            BoardPoints.Add("chess.King", King);
            PiecePoints.Add("chess.Pawn", 100);
            PiecePoints.Add("chess.Knight", 320);
            PiecePoints.Add("chess.Bishop", 330);
            PiecePoints.Add("chess.Rook", 500);
            PiecePoints.Add("chess.Queen", 900);
            PiecePoints.Add("chess.King", 20000);


            //https://www.chessprogramming.org/Simplified_Evaluation_Function

        }
        public int[] ChooseMove(int[] BKing, int[] WKing, Figure[,] board)
        {
            
            int[] tmp= MiniMax(-1000000000, 100000000, board, 5, WKing, BKing,true,4);
            return tmp;
           
           
        }
        public int BoardEvaluation(Figure[,] board)
        {
            int result = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] != null)
                    {
                        if (board[i, j].getColor() ==this.color)
                        {
                            int mirrorI = 7 - (2 * i);
                            int mirrorJ = 7 - (2 * j);
                            result += this.PiecePoints[board[i, j].GetType().ToString()];
                            result += this.BoardPoints[board[i, j].GetType().ToString()][i+mirrorI, j+mirrorJ];
                        }
                        else
                        {
                            result -= this.PiecePoints[board[i, j].GetType().ToString()];
                            result -= this.BoardPoints[board[i, j].GetType().ToString()][i,j];
                        }
                    }
                }
            }
            return result;

        }
        public int[] MiniMax(int alpha,int beta,Figure[,] board,int depth, int[] WKing, int[] BKing,bool isMaximizing,int maxDepth)
        {
            if (depth == 0)
            {
                
                return new int[] {0,0,0,0, BoardEvaluation(board) };
            }
            int[]  value= { 0, 0, 0, 0, 0 };
            if (isMaximizing)
            {
                 value[4] =  -1000000000 ;
                List<int[]> moves = Validate.GenerateMoves('b', board, BKing);
                SortMoves(moves, board);
                if (moves.Count == 0)
                {
                    return new int[] { 0, 0, 0, 0, -1000000000 };
                }
                foreach (int[] move in moves)
                {
                    int[] tmpKing = BKing;
                    Figure[,] tmp = copyBoard(board);
                    tmp[move[2], move[3]] = tmp[move[0],move[1]];
                    tmp[move[0], move[1]] = null;

                    if(tmp[move[2], move[3]].GetType().ToString() == "chess.King")
                    {
                        tmp[move[2], move[3]].ChangeFirstTurn();
                        tmpKing =new int[]{ move[2], move[3] };
                        
                        if (move[1]-2==move[3])
                        {
                            tmp[0, 0].ChangeFirstTurn();
                            tmp[0, 2] = tmp[0, 0];
                            tmp[0, 0] = null;

                        }
                        else if (move[1] + 2 == move[3])
                        {
                            tmp[0, 7].ChangeFirstTurn();
                            tmp[0, 5] = tmp[0, 7];
                            tmp[0, 7] = null;
                        }
                    }
                    else if(tmp[move[2], move[3]].GetType().ToString()=="chess.Pawn" )
                    {
                        tmp[move[2], move[3]].ChangeFirstTurn();
                        if (move[2] == 7)
                        {

                            tmp[move[2], move[3]] = new Queen('b');
                        }
                    }
                    else if (tmp[move[2], move[3]].GetType().ToString() == "chess.Rook")
                    {
                        tmp[move[2], move[3]].ChangeFirstTurn();
                    }
                    int[]  tmp1= MiniMax(alpha,beta, tmp, depth - 1, WKing, tmpKing,!isMaximizing,maxDepth);
                   

                    if (value[4]< tmp1[4])
                    {
                        tmp1[0] = move[0];
                        tmp1[1] = move[1];
                        tmp1[2] = move[2];
                        tmp1[3] = move[3];
                        value = tmp1;

                    }

                    else if (value[4] == tmp1[4] && depth==maxDepth)
                    {
                        

                        if (rd.Next(2) == 1)
                        {
                           
                            tmp1[0] = move[0];
                            tmp1[1] = move[1];
                            tmp1[2] = move[2];
                            tmp1[3] = move[3];
                            value = tmp1;
                        }
                    }
                    if (value[4] >= beta)
                    {
                        break;
                    }
                    if (value[4] > alpha)
                    {
                        alpha = value[4];
                    }
                }
               

            }
            else
            {
                value[4] = 100000000;
                List<int[]> moves = Validate.GenerateMoves('w', board, WKing);
                SortMoves(moves, board);
                if (moves.Count == 0)
                {
                    return new int[] { 0, 0, 0, 0, 100000000 };
                }
                foreach (int[] move in moves)
                {
                    int[] tmpKing = WKing;
                    Figure[,] tmp = copyBoard(board);
                    tmp[move[2], move[3]] = tmp[move[0], move[1]];
                    tmp[move[0], move[1]] = null;
                    if (tmp[move[2], move[3]].GetType().ToString() == "chess.King")
                    {
                        tmpKing = new int[] { move[2], move[3] };
                        tmp[move[2], move[3]].ChangeFirstTurn();
                        if (move[1] - 2 == move[3])
                        {
                            tmp[7, 0].ChangeFirstTurn();
                            tmp[7, 2] = tmp[7, 0];
                            tmp[7, 0] = null;

                        }
                        else if (move[1] + 2 == move[3])
                        {
                            tmp[7, 7].ChangeFirstTurn();
                            tmp[7, 5] = tmp[7, 7];
                            tmp[7, 7] = null;
                        }
                    }
                    else if (tmp[move[2], move[3]].GetType().ToString() == "chess.Pawn")
                    {
                        
                            tmp[move[2], move[3]].ChangeFirstTurn();
                            if (move[2] == 0)
                            {

                                tmp[move[2], move[3]] = new Queen('w');
                            }
                        
                        
                    }
                    else if (tmp[move[2], move[3]].GetType().ToString() == "chess.Rook")
                    {
                        tmp[move[2], move[3]].ChangeFirstTurn();
                    }
                    int [] tmp1 = MiniMax(alpha,beta,tmp, depth - 1, tmpKing, BKing, !isMaximizing,maxDepth);

                    if (value[4] > tmp1[4])
                    {
                        tmp1[0] = move[0];
                        tmp1[1] = move[1];
                        tmp1[2] = move[2];
                        tmp1[3] = move[3];
                        value = tmp1;
                    }
                    if (value[4] <= alpha)
                    {
                        break;
                    }
                    if (value[4] < beta)
                    {
                        beta = value[4];
                    }
                }
            }
            return value;

        }
        public char getColor()
        {
            return this.color;
        }
        public void SortMoves(List<int[]> moves, Figure[,] board)
        {
            int[] Ranking = new int[moves.Count / 3];

            for (int i = 0; i < moves.Count; i++)
            {
                if (board[moves[i][2], moves[i][3]] != null)
                {

                    int points = this.PiecePoints[board[moves[i][2], moves[i][3]].GetType().ToString()] - this.PiecePoints[board[moves[i][0], moves[i][1]].GetType().ToString()];

                    for (int j = 0; j < Ranking.Length; j++)
                    {

                        if (points > Ranking[j] || Ranking[j] == null)
                        {
                            int[] tmp = moves[i];
                            moves[i] = moves[j];
                            moves[j] = tmp;
                            Ranking[j] = points;
                        }
                    }
                }
            }

        }
        public Figure[,] copyBoard(Figure[,] Board)
        {
            Figure[,] tmp = new Figure[8, 8];
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Board[i, j] != null)
                    {
                        tmp[i, j] = Board[i, j].Copy();
                    }
                }
            }
            return tmp;

        }

    }
}


