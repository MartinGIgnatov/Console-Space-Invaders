using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public static class Display
    {

        public static void PrintPause()
        {
            Console.Clear();
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2));
            Console.WriteLine("Game Paused");
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2 + 1));
            Console.WriteLine("-----------");
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2 + 2));
            Console.WriteLine("Press space");

        }

        public static void PrintGameOver()
        {
            Console.Clear();
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2));
            Console.WriteLine("Game Over");
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2 + 1));
            Console.WriteLine("---------");
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2 + 2));
            Console.WriteLine("You Lost!");
            Console.ReadKey();
        }

        public static void PrintClearPause()
        {
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2));
            Console.WriteLine("           ");
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2 + 1));
            Console.WriteLine("           ");
            Console.SetCursorPosition((int)((ConsoleParameters.Width - 11) / 2), (int)(ConsoleParameters.Height / 2 + 2));
            Console.WriteLine("           ");

        }

        public static void PrintAllFields()
        {
            PrintScoreField();
            PrintLivesField();
            PrintFpsField();
            PrintScoreBoardLine();
            PrintHeroSpaceLine();
        }

        private static void PrintFpsField()
        {
            Console.SetCursorPosition(DisplayParameters.Fps.X, DisplayParameters.Fps.Y);
            Console.Write($"FPS : ");
        }

        private static void PrintHeroSpaceLine()
        {
            Console.SetCursorPosition(DisplayParameters.HeroSpaceLine.X, DisplayParameters.HeroSpaceLine.Y);
            Console.Write("---");
            Console.SetCursorPosition(ConsoleParameters.Width - 3, DisplayParameters.HeroSpaceLine.Y);
            Console.Write("---");

        }

        private static void PrintLivesField()
        {
            Console.SetCursorPosition(DisplayParameters.Lives.X, DisplayParameters.Lives.Y);
            Console.Write($"Lives :");
        }

        private static void PrintScoreField()
            {
                Console.SetCursorPosition(DisplayParameters.Score.X, DisplayParameters.Score.Y);
                Console.Write($"Score : ");
            }

        private static void PrintScoreBoardLine()
        {
            Console.SetCursorPosition(DisplayParameters.ScoreBoardLine.X, DisplayParameters.ScoreBoardLine.Y);
            for (int i = 0; i < ConsoleParameters.Width; i++)
            {
                Console.Write("*");
            }
        }

        /// <summary>
        /// Clears and prints all objects if they have experianced a movement.
        /// </summary>
        /// <param name="hero"></param>
        /// <param name="heroProjectiles"></param>
        /// <param name="bonusProjectiles"></param>
        /// <param name="enemyProjectiles"></param>
        /// <param name="enemyLines"></param>
        /// <param name="currTime"></param>
        public static void PrintAllMovement(
            Hero hero, List<Projectile> heroProjectiles, List<Projectile> bonusProjectiles,
            List<Projectile> enemyProjectiles, List<EnemyLine> enemyLines, long currTime)
        {
            PrintScore(hero);
            PrintLives(hero);
            PrintFps();
            hero.Clear();
            hero.Display();
            PrintProjectiles(heroProjectiles, currTime);
            PrintProjectiles(bonusProjectiles, currTime);
            PrintProjectiles(enemyProjectiles, currTime);
            foreach (EnemyLine enemyLine in enemyLines)
            {
                enemyLine.Clear();
                enemyLine.ForceDisplay();
            }

        }

        /// <summary>
        /// Prints all movements for the hero and the enemy lines no matter if anything has moved.
        /// </summary>
        /// <param name="hero">The existing instance of the hero.</param>
        /// <param name="enemyLines">All existant enemy lines.</param>
        public static void PrintForceAllMovement(Hero hero,List<EnemyLine> enemyLines)
        {
            hero.ForceDisplay();
            foreach (EnemyLine enemyLine in enemyLines)
            {
                enemyLine.ForceDisplay();
            }

        }

        private static void PrintFps()
        {
            Console.SetCursorPosition(DisplayParameters.Fps.X + 6, DisplayParameters.Fps.Y);
            Console.Write(FPS.Get());
        }

        private static void PrintLives(Hero hero)
        {
            Console.SetCursorPosition(DisplayParameters.Lives.X + 7, DisplayParameters.Lives.Y);
            for (int i = 0; i < hero.Health; i++)
            {
                Console.Write(" !");
            }
            for (int i = hero.Health; i < 12; i++)
            {
                Console.Write("  ");
            }

        }

        private static void PrintProjectiles(List<Projectile> projectiles,long currTime)
        {
            
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (!projectiles[i].MoveOrDelete(currTime))
                {
                    projectiles[i].Clear();
                    projectiles.RemoveAt(i--);
                }
            }
            projectiles.ForEach(projectile => projectile.Clear());
            projectiles.ForEach(projectile => projectile.Display());
        }

        private static void PrintScore(Hero hero)
        {
            Console.SetCursorPosition(DisplayParameters.Score.X + 8, DisplayParameters.Score.Y);
            Console.Write(hero.Score);
        }

    }
}
