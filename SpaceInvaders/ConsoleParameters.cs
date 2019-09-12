using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SpaceInvaders
{
    public static class ConsoleParameters
    {

        //These are the requirements for fullscreen
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int MAXIMIZE = 3;

        public static int Width { get; private set; }

        public static int Height { get; private set; }

        /// <summary>
        /// Sets the console to fullscreen and makes the buffer area same as the window area.
        /// </summary>
        public static void Set()
        {
            Width = Console.LargestWindowWidth;
            Height = Console.LargestWindowHeight;

            ShowWindow(ThisConsole, MAXIMIZE);

            if (Height < 50 || Width < 67)
            {
                throw new ArgumentOutOfRangeException("Size of the monitor you have is too small, sorry!?!");
            }

            Console.TreatControlCAsInput = true;//prevents using CTRL + C

            Console.BufferWidth = Width;
            Console.WindowWidth = Width;

            Console.BufferHeight = Height;
            Console.WindowHeight = Height;

            Console.CursorVisible = false;
            Console.Title = "Space Invaders";

        }
    }
}
