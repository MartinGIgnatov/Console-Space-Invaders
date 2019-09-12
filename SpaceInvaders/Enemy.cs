using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public class Enemy : Ship
    {
        private Coordinates _myInitialCoordinates;

        public long SpawnTime { get; }

        public Enemy(Coordinates coordinates, long spawnTime)
        {
            SpawnTime = spawnTime;
            _myInitialCoordinates = coordinates;
            _coordinates = coordinates;
            _oldCoordinates = coordinates;
            _oldCoordinates.SetDisplacements(0, -1);
            _relativeBoundry = new Coordinates(Designs.GetEnemy().Length-1, 0);
        }

        public override void Display()
        {
            if (!_coordinates.Equals(_oldCoordinates))
            {
                for (int i = 0; i <= _relativeBoundry.Y; i++)
                {
                    if (Designs.GetEnemy().Length + _coordinates.X > ConsoleParameters.Width - 5)
                    {
                        throw new ArgumentOutOfRangeException($" {nameof(Enemy)} is placed too close to boudry.");
                    }
                    ;
                    Console.SetCursorPosition(_coordinates.X, _coordinates.Y + i);
                    Console.Write(Designs.GetEnemy());
                }
            }
        }

        public void ForceDisplay()
        {
            for (int i = 0; i <= _relativeBoundry.Y; i++)
            {
                if (Designs.GetEnemy().Length + _coordinates.X > ConsoleParameters.Width - 5)
                {
                    throw new ArgumentOutOfRangeException($" {nameof(Enemy)} is placed too close to boudry.");
                }
                ;
                Console.SetCursorPosition(_coordinates.X, _coordinates.Y + i);
                Console.Write(Designs.GetEnemy());
            }
        }

        public override void Clear()
        {
            if (!_coordinates.Equals(_oldCoordinates))
            {
                for (int i = 0; i <= _relativeBoundry.Y; i++)
                {
                    Console.SetCursorPosition(_oldCoordinates.X, _oldCoordinates.Y + i);
                    Console.Write("     ");//5
                }
            }
        }

        public void ForceClear()
        {
            for (int i = 0; i <= _relativeBoundry.Y; i++)
            {
                Console.SetCursorPosition(_coordinates.X, _coordinates.Y + i);
                Console.Write("     ");//5
            }
        }

        public bool MoveAndIfHit(long currTime)
        {
            _oldCoordinates = _coordinates;

            _wantedPosition = _myInitialCoordinates.Y + (int)((currTime - SpawnTime) * DisplayParameters.EnemyMovementsPermS);

            if (_wantedPosition != _coordinates.Y)
            {
                _coordinates.Y =  _wantedPosition;
            }

            if (_coordinates.Y >= DisplayParameters.HeroSpaceLine.Y)
            {
                return true;
            }
            return false;
        }

    }
}
