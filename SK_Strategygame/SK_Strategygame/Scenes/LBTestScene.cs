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
            listbox_01.Add("Teach");
            listbox_01.Add("Kill Yourself");
            listbox_01.Add("More testing");
            listbox_01.Add("A lot of entries. Wow so cool hehe.");
            dm.Add(listbox_01);
        }

        public override void Draw(GameWindow gw)
        {
            dm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(MouseButtonEventArgs button) { }
        public override void OnMouseUp(MouseButtonEventArgs button) { }
        public override void Update() { }
    }
}
