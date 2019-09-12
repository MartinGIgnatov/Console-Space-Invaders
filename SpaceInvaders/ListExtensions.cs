using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpaceInvaders
{
    public static class ListExtensions
    {
        /// <summary>
        /// Finds the first projectile that hits the object if there is one.
        /// </summary>
        /// <param name="projectiles">The list of existant on the board projectiles.</param>
        /// <param name="coordinates">The coordinates of the top left border of the object.</param>
        /// <param name="relativeBoundry">The relative coordinates to the top left coordinates of the object.</param>
        /// <returns>Returns true if there is a hit, false if not.</returns>
        public static bool ProjectileHitList(this List<Projectile> projectiles, Coordinates coordinates, Coordinates relativeBoundry)
        {
            ///<summary>Takes a List of prejectiles and gives back bool if any of them cover the given object</summary>
            bool check = false;

            for (int i = 0; i < projectiles.Count; i++)
            {
                if (IfHit(projectiles[i], coordinates, relativeBoundry))
                {
                    check = true;
                    projectiles[i].ForceClear();
                    projectiles.RemoveAt(i);
                    break;
                }

            }

            return check;

        }

        /// <summary>
        /// Finds the first projectile that hits the object if there is one.
        /// </summary>
        /// <param name="projectiles">The list of existant on the board projectiles.</param>
        /// <param name="coordinates">The coordinates of the top left border of the object.</param>
        /// <param name="relativeBoundry">The relative coordinates to the top left coordinates of the object.</param>
        /// <param name="currType">The type of the projectile that first hits.</param>
        /// <returns>Returns true if there is a hit, false if not.</returns>
        public static bool ProjectileHitList(this List<Projectile> projectiles, Coordinates coordinates, Coordinates relativeBoundry, out ProjectileTypes currType)
        {
            ///<summary>Takes a List of prejectiles and gives back bool if any of them cover the given object</summary>
            bool check = false;

            currType = ProjectileTypes.EnemyProjectile;

            for (int i = 0; i < projectiles.Count; i++)
            {
                if (IfHit(projectiles[i], coordinates, relativeBoundry))
                {
                    check = true;
                    projectiles[i].ForceClear();
                    currType = projectiles[i].MyType;
                    projectiles.RemoveAt(i);
                    break;
                }

            }

            return check;

        }

        private static bool IfHit(Projectile projectile, Coordinates coordinates, Coordinates relativeBoundry)
        {
            int startXCoordinate = coordinates.X;
            int startYCoordinate = coordinates.Y;

            int endXCoordinate = coordinates.X + relativeBoundry.X;
            int endYCoordinate = coordinates.Y + relativeBoundry.Y;

            return (projectile.Position.X >= startXCoordinate
                        && (projectile.MyType != ProjectileTypes.HeroProjectile
                            ? projectile.Position.Y >= startYCoordinate //this line allows bullet teleportation
                            : true
                            )
                        && projectile.Position.X <= endXCoordinate
                        && (projectile.MyType != ProjectileTypes.LevelUp && projectile.MyType != ProjectileTypes.LifeBonus
                            ? projectile.Position.Y <= endYCoordinate
                            : true
                            )
                    )
                    ;
        }

    }
}
