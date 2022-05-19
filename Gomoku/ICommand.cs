using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace IFN563_Gomoku
{
    public interface ICommand
    {
        void Execute(Board gameboard, int count);
    }
    public class RedoCommand : ICommand
    {
        private int X { get; set; }
        private int Y { get; set; }
        public RedoCommand()
        {
        }
        public void Execute(Board gameboard, int count)
        {
            X = MovementHistory.MH.ReverseMoveHistory[count, 0];
            Y = MovementHistory.MH.ReverseMoveHistory[count, 1];
            X = MovementHistory.MH.MoveHistory[count, 0];
            Y = MovementHistory.MH.MoveHistory[count, 1];
            if (MovementHistory.MH.ReverseMoveHistory[count, 2] == 0)
            {
                gameboard.GB[X, Y] = Game.HumanPlayer1.Player_Piece;
                Files.StepDetailList.Add(new Files(Game.GameModeInput, count, Game.HumanPlayer1, X, Y));
            }
            else if (Game.GameModeInput == 1)
            {
                gameboard.GB[X, Y] = Game.AIPlayer.Player_Piece;
                Files.StepDetailList.Add(new Files(Game.GameModeInput, count, Game.AIPlayer, X, Y));
            }
            else
            {
                gameboard.GB[X, Y] = Game.HumanPlayer2.Player_Piece;
                Files.StepDetailList.Add(new Files(Game.GameModeInput, count, Game.HumanPlayer2, X, Y));
            }
        }
    }
    public class UndoCommand : ICommand
    {
        private int X { get; set; }
        private int Y { get; set; }
        public UndoCommand()
        {
        }
        public void Execute(Board gameboard, int count)
        {
            X = MovementHistory.MH.MoveHistory[count, 0];
            Y = MovementHistory.MH.MoveHistory[count, 1];
            gameboard.GB[X, Y] = Board.board_element;
            Files.StepDetailList.RemoveAt(count);
        }
    }
    public class Invoker
    {
        ICommand cmd = null;
        public ICommand GetCommand(string action)
        {
            switch (action)
            {
                case "U":
                    cmd = new UndoCommand();
                    break;
                case "R":
                    cmd = new RedoCommand();
                    break;
                default:
                    break;
            }
            return cmd;
        }
    }
}







