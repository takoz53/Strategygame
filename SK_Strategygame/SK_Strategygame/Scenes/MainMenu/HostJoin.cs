using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib.Scenes;
using AGFXLib.Drawables;
using OpenTK;
using OpenTK.Input;

namespace SK_Strategygame.Scenes.MainMenu
{
    class HostJoin : Scene
    {
        Sprite HostSprite, JoinSprite;
        DrawManager dm;

        public HostJoin()
        {
            dm = new DrawManager();
        }
        public override void Draw(GameWindow gw)
        {
            dm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {
            if(key.Key == Key.H)
            {

            }
        }

        public override void OnKeyUp(KeyboardKeyEventArgs key) { }

        public override void OnMouseDown(MouseButtonEventArgs button) { }

        public override void OnMouseUp(MouseButtonEventArgs button) { }

        public override void Update() { }
    }
}
