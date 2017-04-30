using AGFXLib.Scenes;
using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace SK_Strategygame.Scenes.InGame
{
    class GameScene : Scene
    {
        Sprite testsprite, testspriteforest;
        DrawManager dm;
        public GameScene()
        {
            dm = new DrawManager();
            testsprite = new Sprite("Resources/InGame/Fields/infertile/Deserts/test.png");
            testspriteforest = new Sprite("Resources/InGame/Fields/fertile/Forests/largetest.png");
            testspriteforest.x = testsprite.w + 1;
            dm.Add(testsprite);
            dm.Add(testspriteforest);
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
