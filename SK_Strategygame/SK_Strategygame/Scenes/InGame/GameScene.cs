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
using SK_Strategygame.Domain.Player;

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
            //Hier werden die Spielfelder und die Spielfiguren erstellt

            dm = new DrawManager();
            PlayField pf = new Gameplay.Field_Creation.PlayField(10);
            Playfigure player = new Playfigure(false, 25, 25);
            gameField = pf.getPlayField();

            //Berechnung der Breite und der höhe des Spielbretts
            gameFieldWidth = (gameField[gameField.Count - 1].getCoordinate().getX() + 1) * 250;
            gameFieldHeight = (gameField[gameField.Count - 1].getCoordinate().getY() + 1) * 250;

            //die layers des Spielbretts und die bereitstellung zum zeichnen
            foreach (Field f in gameField)
            {
                dm.Add(f);
            }

            dm.Add(player);

            cursor = new bCursor();

            dm.Add(cursor);

            
        }
        public override void Draw(GameWindow gw)
        {
            //Lern englisch
            dm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {
            if (key.Key == OpenTK.Input.Key.Escape)
            {
                Environment.Exit(0);
            }
        }

        public override void OnKeyUp(KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(MouseButtonEventArgs button) { }
        public override void OnMouseUp(MouseButtonEventArgs button) { }
        public override void Update() { }
    }
}
