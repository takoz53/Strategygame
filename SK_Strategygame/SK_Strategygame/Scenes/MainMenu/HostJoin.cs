using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib.Scenes;
using OpenTK;
using OpenTK.Input;
using AGFXLib.Drawables;
using SK_Strategygame.UI;

namespace SK_Strategygame.Scenes
{
    class HostJoin : Scene
    {
        public Sprite borderSprite, BackgroundSprite, CursorSprite;
        public DrawManager dm;
        public bButton newGameSprite, optionsSprite, exitSprite;
        NotificationBox nb;
        public HostJoin()
        {
            // init code
            Console.WriteLine("Initializing MainMenuScene");
            dm = new DrawManager();
            nb = new NotificationBox();
            borderSprite = new Sprite("Resources/MainMenu/border.png");
            borderSprite.x = 1680 / 2 - borderSprite.width / 2;
            borderSprite.y = 180;
            dm.Add(borderSprite);
        }

        public override void Draw(GameWindow gw)
        {
            dm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {

        }

        public override void OnKeyUp(KeyboardKeyEventArgs key)
        {

        }

        public override void OnMouseDown(MouseButtonEventArgs button)
        {

        }

        public override void OnMouseUp(MouseButtonEventArgs button)
        {

        }

        public override void Update()
        {

        }
    }
}
