using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleParameters.Set();//Sets the Console 

            DisplayParameters.Calculate();//Calculates and stores positions of stuff

            Hero hero = new Hero();
            List<Projectile> heroProjectiles = new List<Projectile>();
            List<Projectile> bonusProjectiles = new List<Projectile>();

            List<EnemyLine> enemyLines = new List<EnemyLine>();
            int EnemyLinesSpawned = 0;
            List<Projectile> enemyProjectiles = new List<Projectile>();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            long waitTime;
            long prevTime = 0, currTime;

            Display.PrintAllFields();

            while (true)
            {
                if (hero.Health <= 0)
                {
                    break;
                } // GameOver

                if (Shoot.ShouldOneHero(stopWatch.ElapsedMilliseconds, hero.EngineLevel))
                {
                    Shoot.AddHeroProjectiles(hero.Level, hero.GetPosition(), heroProjectiles, stopWatch.ElapsedMilliseconds);
                } //Add hero Projectiles

                foreach (EnemyLine enemyLine in enemyLines)
                {
                    int index = Shoot.ShouldWhichEnemy(stopWatch.ElapsedMilliseconds, enemyLine.GetNumberEnemies());

                    if (index >= 0)
                    {
                        Shoot.AddEnemyProjectile(enemyLine.GetCoordinatesForIndex(index), enemyProjectiles, stopWatch.ElapsedMilliseconds);
                    }
                }//Add enemyProjectiles

                if (stopWatch.ElapsedMilliseconds - 2 * EnemyLinesSpawned * (1 / DisplayParameters.EnemyMovementsPermS) > 0)
                {
                    EnemyLinesSpawned++;
                    enemyLines.Add(new EnemyLine(stopWatch.ElapsedMilliseconds));
                }//Enemy lines spawn

                Display.PrintAllMovement(hero, heroProjectiles, bonusProjectiles, enemyProjectiles, enemyLines, stopWatch.ElapsedMilliseconds);

                for (int i = 0; i < enemyLines.Count; i++)
                {
                    hero.GetPoints(enemyLines[i].ProjectileHit(heroProjectiles, bonusProjectiles, stopWatch.ElapsedMilliseconds));
                    if (enemyLines[i].GetNumberEnemies() == 0)
                    {
                        enemyLines.RemoveAt(i--);
                    }
                }//Hit enemies check

                hero.BonusProjectileHit(bonusProjectiles);//check for catch

                hero.EnemyProjectileHit(enemyProjectiles);//check for hero damage

                foreach (EnemyLine enemyLine in enemyLines)
                {
                    if (enemyLine.MoveAndIfHitBorder(stopWatch.ElapsedMilliseconds))
                    {
                        hero.TakeDamage(enemyLine.GetNumberEnemies());
                    }
                }// if enemy reaches border

                currTime = stopWatch.ElapsedMilliseconds;
                waitTime = (long)(1000 / 60 - (currTime - prevTime));
                prevTime = currTime;

                if (waitTime > 0)
                {
                    //Console.SetCursorPosition(50, 50);
                    //Console.Write(waitTime);
                    Thread.Sleep((int)waitTime);
                }

                if (Console.KeyAvailable)
                {
                    hero.Move(KeyReact.Input(Console.ReadKey(true)));
                    if (KeyReact.IsPaused)
                    {
                        stopWatch.Stop();
                        KeyReact.Pause();
                        stopWatch.Start();
                        Display.PrintClearPause();
                        Display.PrintAllFields();
                        Display.PrintForceAllMovement(hero, enemyLines);
                    }
                }// llee move Hero
                else
                {
                    hero.Equalize();
                }

            }//

            Display.PrintGameOver();

        }
    }
}
