using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace IFN563_Gomoku
{

    public sealed class Judgements //singleton
    {
        private Judgements()
        {
        }

        public static Judgements Judgement { get { return Nested.judgement; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly Judgements judgement = new Judgements();
        }

        private int X { get; set; }
        private int Y { get; set; }

        // check pieve valid
        public bool CheckPieceValid(Board gameboard, int X, int Y)
        {
            bool piecevalid;
            if (gameboard.GB[X, Y] == ". ")
            {
                piecevalid = true;
            }
            else
            {
                piecevalid = false;
            }
            return piecevalid;
        }

        // check unbroken chain
        public bool CheckUBChain(Board gameboard, int X, int Y)
        {
            int count = 0;
            int tempX = X, tempY = Y;

            // vertical check
            while (Y > 0)
            {
                if (gameboard.GB[tempX, tempY] == gameboard.GB[X, --Y])
                {
                    count++;
                }
                else
                    break;
            }
            if (count >= 4)
            {
                return true;
            }
            X = tempX; Y = tempY;
            while (Y < 14)
            {
                if (gameboard.GB[tempX, tempY] == gameboard.GB[X, ++Y])
                {
                    count++;
                }
                else
                    break;
            }
            if (count >= 4)
            {
                return true;
            }
            X = tempX; Y = tempY; count = 0;

            // horizontal check
            while (X > 0)
            {
                if (gameboard.GB[tempX, tempY] == gameboard.GB[--X, Y])
                {
                    count++;
                }
                else
                    break;
            }
            if (count >= 4)
            {
                return true;
            }
            X = tempX; Y = tempY;
            while (X < 14)
            {
                if (gameboard.GB[tempX, tempY] == gameboard.GB[++X, Y])
                {
                    count++;
                }
                else
                    break;
            }
            if (count >= 4)
            {
                return true;
            }
            X = tempX; Y = tempY; count = 0;

            // diagonal check
            // positive type
            while (Y > 0 && X > 0)
            {
                if (gameboard.GB[tempX, tempY] == gameboard.GB[--X, --Y])
                {
                    count++;
                }
                else
                    break;
            }
            if (count >= 4)
            {
                return true;
            }
            X = tempX; Y = tempY;
            while (Y < 14 && X < 14)
            {
                if (gameboard.GB[tempX, tempY] == gameboard.GB[++X, ++Y])
                {
                    count++;
                }
                else
                    break;
            }
            if (count >= 4)
            {
                return true;
            }
            X = tempX; Y = tempY; count = 0;

            // negative type
            while (Y > 0 && X < 14)
            {
                if (gameboard.GB[tempX, tempY] == gameboard.GB[++X, --Y])
                {
                    count++;
                }
                else
                    break;
            }
            if (count >= 4)
            {
                return true;
            }
            X = tempX; Y = tempY;
            while (Y < 14 && X > 0)
            {
                if (gameboard.GB[tempX, tempY] == gameboard.GB[--X, ++Y])
                {
                    count++;
                }
                else
                    break;
            }
            if (count >= 4)
            {
                return true;
            }
            return false;


        }

    }

}


