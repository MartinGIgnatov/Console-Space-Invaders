using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SpaceInvaders
{
    public sealed class Hero : Ship
    {
        private int _prevDirection;
        private long _lastTimePressed = 0;
        private double _movementsPmS;
        private static Random _rnd = new Random();

        private Stopwatch movingTime = new Stopwatch();

        public int Score { get; private set; }

        public int EngineLevel { get; private set; }

        /// <summary>
        /// Initializes an instance of the hero with its basic stats.
        /// </summary>
        public Hero()
        {
            _coordinates = DisplayParameters.HeroInitial;
            _oldCoordinates = new Coordinates(0, 0);
            _relativeBoundry = new Coordinates(4, 1);
            Score = 0;
            Health = 3;
            movingTime.Start();
            EngineLevel = 1;
            Level = 1;

            _movementsPmS = (int)((double)ConsoleParameters.Width - 8.0) / 50 != 0
                        ? (int)((double)ConsoleParameters.Width - 8.0) / 50
                        : 1
                        ;
        }

        /// <summary>
        /// Displays the hero, if it has moved.
        /// </summary>
        public override void Display()
        {
            if (!_coordinates.Equals(_oldCoordinates))
            {
                for (int i = 0; i <= _relativeBoundry.Y; i++)
                {
                    if (Designs.GetHero(Level, EngineLevel)[i].Length + _coordinates.X > ConsoleParameters.Width)
                    {
                        throw new ArgumentOutOfRangeException($" {nameof(Hero)} is placed too close to boudry.");
                    }
                    ;
                    Console.SetCursorPosition(_coordinates.X, _coordinates.Y + i);
                    Console.Write(Designs.GetHero(Level, EngineLevel)[i]);
                }
            }

        }

        /// <summary>
        /// Displays the hero, no matter if it has moved.
        /// </summary>
        public void ForceDisplay()
        {
            for (int i = 0; i <= _relativeBoundry.Y; i++)
            {
                if (Designs.GetHero(Level, EngineLevel)[i].Length + _coordinates.X > ConsoleParameters.Width)
                {
                    throw new ArgumentOutOfRangeException($" {nameof(Hero)} is placed too close to boudry.");
                }
                
                Console.SetCursorPosition(_coordinates.X, _coordinates.Y + i);
                Console.Write(Designs.GetHero(Level, EngineLevel)[i]);
            }
        }

        /// <summary>
        /// Equalizes the old coordinates with the current ones.
        /// </summary>
        public void Equalize() => _oldCoordinates = _coordinates;

        /// <summary>
        /// Increases the score of the hero by one for each kill.
        /// </summary>
        /// <param name="kills">The given number of kills.</param>
        public void GetPoints(int kills) => Score += kills;

        /// <summary>
        /// Clears the existand image of the hero, if it has moved.
        /// </summary>
        public override void Clear()
        {
            if (!_coordinates.Equals(_oldCoordinates))
            {
                for (int i = 0; i <= _relativeBoundry.Y; i++)
                {
                    Console.SetCursorPosition(_oldCoordinates.X, _oldCoordinates.Y + i);
                    Console.Write("     ");
                }
            }
        }

        /// <summary>
        /// Finds the first, if existand, bonus projectile that overlaps or is below the hero ship and does the appropriate action.
        /// </summary>
        /// <param name="bonusProjectiles">The list of all bonus projectiles on the board.</param>
        public void BonusProjectileHit(List<Projectile> bonusProjectiles)
        {
            ProjectileTypes projectileType = new ProjectileTypes();

            if (bonusProjectiles.ProjectileHitList(GetPosition(), GetRelativeBoundry(), out projectileType))
            {
                if (projectileType == ProjectileTypes.LevelUp)
                {
                    if (_rnd.Next(2) == 0)
                    {
                        LevelUp();
                    }
                    else
                    {
                        EngineLevelUp();
                    }
                }
                else
                {
                    HealthUp();
                }
            }

            ForceDisplay();
        }

        /// <summary>
        /// Chechs if there is at least one overlap between enemy projectile and the hero and if so does one damage to the hero.
        /// </summary>
        /// <param name="enemyProjectiles">The list of existing enemy projectiles on the board.</param>
        public void EnemyProjectileHit(List<Projectile> enemyProjectiles)
        {
            if (enemyProjectiles.ProjectileHitList(GetPosition(),GetRelativeBoundry()))
            {
                TakeDamaged();
            }
        }

        /// <summary>
        /// Moves the hero in the given direction.
        /// </summary>
        /// <param name="direction">Plus one means left minus one means right and zero means lack og movement.</param>
        public void Move(int direction)
        {
            if (direction != 0)
            {
                if (direction != _prevDirection)
                {
                    ;
                    _wantedPosition = _coordinates.X + direction;
                    _lastTimePressed = movingTime.ElapsedMilliseconds;
                    _prevDirection = direction;

                }
                else
                {
                    if ((movingTime.ElapsedMilliseconds - _lastTimePressed) > (_movementsPmS / EngineLevel))
                    {
                        _wantedPosition = _coordinates.X + direction;
                        _lastTimePressed = movingTime.ElapsedMilliseconds;
                    }
                }
            }

            _oldCoordinates = _coordinates;


            if (_wantedPosition < 3)
            {
                _coordinates.X = 3;
            }
            else if (_wantedPosition + _relativeBoundry.X + 3 >= ConsoleParameters.Width - 1)
            {
                _coordinates.X = ConsoleParameters.Width - 4 - _relativeBoundry.X;
            }
            else
            {
                _coordinates.X = _wantedPosition;
            }
        }

        /// <summary>
        /// Increase the engine level by one if not maxed out.
        /// </summary>
        public void EngineLevelUp()
        {
            EngineLevel = EngineLevel < 4
                ? EngineLevel + 1
                : EngineLevel
                ;
        }

        /// <summary>
        /// Increase the health by one if not maxed out.
        /// </summary>
        public void HealthUp()
        {
            Health = Health < 6
                ? Health + 1
                : Health
                ;
        }

        /// <summary>
        /// Increase the level by one if not maxed out.
        /// </summary>
        public void LevelUp()
        {
            Level = Level < 4
                ? Level + 1
                : Level
                ;
        }

    }
}
