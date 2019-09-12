using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// 
    /// </summary>
    public static class Shoot
    {
        private static int _FireRateHero = 3;// per second
        private static int _FireRateEnemies = 1;// per second
        private static int _heroCount = 0;
        private static int _enemiesCount = 0;
        private static int _bonusChance = 10;
        private static int _prevHeroEngineLevel = 1;
        private static Random _rnd = new Random();

        /// <summary>
        /// Checks if the hero should be shooting.
        /// </summary>
        /// <param name="time">The current time.</param>
        /// <param name="heroEngineLevel">The hero engine level.</param>
        /// <returns>Returns true if it should.</returns>
        public static bool ShouldOneHero(long time, int heroEngineLevel)
        {
            if(_prevHeroEngineLevel != heroEngineLevel)
            {
                _prevHeroEngineLevel = heroEngineLevel;

                _heroCount = (int)((double)time * (_FireRateHero + heroEngineLevel - 1) / 1000);

                return true;
            }
            if ((int)((double)time * (_FireRateHero + heroEngineLevel - 1) / 1000) > _heroCount)
            {
                _heroCount++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// By RNG determines which enemy in a line should be shooting.
        /// </summary>
        /// <param name="time">The current time.</param>
        /// <param name="numberEnemies">The total number of enemies in a line.</param>
        /// <returns></returns>
        public static int ShouldWhichEnemy(long time, int numberEnemies)
        {
            if ((int)((double)time * _FireRateEnemies / 1000) > _enemiesCount)
            {
                _enemiesCount++;
                return _rnd.Next(numberEnemies);
            }
            return -1;
        }

        /// <summary>
        /// Adds a projectile to the enemy projectiles depending on the current time and its possition.
        /// </summary>
        /// <param name="enemyCoordinates">The coordinates of the enemy.</param>
        /// <param name="enemyProjectiles">The list of enemy projectiles.</param>
        /// <param name="currTime">The current game time.</param>
        public static void AddEnemyProjectile(Coordinates enemyCoordinates, List<Projectile> enemyProjectiles,long currTime)
        {
            enemyProjectiles.Add(new Projectile(enemyCoordinates, ProjectileTypes.EnemyProjectile, currTime));
        }

        /// <summary>
        /// Determines by RNG if a bonus should be falling after a kill.
        /// </summary>
        /// <returns>Returns true if bonus should fall and false if not.</returns>
        public static bool ShouldBonusFall()
        {
            return _rnd.Next(_bonusChance)<=1
                ?true
                :false
                ;
        }

        /// <summary>
        /// Determines by RNG which bonus should fall. 
        /// </summary>
        /// <returns>Returns true or false and are equally likely.</returns>
        private static bool WhichBonusShouldFall()
        {
            return _rnd.Next(2) < 1
                ? true
                : false
                ;
        }

        /// <summary>
        /// Adds a projectile to the bonus projectiles depending on the current time and the possition.
        /// </summary>
        /// <param name="enemyCoordinates">The coordinates of the killed enemy.</param>
        /// <param name="enemyProjectiles">The list of bonus projectiles.</param>
        /// <param name="currTime">The current game time.</param>
        public static void AddBonusProjectile(Coordinates coordinates,List<Projectile> bonusProjectiles,long currTime)
        {
            bonusProjectiles.Add(new Projectile(coordinates,
                WhichBonusShouldFall()
                    ? ProjectileTypes.LevelUp
                    : ProjectileTypes.LifeBonus
                , currTime));
        }

        /// <summary>
        /// Adds a projectile to the hero projectiles depending on the current time, its level and its possition.
        /// </summary>
        /// <param name="heroLevel">The hero level.</param>
        /// <param name="heroCoordinates">The coordinates of the hero.</param>
        /// <param name="heroProjectiles">The list of hero projectiles.</param>
        /// <param name="currTime">The current game time.</param>
        public static void AddHeroProjectiles(int heroLevel, Coordinates heroCoordinates, List<Projectile> heroProjectiles, long currTime)
        {
            for (int i = 0; i < 5; i++)
            {
                if (ShouldPositionShoot(heroLevel, i))
                {
                    heroCoordinates.SetDisplacements(i, -1);
                    heroProjectiles.Add(new Projectile(heroCoordinates, ProjectileTypes.HeroProjectile, currTime));
                    heroCoordinates.SetDisplacements(-i,+1);
                }
            }
        }

        /// <summary>
        /// Determines if the position infront of ship should produce a projectile. All depends on the hero level.
        /// </summary>
        /// <param name="level">The hero level.</param>
        /// <param name="position">The hero position.</param>
        /// <returns></returns>
        private static bool ShouldPositionShoot(int level, int position)
        {
            switch (position)
            {
                case 0:
                    return (level == 4);

                case 1:
                    return (level > 1);

                case 2:
                    return (level != 2);

                case 3:
                    return (level > 1);

                case 4:
                    return (level == 4);

                default:
                    throw new ArgumentOutOfRangeException("Position is not infront of the space ship.");
            }
        }

    }
}
