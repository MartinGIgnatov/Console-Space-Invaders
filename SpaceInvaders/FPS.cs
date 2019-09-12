using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    /// <summary>
    /// NOT USED, DOES NOT WORK.
    /// </summary>
    public static class FPS
    {
        private static int _fps;

        public static void Calculate(long waitTime)
        {
            if(-waitTime / 1000 + 1 / 60 != 0)
            {
                _fps = (int)(1000 / ( - waitTime + 1000/60 ));
            }
            else
            {
                _fps = 0;
            } 
 
        }

        public static int Get()
        {
            return _fps;
        }

    }
}
