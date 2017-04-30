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
    class LBTestScene : Scene
    {
        public DrawManager dm = new DrawManager();
        public bListbox listbox_01 = new bListbox();
        public LBTestScene ()
        {
            listbox_01 = new bListbox();
            bListboxEntry b1 = new bListboxEntry("Hello World");
            b1.OnClick += HandleClick;
            listbox_01.Add(b1);
            dm.Add(listbox_01);
        }

        public void HandleClick (object s, MouseArgs m)
        {
            Console.WriteLine("I was clicked");
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
        public override void Update() { }
    }
}
