using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public abstract class Ship
    {
        protected Coordinates _coordinates;
        protected Coordinates _oldCoordinates;
        protected Coordinates _relativeBoundry;
        protected int _wantedPosition;

        public int Health { get; protected set; } = 1;

        public int Level { get; protected set; } = 1;

        public abstract void Display();

        public void TakeDamaged() => Health --;

        public void TakeDamage(int damage) => Health -= damage;

        public Coordinates GetPosition() => _coordinates;

        public Coordinates GetRelativeBoundry() => _relativeBoundry;

        public abstract void Clear();

    }
}
