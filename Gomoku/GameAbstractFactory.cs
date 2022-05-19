using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using IFN563_Gomoku;

namespace IFN563_Gomoku
{
    abstract class GameAbstractFactory
    {
        public abstract BoardFactory CreateBoard();
        public abstract PieceFactory CreatePiece();
    }

    // Gomoku factory
    class GomokuFactory : GameAbstractFactory
    {
        public override BoardFactory CreateBoard()
        {
            return new GomokuBoard();
        }

        public override PieceFactory CreatePiece()
        {
            return new GomokuPiece();
        }
    }

    // Connect-4 factory
    //class Connect4Factory
    //{

    //}

    // Abstact product
    interface BoardFactory
    {
        public int BoardColumn();
        public int BoardRow();
    }

    interface PieceFactory
    {
        public string PieceShape1();
        public string PieceShape2();
    }

    class GomokuBoard : BoardFactory
    {
        public int Board_Column { get; set; }
        public int Board_Row { get; set; }

        public GomokuBoard()
        {
            this.Board_Column = 15;
            this.Board_Row = 15;
        }

        public int BoardColumn()
        {
            return Board_Column;
        }
        public int BoardRow()
        {
            return Board_Row;
        }

    }

    class GomokuPiece : PieceFactory
    {
        public string Piece_Shape1 { get; set; }
        public string Piece_Shape2 { get; set; }
        public GomokuPiece()
        {
            this.Piece_Shape1 = "X ";
            this.Piece_Shape2 = "O ";
        }

        public string PieceShape1()
        {
            return Piece_Shape1;
        }
        public string PieceShape2()
        {
            return Piece_Shape2;
        }
    }

    // client
    class Client
    {
        BoardFactory _board;
        PieceFactory _piece;
        public string Piece_Shape { get; set; }
        public int Player_ID { get; set; }
        public string Player_Shape { get; set; }

        public Client(GameAbstractFactory factory)
        {
            _board = factory.CreateBoard();
            _piece = factory.CreatePiece();
        }
        public string GetPieceShape1()
        {
            return _piece.PieceShape1();
        }
        public string GetPieceShape2()
        {
            return _piece.PieceShape2();
        }
        public int GetBoardColumn()
        {
            return _board.BoardColumn();
        }
        public int GetBoardRow()
        {
            return _board.BoardRow();
        }
    }
}
