using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public class EnemyLine
    {
        private List<Enemy> _enemies = new List<Enemy>();
        
        public long SpawTime { get; private set; }

        /// <summary>
        /// Creates an istance of an enemy line within the board and its appropriate stats.
        /// </summary>
        /// <param name="spawnTime"></param>
        public EnemyLine(long spawnTime)
        {
            SpawTime = spawnTime;

            for (int i = 0; i < DisplayParameters.NumberEnemiesLine;i++)
            {
                Coordinates coordinates = DisplayParameters.FirstEnemy;
                _enemies.Add(new Enemy(coordinates.SetDisplacements( (Designs.GetEnemy().Length + 1) * i , 1), spawnTime));
            }

        }

        /// <summary>
        /// Displays the enemy line, if it has moved.
        /// </summary>
        public void Display()
        {
            foreach(Enemy enemy in _enemies)
            {
                enemy.Display();
            }
        }

        /// <summary>
        /// Displays the enemy line, no matter if it has moved.
        /// </summary>
        public void ForceDisplay()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.ForceDisplay();
            }
        }

        /// <summary>
        /// Clears the enemy line from the display if it has moved.
        /// </summary>
        public void Clear()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.Clear();
            }
        }

        /// <summary>
        /// Chechs if the current enemy line has reached the border.
        /// </summary>
        /// <param name="currTime">The current game time.</param>
        /// <returns>Retuns true if the line has reached the hero line, false if not.</returns>
        public bool MoveAndIfHitBorder(long currTime)
        {
            if (_enemies.Count == 0)
            {
                throw new Exception("Do not use this function on empty list");
            }

            List<bool> check = new List<bool>();
            foreach (Enemy enemy in _enemies)
            {
                check.Add(enemy.MoveAndIfHit(currTime));
            }

            for (int i = 0; i < check.Count - 1; i++)
            {
                if (check[i] != check[i + 1])
                {
                    throw new Exception("Some enemies in the current enemy line have not hit the border");
                }
            }

            return check[0];
        }

        /// <summary>
        /// Chechs if the hero projectiles are overlaping or behind any enemy and if so does damage to them,
        /// removes the projectile and with RNG desides to add a bonus projectile.
        /// </summary>
        /// <param name="heroProjectiles">The list of all existant hero projectiles on the board.</param>
        /// <param name="bonusProjectiles">The list of all existant bonus projectiles on the board.</param>
        /// <param name="currTime">The current game time.</param>
        /// <returns>Returns the number of killed enemies.</returns>
        public int ProjectileHit(List<Projectile> heroProjectiles,List<Projectile> bonusProjectiles,long currTime)
        {
            int kills = 0;
            for(int i = 0; i < _enemies.Count; i++)
            {
                if (heroProjectiles.ProjectileHitList(_enemies[i].GetPosition(), _enemies[i].GetRelativeBoundry()))
                {
                    
                    _enemies[i].TakeDamage(1);
                    if (_enemies[i].Health <= 0)
                    {
                        _enemies[i].ForceClear();

                        if (Shoot.ShouldBonusFall())
                        {
                            Shoot.AddBonusProjectile(_enemies[i].GetPosition(), bonusProjectiles, currTime);
                        }

                        _enemies.RemoveAt(i--);
                        kills++;
                    }
                }
            }
            return kills;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Retuns the number of enemies in the current enemy line.</returns>
        public int GetNumberEnemies() => _enemies.Count;

        /// <summary>
        /// Finds the coordinates of an enemy by index in the current enemy line.
        /// </summary>
        /// <param name="index">The index of an enemy in the line.</param>
        /// <returns>The coordinates of the enemy.</returns>
        public Coordinates GetCoordinatesForIndex(int index)
        {
            return _enemies[index].GetPosition();
        }

    }

}
