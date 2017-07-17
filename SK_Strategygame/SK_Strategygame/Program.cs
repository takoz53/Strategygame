using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib;
using OpenTK;

namespace SK_Strategygame
{
    class Program
    {
        public static AlphaWindow aw;
        public const int ScreenWidth = 1680;
        public const int ScreenHeight = 1050;
        public static DateTime ProgramStartTime;
        public static DateTime CTimer;
        public static int FramesPassed = 0;
        public static Random rd = new Random();
        public static void CalculateFPS () // call this in draw function and it will print fps in console.
        {
            double dt = DateTime.Now.Subtract(CTimer).TotalSeconds;
            if (dt >= 1)
            {
                Console.WriteLine("FPS: " + (Math.Floor(FramesPassed / dt * 100) / 100));
                CTimer = DateTime.Now;
                FramesPassed = 0;
            }
            FramesPassed++;
        }

        static void Main(string[] args)
        {
            ProgramStartTime = DateTime.Now;
            CTimer = DateTime.Now;
            aw = new AlphaWindow(1680,1050);
            aw.scene = new Scenes.MainMenuScene();
            aw.Run(60);
        }
    }
}
