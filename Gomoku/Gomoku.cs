using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.IO;

namespace IFN563_Gomoku
{
    class Gomoku : Game
    {
        public static int Count = -1;
        private int stepCount = -1;
        public static int TurnCheck = 1;
        Client gomoku = new Client(new GomokuFactory());
        public override void GetPlayerInfo()
        {
            HumanPlayer1 = new Player.HumanPlayer();
            HumanPlayer1.Player_ID = 1;
            HumanPlayer1.GetPlayerName(HumanPlayer1.Player_ID);
            HumanPlayer1.Player_Piece = GetPiece(HumanPlayer1.Player_ID);
            if (GameModeInput == 1)
            {
                AIPlayer = new Player.AI();
                AIPlayer.Player_ID = 2;
                AIPlayer.GetPlayerName(AIPlayer.Player_ID);
                AIPlayer.Player_Piece = GetPiece(AIPlayer.Player_ID);
            }
            else
            {
                HumanPlayer2 = new Player.HumanPlayer();
                HumanPlayer2.Player_ID = 2;
                HumanPlayer2.GetPlayerName(HumanPlayer2.Player_ID);
                HumanPlayer2.Player_Piece = GetPiece(HumanPlayer2.Player_ID);
            }
        }
        public override void GetBoardInfo()
        {
            Board_Column = gomoku.GetBoardColumn();
            Board_Row = gomoku.GetBoardRow();
            gameboard = new Board(Board_Column, Board_Row);
            MovementHistory.MH.CreateMoveSave();
            gameboard.ResetBoard();
            gameboard.DrawBoard();
        }
        public override string GetPiece(int Player_ID)
        {
            switch (Player_ID)
            {
                case 1:
                    Player_Piece = gomoku.GetPieceShape1();
                    break;
                case 2:
                    Player_Piece = gomoku.GetPieceShape2();
                    break;
            }
            return Player_Piece;
        }
        public override int LoadGame(Board gameboard)
        {
            const string FILENAME = "GameSave.txt";
            FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);
            HumanPlayer1 = new Player.HumanPlayer();
            AIPlayer = new Player.AI();
            HumanPlayer2 = new Player.HumanPlayer();
            while (!reader.EndOfStream)
            {
                Turn = !Turn;
                string readin = reader.ReadLine();
                string[] savings = readin.Split(',');
                int GameMode = int.Parse(savings[0]);
                GameModeInput = GameMode;
                int Count = int.Parse(savings[1]);
                int Input_X = int.Parse(savings[5]);
                int Input_Y = int.Parse(savings[6]);
                switch (int.Parse(savings[2]))
                {
                    case 1:
                        HumanPlayer1.Player_ID = int.Parse(savings[2]);
                        HumanPlayer1.Player_Name = savings[3];
                        HumanPlayer1.Player_Piece = savings[4];
                        gameboard.GB[Input_X / 2, Input_Y] = HumanPlayer1.Player_Piece;
                        Files.StepDetailList.Add(new Files(GameMode, Count, HumanPlayer1, Input_X, Input_Y));
                        break;
                    case 2:
                        if (GameMode == 1)
                        {
                            AIPlayer.Player_ID = int.Parse(savings[2]);
                            AIPlayer.Player_Name = savings[3];
                            AIPlayer.Player_Piece = savings[4];
                            gameboard.GB[Input_X / 2, Input_Y] = AIPlayer.Player_Piece;
                            Files.StepDetailList.Add(new Files(GameMode, Count, AIPlayer, Input_X, Input_Y));
                        }
                        else
                        {
                            HumanPlayer2.Player_ID = int.Parse(savings[2]);
                            HumanPlayer2.Player_Name = savings[3];
                            HumanPlayer2.Player_Piece = savings[4];
                            gameboard.GB[Input_X / 2, Input_Y] = HumanPlayer2.Player_Piece;
                            Files.StepDetailList.Add(new Files(GameMode, Count, HumanPlayer2, Input_X, Input_Y));
                        }
                        break;
                }
            }
            gameboard.DrawBoard();
            reader.Close();
            inFile.Close();
            return GameModeInput;
        }
        public override void PlayGame()
        {
            while (!isWin)
            {
                Turn = !Turn;
                Indicators.Indicator.PlayKeyShowing();
                switch (Turn)
                {
                    case true:
                        Indicators.Indicator.DisplayPlayerTurn(HumanPlayer1);
                        CursorMove(gameboard);
                        gameboard.DrawBoard();
                        TurnCheck = 0;
                        break;
                    case false:
                        if (GameModeInput == 1)
                        {
                            Indicators.Indicator.DisplayPlayerTurn(AIPlayer);
                            bool AICheck = false;
                            while (!AICheck)
                            {
                                Input_X = AIThinking.AI.GetAIX();
                                Input_Y = AIThinking.AI.GetAIY();
                                AICheck = Judgements.Judgement.CheckPieceValid(gameboard, Input_X, Input_Y);
                            }
                            TurnCheck = 1;
                            Count++;
                            stepCount++;
                            MovementHistory.MH.MoveSave(Count, Input_X, Input_Y);
                            MovementHistory.MH.ReverseMoveSave(Count, Input_X, Input_Y, TurnCheck);
                            DropPiece(gameboard, Input_X, Input_Y);
                            Files.StepDetailList.Add(new Files(GameModeInput, Count, AIPlayer, Input_X, Input_Y));
                            gameboard.DrawBoard();
                            break;
                        }
                        else
                        {
                            Indicators.Indicator.DisplayPlayerTurn(HumanPlayer2);
                            CursorMove(gameboard);
                            gameboard.DrawBoard();
                            TurnCheck = 1;
                            break;
                        }
                }
            }
            if (Turn)
                Indicators.Indicator.DisplayWin(HumanPlayer1);
            else
                if (GameModeInput == 1)
                Indicators.Indicator.DisplayWin(AIPlayer);
            else
                Indicators.Indicator.DisplayWin(AIPlayer);
        }
        public override void DropPiece(Board gameboard, int Input_X, int Input_Y)
        {
            Count++;
            stepCount++;
            switch (Turn)
            {
                case true:
                    gameboard.GB[Input_X / 2, Input_Y] = HumanPlayer1.Player_Piece;
                    Files.StepDetailList.Add(new Files(GameModeInput, Count, HumanPlayer1, Input_X, Input_Y));
                    gameboard.DrawBoard();
                    TurnCheck = 0;
                    break;
                case false:
                    if (GameModeInput == 1)
                        gameboard.GB[Input_X / 2, Input_Y] = AIPlayer.Player_Piece;
                    else
                    {
                        gameboard.GB[Input_X / 2, Input_Y] = HumanPlayer2.Player_Piece;
                        Files.StepDetailList.Add(new Files(GameModeInput, Count, HumanPlayer2, Input_X, Input_Y));
                        gameboard.DrawBoard();
                    }
                    TurnCheck = 1;
                    break;
            }
            MovementHistory.MH.MoveSave(Count, Input_X / 2, Input_Y);
            MovementHistory.MH.ReverseMoveSave(Count, Input_X / 2, Input_Y, TurnCheck);
        }
        public override void CursorMove(Board gameboard)
        {
            int X = 14;
            int Y = 7;
            bool IsInput = false;
            SetCursorPosition(X, Y);
            Invoker invoker = new Invoker();
            // excute cursor movement based on key entered
            while (!IsInput)
            {
                ConsoleKey Key = ReadKey(true).Key;
                SetCursorPosition(X, Y);
                switch (Key)
                {
                    case ConsoleKey.UpArrow:
                        if (Y > 0 && Y < gameboard.Board_Row)
                            Y--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (Y >= 0)
                            Y++;
                        if (Y >= gameboard.Board_Row)
                            Y = gameboard.Board_Row - 1;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (X > 0)
                            X = X - 2;
                        if (X <= 0)
                            X = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        if (X / 2 < gameboard.Board_Column)
                            X = X + 2;
                        if (X / 2 >= gameboard.Board_Column)
                            X = (gameboard.Board_Column - 1) * 2;
                        break;
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Enter:
                        SetCursorPosition(X, Y);
                        bool valid = Judgements.Judgement.CheckPieceValid(gameboard, X / 2, Y);

                        if (valid == true)
                            DropPiece(gameboard, X, Y);
                        else
                        {
                            Indicators.Indicator.DisplayInvalidLocation();
                            IsInput = false;
                            break;
                        }
                        isWin = Judgements.Judgement.CheckUBChain(gameboard, X / 2, Y);
                        IsInput = true;
                        break;
                    case ConsoleKey.U:
                        if (Count > 0)
                        {
                            ICommand Ucommand = invoker.GetCommand("U");
                            Ucommand.Execute(gameboard, Count);
                            Count--;
                            Ucommand = invoker.GetCommand("U");
                            Ucommand.Execute(gameboard, Count);
                            Count--;
                        }
                        else
                            Indicators.Indicator.DisplayRedoUndo();
                        Turn = !Turn;
                        IsInput = true;
                        break;
                    case ConsoleKey.R:
                        if (Count < stepCount)
                        {
                            Count++;
                            ICommand Rcommand = invoker.GetCommand("R");
                            Rcommand.Execute(gameboard, Count);
                            Count++;
                            Rcommand = invoker.GetCommand("R");
                            Rcommand.Execute(gameboard, Count);
                        }
                        else
                            Indicators.Indicator.DisplayRedoUndo();
                        Turn = !Turn;
                        IsInput = true;
                        break;
                    case ConsoleKey.S:
                        Files.File.SaveGame();
                        Indicators.Indicator.DisplaySave();
                        break;
                    case ConsoleKey.E:
                        Environment.Exit(0);
                        break;
                }
                SetCursorPosition(X, Y);
            }
        }
    }
}
