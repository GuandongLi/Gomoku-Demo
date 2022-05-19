using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace IFN563_Gomoku
{
    public sealed class Indicators //singleton
    {
        private Indicators()
        {
        }

        public static Indicators Indicator { get { return Nested.indicator; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly Indicators indicator = new Indicators();
        }


        // Indicators Method
        // get input from user
        public int GetInput()
        {
            string temp = ReadLine();
            int Input;
            while (int.TryParse(temp, out Input) == false)
            {
                WriteLine("This is a invalid option!\nPlease enter your choice:");
                temp = ReadLine();
            }
            return Input;
        }

        // display entry menu
        public void DisplayStartGameMenu()
        {
            WriteLine("Hello there! Please select to start: ");
            WriteLine("1. Start Game\n2. Load Game\n3. Exit");
        }

        // display game type menu
        public void DisplayGameTypeMenu()
        {
            WriteLine("Please select the game type: ");
            WriteLine("1. Gomoku\n2. Connect-four");
        }

        // display game mode menu
        public void DisplayGameModeMenu()
        {
            WriteLine("Please select the game mode: ");
            WriteLine("1. Single-Player vs AI\n2. Two-Player with your friend");
        }

        // show how to play the game
        public void PlayKeyShowing()
        {
            SetCursorPosition(32, 7);
            WriteLine("Please use ← ↑ → ↓ to move your piece.");
            SetCursorPosition(32, 8);
            WriteLine("Use Enter or Space key to drop your piece.");
            SetCursorPosition(32, 9);
            WriteLine("S: Save the game  R: Redo  U: Undo  E: Exit");
        }

        // display player turn
        public void DisplayPlayerTurn(Player player)
        {
            SetCursorPosition(0, 17);
            WriteLine("____________________");
            WriteLine("Player {0}: {1} please drop your {2} piece.", player.Player_ID, player.Player_Name, player.Player_Piece);
        }

        // display invalid location
        public void DisplayInvalidLocation()
        {
            SetCursorPosition(0, 16);
            WriteLine("Invalid position for piece, please reselect: ");
        }

        // display win
        public void DisplayWin(Player player)
        {
            SetCursorPosition(0, 20);
            WriteLine("Congratulations!!! {0} won the game!", player.Player_Name);
        }

        // display undo/redo limit
        public void DisplayRedoUndo()
        {
            SetCursorPosition(0, 16);
            WriteLine("You have reached undo/redo limit! Press anykey to continue.");
            ReadKey();
        }

        // display save game
        public void DisplaySave()
        {
            SetCursorPosition(0, 16);
            WriteLine("Game has been saved.");
            ReadKey();
        }

    }




}
