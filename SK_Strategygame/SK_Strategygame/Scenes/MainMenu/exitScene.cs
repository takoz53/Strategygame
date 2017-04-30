using AGFXLib.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using AGFXLib.Drawables;
using SK_Strategygame.UI;

namespace SK_Strategygame.Scenes.MainMenu
{
    class exitScene : Scene
    {
        DrawManager dm;
        public bButton yesButton, noButton;
        public Sprite borderSprite, backgroundSprite; //textSprite;
        public exitScene()
        {
            //Add Border, Text, Yes, No Button, Background
        }

        public override void Draw(GameWindow gw)
        {
            dm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {
            dm.OnKeyDown(key);
        }

        public override void OnKeyUp(KeyboardKeyEventArgs key)
        {
            dm.OnKeyUp(key);
        }

        public override void OnMouseDown(MouseButtonEventArgs button)
        {
            dm.OnMouseDown(button);
        }

        public override void OnMouseUp(MouseButtonEventArgs button)
        {
            dm.OnMouseUp(button);
        }

        public override void Update()
        {
        }
    }
}
