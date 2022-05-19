using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace IFN563_Gomoku
{
    public class Board
    {
        public readonly static string board_element = ". ";

        public int Board_Column { get; set; }
        public int Board_Row { get; set; }
        public string Board_Element { get; }
        public string[,] GB { get; set; }

        //set board size -constructor
        public Board(int board_column, int board_row)
        {
            this.Board_Column = board_column;
            this.Board_Row = board_row;
            GB = new string[board_column, board_row];
        }
        //set board
        public void ResetBoard()
        {
            for (int i = 0; i < Board_Column; i++)
            {
                for (int j = 0; j < Board_Row; j++)
                {
                    GB[j, i] = board_element;
                }
            }
        }
        // draw board
        public void DrawBoard()
        {
            Clear();
            for (int i = 0; i < Board_Column; i++)
            {
                for (int j = 0; j < Board_Row; j++)
                {
                    Write(GB[j, i]);
                }
                WriteLine();
            }
        }
    }
}

