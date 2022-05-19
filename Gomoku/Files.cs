using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static System.Console;

namespace IFN563_Gomoku
{
    public sealed class Files //singleton
    {
        private Files()
        {
        }

        public static Files File { get { return Nested.file; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly Files file = new Files();
        }
        public static List<Files> StepDetailList = new List<Files>();
        private int X { get; set; }
        private int Y { get; set; }
        private Player Player { get; set; }
        private int Count { get; set; }
        private int GameMode { get; set; }
        public Files(int gamemode, int count, Player player, int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Player = player;
            this.Count = count;
            this.GameMode = gamemode;
        }
        public string StepDetailsToString()
        {
            return GameMode + "," + Count + "," + Player.Player_ID + "," + Player.Player_Name + "," + Player.Player_Piece + "," + X + "," + Y;
        }


        public void SaveGame()
        {
            const string FILENAME = "GameSave.txt";
            FileStream outFile = new FileStream(FILENAME, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(outFile);

            foreach (Files Steps in StepDetailList)
                writer.WriteLine(Steps.StepDetailsToString());
            writer.Close();
            outFile.Close();
        }
    }
}
