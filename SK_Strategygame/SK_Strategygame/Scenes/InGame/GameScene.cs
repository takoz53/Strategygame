using AGFXLib.Scenes;
using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using SK_Strategygame.UI;
using SK_Strategygame.Gameplay.Field_Creation;
using SK_Strategygame.Domain.Player;
using AGFXLib;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        bool isBeingHold;
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

        //public void handleDrag()
        //{
        //    float mouseposX = UserMouse.getX();
        //    float mouseposY = UserMouse.getY();
        //    user[0].move(new Coordinate(UserMouse.getX(), UserMouse.getY()), user[0].setLocationX(mouseposX), user[0].setLocationY(UserMouse.getY()));
        //    user[0].x = mouseposX;
        //    user[0].y = mouseposY;
        //}

        //private Point firstPoint = new Point();
        //public void handleDrag(Player user)
        //{
        //    user.OnMouseDown += (ss, ee) =>
        //     {
        //         if (ee.Button == System.Windows.Forms.MouseButtons.Left)
        //         {
        //             firstPoint = Control.MousePosition;
        //         }
        //     };

        //    this.MouseMove += (ss, ee) =>
        //    {
        //        if (ee.Button == System.Windows.Forms.MouseButtons.Left)
        //        {
        //            Point temp = Control.MousePosition; // -- Create a temp point
        //            Point res = new Point(firstPoint.X - temp.X, firstPoint.Y - temp.Y);
        //            this.Location = new Point(this.Location.X - res.X, this.Location.Y - res.Y);
        //            firstPoint = temp; // -- upgrade first point
        //        }
        //    };
        //}

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
            //isBeingHold = true;
            float mouseposX = UserMouse.getX();
            float mouseposY = UserMouse.getY();
            Console.WriteLine("Holding mouse down");
            Console.WriteLine(user[0].getCoordinate().getX() + "/" + user[0].getCoordinate().getY());
            //while (isBeingHold == true)
            //{
                //Thread drag = new Thread(handleDrag(user[0]);
                
                user[0].move(new Coordinate(UserMouse.getX(), UserMouse.getY()), user[0].setLocationX(mouseposX), user[0].setLocationY(UserMouse.getY()));
                user[0].x = mouseposX;
                user[0].y = mouseposY;
            //}

        }
        public override void OnMouseUp(MouseButtonEventArgs button)
        {
            isBeingHold = false;
        }
        public override void Update() { }
    }
}
