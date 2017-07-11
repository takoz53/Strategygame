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
using AGFXLib;

namespace SK_Strategygame.Scenes.InGame
{
    class GameScene : Scene
    {
        DrawManager dm;
        bCursor cursor;
        List<Field> gameField;
        List<Player> user;
        float gameFieldWidth;
        float gameFieldHeight;
        public GameScene()
        {
            dm = new DrawManager();
            PlayField pf = new Gameplay.Field_Creation.PlayField(10);
            Users users = new Users(3);
            cursor = new bCursor();

            gameField = pf.getPlayField();
            user = users.getPlayers();

            gameFieldWidth = (gameField[gameField.Count - 1].getCoordinate().getX() + 1) * 250;
            gameFieldHeight = (gameField[gameField.Count - 1].getCoordinate().getY() + 1) * 250;


            foreach (Field f in gameField)
            {
                dm.Add(f);
            }
            foreach (Player p in user)
            {
                dm.Add(p);
            }

            dm.Add(cursor);

            
        }

        public override void Draw(GameWindow gw)
        {
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
        public override void OnMouseDown(MouseButtonEventArgs button)
        {
            float mouseposX = UserMouse.getX();
            //user[0].move(new Coordinate(UserMouse.getX(), UserMouse.getY()), user[0].setLocationX(mouseposX), user[0].setLocationY(UserMouse.getY()));
            Console.WriteLine("Holding mouse down");
            Console.WriteLine(user[0].getCoordinate().getX() + "" + user[0].getCoordinate().getY()); 
        }
        public override void OnMouseUp(MouseButtonEventArgs button) { }
        public override void Update() { }
    }
}
