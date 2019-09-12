using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public struct Coordinates
    {
        private int _x;
        private int _y;

        public int X
        {
            get => _x;
            set
            {
                if (value > ConsoleParameters.Width || value < 0)
                {
                    throw new ArgumentOutOfRangeException("X - coordinate out of range.");
                }
                else
                {
                    _x = value;
                }
            }
        }

        public int Y
        {
            get => _y;
            set
            {
                if (value >= ConsoleParameters.Height || value < 0)
                {
                    throw new ArgumentOutOfRangeException("Y - coordinate out of range.");
                }
                else
                {
                    _y = value;
                }
            }
        }

        public Coordinates(int x, int y)
        {
            if (x > ConsoleParameters.Width || x < 0)
            {
                throw new ArgumentOutOfRangeException("X - coordinate out of range.");
            }
            _x = x;
            if (y >= ConsoleParameters.Height || y < 0)
            {
                throw new ArgumentOutOfRangeException("Y - coordinate out of range.");
            }
            _y = y;
        }

        public Coordinates SetDisplacements(int xDisplacement, int yDisplacement)
        {
            X += xDisplacement;
            Y += yDisplacement;
            return this;
        }

    }
}
