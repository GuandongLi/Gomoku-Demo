using System;
using System.Collections.Generic;
using System.Text;
using static IFN563_Gomoku.GameAbstractFactory;
using static System.Console;

namespace IFN563_Gomoku
{
    public abstract class Game
    {
        //properties
        private int StartGameInput { get; set; }
        public static int GameModeInput { get; set; }
        private int GameTypeInput { get; set; }
        public static int Board_Column { get; set; }
        public static int Board_Row { get; set; }
        public static bool Turn { get; set; }
        public bool isWin { get; set; }
        public int Input_X { get; set; }
        public int Input_Y { get; set; }
        public static Board gameboard { get; set; }
        public static Player HumanPlayer1 { get; set; }
        public static Player HumanPlayer2 { get; set; }
        public static Player AIPlayer { get; set; }
        public string Player_Piece { get; set; }
        // Start Game (template method)
        public void StartGame()
        {
            Indicators.Indicator.DisplayStartGameMenu();
            StartGameInput = Indicators.Indicator.GetInput();
            switch (StartGameInput)
            {
                case 1:
                    SelectGameType();
                    break;
                case 2:
                    GetBoardInfo();
                    LoadGame(gameboard);
                    PlayGame();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        //select game type (gomoku or connect-four)
        private void SelectGameType()
        {
            Indicators.Indicator.DisplayGameTypeMenu();
            GameTypeInput = Indicators.Indicator.GetInput();
            switch (GameTypeInput)
            {
                case 1:
                    SelectGameMode();
                    break;

                case 2:
                    while (GameModeInput != 1)
                    {
                        WriteLine("This game mode is not available currently.\nPlease reselect game mode.");
                        GameModeInput = Indicators.Indicator.GetInput();
                    }
                    SelectGameMode();
                    break;
                default:
                    break;
            }
        }

        // select game mode (single player or two player)
        private void SelectGameMode()
        {
            Indicators.Indicator.DisplayGameModeMenu();
            GameModeInput = Indicators.Indicator.GetInput();

            switch (GameModeInput)
            {
                case 1:
                    //single player
                    GetPlayerInfo();
                    GetBoardInfo();
                    PlayGame();
                    break;
                case 2:
                    //two player
                    GetPlayerInfo();
                    GetBoardInfo();
                    PlayGame();
                    break;
                default:
                    break;
            }
        }
        public abstract void PlayGame();
        //get piece
        public abstract string GetPiece(int Player_ID);
        public abstract void GetPlayerInfo();
        public abstract void GetBoardInfo();
        public abstract int LoadGame(Board gameboard);
        public abstract void DropPiece(Board gameboard, int Input_X, int Input_Y);
        public abstract void CursorMove(Board gameboard);
    }
}
