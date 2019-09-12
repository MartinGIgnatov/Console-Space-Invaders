using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public static class Designs
    {
        private static string[] _hero = new string[2];

        /// <summary>
        /// Contains the symbols representing the hero based on its parameters.
        /// </summary>
        /// <param name="level">The level of the hero.</param>
        /// <param name="engineLevel">The engine level of the hero.</param>
        /// <returns>Returns an array containing ths symbols.</returns>
        public static string[] GetHero(int level,int engineLevel)
        {
            switch (level)
            {
                case 1:
                    _hero[0] = Encoding.ASCII.GetString(new byte[] { 60,0,124,0,62 });
                    break;
                case 2:
                    _hero[0] = Encoding.ASCII.GetString(new byte[] { 60, 124, 0, 124, 62 });
                    break;
                case 3:
                    _hero[0] = Encoding.ASCII.GetString(new byte[] { 60, 40, 124, 41, 62 });
                    break;
                case 4:
                    _hero[0] = Encoding.ASCII.GetString(new byte[] { 60, 124, 127, 124, 62 });
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Hero having out of range level.");
            }
            switch (engineLevel)
            {
                case 1:
                    _hero[1] = Encoding.ASCII.GetString(new byte[] { 0, 0, 94, 0, 0 });
                    break;
                case 2:
                    _hero[1] = Encoding.ASCII.GetString(new byte[] { 0, 47, 0, 92, 0 });
                    break;
                case 3:
                    _hero[1] = Encoding.ASCII.GetString(new byte[] { 0, 47, 94, 92, 0 });
                    break;
                case 4:
                    _hero[1] = Encoding.ASCII.GetString(new byte[] { 47, 47, 94, 92, 92 });
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Hero having out of range level.");
            }

            return _hero;
        }

        /// <summary>
        /// Contains the symbols representing an enemy.
        /// </summary>
        /// <returns>Returns a string of symbols.</returns>
        public static string GetEnemy()
        {
            return "█████";
        }
    }
}
