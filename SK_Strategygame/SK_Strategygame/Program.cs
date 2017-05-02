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
        static void Main(string[] args)
        {
            aw = new AlphaWindow(1680,1050);
            aw.scene = new Scenes.MainMenuScene();
            aw.Run(60);
        }
    }
}
