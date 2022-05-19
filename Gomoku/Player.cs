using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace IFN563_Gomoku
{
    public abstract class Player
    {
        // properties
        public int Player_ID { get; set; }
        public string Player_Name { get; set; }
        public string Player_Piece { get; set; }

        public abstract void GetPlayerName(int Player_ID);
        public void GetPlayerID()
        {
            this.Player_ID = Player_ID;
        }

        public class HumanPlayer : Player
        {
            public override void GetPlayerName(int Player_ID)
            {
                WriteLine("Player {0} please enter your name: ", Player_ID);
                this.Player_Name = ReadLine();
            }
        }

        public class AI : Player
        {
            public override void GetPlayerName(int Player_ID)
            {
                AI ai = new AI();
                this.Player_Name = "Smartest little boy";
            }
        }


    }
}
