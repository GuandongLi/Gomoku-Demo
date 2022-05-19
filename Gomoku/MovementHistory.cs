using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;


namespace IFN563_Gomoku
{
    public sealed class MovementHistory //singleton
    {
        private MovementHistory()
        {
        }

        public static MovementHistory MH { get { return Nested.movehistory; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly MovementHistory movehistory = new MovementHistory();
        }

        public int[,] MoveHistory;
        public int[,] ReverseMoveHistory;


        public void CreateMoveSave()
        {
            MoveHistory = new int[Game.Board_Column * Game.Board_Row, 2];
            ReverseMoveHistory = new int[Game.Board_Column * Game.Board_Row, 3];

        }

        public void MoveSave(int count, int x, int y)
        {
            MoveHistory[count, 0] = x;
            MoveHistory[count, 1] = y;

        }
        public void ReverseMoveSave(int count, int x, int y, int TurnCheck)
        {
            ReverseMoveHistory[count, 0] = x;
            ReverseMoveHistory[count, 1] = y;
            ReverseMoveHistory[count, 2] = TurnCheck;
        }





    }






}
