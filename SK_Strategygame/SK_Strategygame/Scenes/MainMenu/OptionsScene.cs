﻿using AGFXLib.Scenes;
using OpenTK;
using OpenTK.Input;
using AGFXLib.Drawables;

namespace SK_Strategygame.Scenes.MainMenu
{
    class OptionsScene : Scene
    {
        DrawManager dm;

        public OptionsScene()
        {
            // lmao
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
