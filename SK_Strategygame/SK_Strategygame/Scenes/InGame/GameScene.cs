using AGFXLib.Scenes;
using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using SK_Strategygame.UI;

namespace SK_Strategygame.Scenes.InGame
{
    class GameScene : Scene
    {
        Sprite testsprite, testspriteforest;
        DrawManager dm;
        bCursor cursor;
        public GameScene()
        {
            dm = new DrawManager();
            testsprite = new Sprite("Resources/InGame/Fields/infertile/Deserts/test.png");
            testspriteforest = new Sprite("Resources/InGame/Fields/fertile/Forests/largetest.png");
            cursor = new bCursor();
            testspriteforest.x = testsprite.w;
            dm.Add(testsprite);
            dm.Add(testspriteforest);
            dm.Add(cursor);
        }

        public override void Draw(GameWindow gw)
        {
            dm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {

        }

        public override void OnKeyUp(KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(MouseButtonEventArgs button) { }
        public override void OnMouseUp(MouseButtonEventArgs button) { }
        public override void Update() { }
    }
}
