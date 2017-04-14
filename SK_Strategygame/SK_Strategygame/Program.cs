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
            aw = new AlphaWindow(800, 600);
            aw.scene = new Scenes.LBTestScene();
            aw.Run(60);
        }
    }
}
