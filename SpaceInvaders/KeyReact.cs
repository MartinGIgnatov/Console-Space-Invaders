using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SpaceInvaders
{
    public static class KeyReact
    {
        private static int _direction = 0;
        private static Stopwatch _stopedTime = new Stopwatch();

        public static bool IsPaused { get; private set; }

        public static bool WasPaused { get; set; } = false;

        public static long TimePaused { get; private set; }

        /// <summary>
        /// Determines the appropriate action by the given input.
        /// </summary>
        /// <param name="keyInfo">The key input.</param>
        /// <returns>Returns the direction of the the hero.</returns>
        public static int Input(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key.Equals(ConsoleKey.LeftArrow))
            {
                _direction = -1;
            }
            else if (keyInfo.Key.Equals(ConsoleKey.RightArrow))
            {
                _direction = 1;
            }
            else if (keyInfo.Key.Equals(ConsoleKey.Spacebar))
            {
                _direction = 0;
                IsPaused = true;
            }
            else
            {
                _direction = 0;
            }
            return _direction;
        }

        /// <summary>
        /// Check is pause hase been pressed and pauses the screen, if so.
        /// </summary>
        public static void Pause()
        {
            if (IsPaused)
            {
                _stopedTime.Start();
                Display.PrintPause();
                while (true)
                {
                    if (Console.ReadKey(true).Key.Equals(ConsoleKey.Spacebar) )
                    {
                        IsPaused = false;
                        break;
                    }
                }
                TimePaused = _stopedTime.ElapsedMilliseconds;
                _stopedTime.Reset();
                WasPaused = true;
            }
        }

    }
}
