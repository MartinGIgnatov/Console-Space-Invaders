using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SpaceInvaders
{
    public enum ProjectileTypes
    {
        HeroProjectile,
        EnemyProjectile,
        LifeBonus,
        LevelUp
    }

    public class Projectile
    {
        protected Coordinates _myInitialCoordinates;
        protected Coordinates _currCoordinates, _prevCoordinates;
        protected int _mSecondsPerMove;
        protected long _myInitialTime;

        public ProjectileTypes MyType { get; }

        public Coordinates Position => _currCoordinates;

        public Projectile(Coordinates coordinates, ProjectileTypes type, long initialTime)
        {
            _myInitialCoordinates = coordinates;
            _currCoordinates = coordinates;
            _prevCoordinates = coordinates;
            _myInitialTime = initialTime;
            MyType = type;

            switch (MyType)
            {
                case ProjectileTypes.EnemyProjectile:
                    _mSecondsPerMove = 50;
                    break;

                case ProjectileTypes.HeroProjectile:
                    _mSecondsPerMove = -20;
                    break
                        ;
                case ProjectileTypes.LifeBonus:
                    _mSecondsPerMove = 100;
                    break;

                case ProjectileTypes.LevelUp:
                    _mSecondsPerMove = 100;
                    break;
            }
        }

        /// <summary>
        /// Moves the projectile or deletes it if it is out of the board.
        /// </summary>
        /// <param name="currTime">The current game time.</param>
        /// <returns>Returns false if it is out of the board and true if it is in.</returns>
        public bool MoveOrDelete(long currTime)
        {
            int wantedPosition = _myInitialCoordinates.Y + (int)((double)(currTime - _myInitialTime) / _mSecondsPerMove);

            if (wantedPosition <= DisplayParameters.ScoreBoardLine.Y || wantedPosition > ConsoleParameters.Height - 1)
            {
                _prevCoordinates = _currCoordinates;
                return false;
            }
            else
            {
                _prevCoordinates = _currCoordinates;
                _currCoordinates.Y = wantedPosition;

                return true;
            }
        }

        /// <summary>
        /// Clears the projectile from the console, if it has moved.
        /// </summary>
        public void Clear()
        {

            Console.SetCursorPosition(_myInitialCoordinates.X, _prevCoordinates.Y);
            Console.Write(" ");

        }

        /// <summary>
        /// Clears the projectile from the console, no matter if it has moved.
        /// </summary>
        public void ForceClear()
        {

            Console.SetCursorPosition(_myInitialCoordinates.X, _currCoordinates.Y);
            Console.Write(" ");

        }

        /// <summary>
        /// Displays the projectile in the console if it has moved.
        /// </summary>
        public void Display()
        {

            switch (MyType)
            {
                case ProjectileTypes.EnemyProjectile:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case ProjectileTypes.HeroProjectile:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break
                        ;
                case ProjectileTypes.LifeBonus:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case ProjectileTypes.LevelUp:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }

            Console.SetCursorPosition(_myInitialCoordinates.X, _currCoordinates.Y);
            Console.Write("■");

            Console.ForegroundColor = ConsoleColor.White;

        }

    }
}
