using System;
using System.Collections.Generic;
using System.Text;

namespace IFN563_Gomoku
{
    public sealed class AIThinking //singleton
    {
        private AIThinking()
        {
        }

        public static AIThinking AI { get { return Nested.ai; } }

        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly AIThinking ai = new AIThinking();
        }

        public int GetAIX()
        {
            int AI_X;
            Random Xrandom = new Random();
            AI_X = Xrandom.Next(0, 14);
            return AI_X;
        }
        public int GetAIY()
        {
            int AI_Y;
            Random Yrandom = new Random();
            AI_Y = Yrandom.Next(0, 14);
            return AI_Y;
        }

    }
}
