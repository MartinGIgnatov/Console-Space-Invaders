using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public static class DisplayParameters
    {
        public static Coordinates Fps { get; private set; }

        public static Coordinates HeroSpaceLine { get; private set; }

        public static Coordinates HeroInitial { get; private set; }

        public static Coordinates Lives { get; private set; }

        public static Coordinates Score { get; private set; }

        public static Coordinates ScoreBoardLine { get; private set; }

        public static Coordinates FirstEnemy { get; private set; }

        public static int NumberEnemiesLine { get; private set; }

        public static double EnemyMovementsPermS { get; private set; }

        /// <summary>
        /// Calculates the positions for all static fileds on the console and all initial positions of the moveing ones.
        /// </summary>
        public static void Calculate()
        {
            Score = new Coordinates(5, 3);
            Lives = new Coordinates(ConsoleParameters.Width / 2 - 5, 3);
            Fps = new Coordinates(ConsoleParameters.Width - 16, 3);
            ScoreBoardLine = new Coordinates(0, 5);
            HeroSpaceLine = new Coordinates(0, ConsoleParameters.Height - 7);
            HeroInitial = new Coordinates(ConsoleParameters.Width / 2 - 2, ConsoleParameters.Height - 5);
            FirstEnemy = new Coordinates(5, 6);
            NumberEnemiesLine = (int)((ConsoleParameters.Width - 10) / (Designs.GetEnemy().Length + 1)) ;
            EnemyMovementsPermS = 0.0002;

        }

    }
}
