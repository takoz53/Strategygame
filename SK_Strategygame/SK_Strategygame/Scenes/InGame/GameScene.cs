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
        public static int pfSize;
        bool isBeingHeld;
        Thread dragThread;
        public GameScene()
        {
            dm = new DrawManager();
            PlayField pf = new Gameplay.Field_Creation.PlayField(5);
            pfSize = pf.getSize();
            Users users = new Users(4);
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

        private void handleDragUser(int index)
        {
            user[index].move(new Coordinate(UserMouse.getX(), UserMouse.getY()), user[index].setLocationX(UserMouse.getX() - 20), user[index].setLocationY(UserMouse.getY() - 20));
<<<<<<< HEAD
            user[index].x = UserMouse.getX()-10;
            user[index].y = UserMouse.getY()-10;
=======
            user[index].x = UserMouse.getX() - 10; // -10 because of delay
            user[index].y = UserMouse.getY() - 10; // from the picture
>>>>>>> 055edaeb8bdcc9e0372877e6cfaf276ecd2001e5
        }
        private void handleDrag()
        {
            while (isBeingHeld)
            {
                try
                {
                    if (user[0].isHovered())
                        handleDragUser(0);
                    else if (user[1].isHovered())
                        handleDragUser(1);
                    else if (user[2].isHovered())
                        handleDragUser(2);
                    else if (user[3].isHovered())
                        handleDragUser(3);
                    else { /*Do Nothing, non of them were clicked*/}
                }
                catch{ }
            }
        }

        public override void Draw(GameWindow gw)
        {
            dm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {
            if (key.Key == OpenTK.Input.Key.Escape)
                Environment.Exit(0);
        }

        public override void OnKeyUp(KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(MouseButtonEventArgs button)
        {
            dragThread = new Thread(handleDrag);
            dragThread.Start();
            isBeingHeld = true;
        }
        public override void OnMouseUp(MouseButtonEventArgs button)
        {
            isBeingHeld = false;
            dragThread.Interrupt();
        }
        public override void Update() { }
    }
}
