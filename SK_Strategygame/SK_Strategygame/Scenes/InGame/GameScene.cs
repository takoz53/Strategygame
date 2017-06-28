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
using SK_Strategygame.Gameplay.Field_Creation;

namespace SK_Strategygame.Scenes.InGame
{
    class GameScene : Scene
    {
        DrawManager dm;
        bCursor cursor;
        List<Field> gameField;
        int gameFieldWidth;
        int gameFieldHeight;

        public GameScene()
        {
            dm = new DrawManager();
            PlayField pf = new Gameplay.Field_Creation.PlayField(10);
            gameField = pf.getPlayField();

            gameFieldWidth = (gameField[gameField.Count - 1].getCoordinate().getX() + 1) * 200;
            gameFieldHeight = (gameField[gameField.Count - 1].getCoordinate().getY() + 1) * 200;


            foreach (Field f in gameField)
            {
                dm.Add(f);
            }

            cursor = new bCursor();

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
