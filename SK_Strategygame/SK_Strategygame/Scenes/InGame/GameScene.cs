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
        bool Drag;
        float startX;
        float startY;
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

            user[index].x = UserMouse.getX() - 10; // -10 because of delay
            user[index].y = UserMouse.getY() - 10; // from the picture
        }
        private void handleDrag()
        {
            int player = 4;
            Drag = false;

            while (isBeingHeld)
            {
                try
                {
                    if (Drag == false)
                    {
                        if (user[0].isHovered())
                        {
                            player = 0;
                            startX = user[player].x;
                            startY = user[player].y;
                        }
                        else if (user[1].isHovered())
                        {
                            player = 1;
                            startX = user[player].x;
                            startY = user[player].y;
                        }
                        else if (user[2].isHovered())
                        {
                            player = 2;
                            startX = user[player].x;
                            startY = user[player].y;
                        }
                        else if (user[3].isHovered())
                        {
                            player = 3;
                            startX = user[player].x;
                            startY = user[player].y;
                        }
                        else { /*Do Nothing, non of them were clicked*/}
                    }
                    else
                    {
                        handleDragUser(player);
                    }


                    if ((startX + user[player].w + user[player].w - 23 >= user[player].x && startY + user[player].h + user[player].h - 23 >= user[player].y) || (startX - user[player].w + 23/*links und oben*/<= user[player].x && startY - user[player].h + 23 <= user[player].y))
                    {
                        if (startX + user[player].w - 23/*<--Breite des commanders*/>= user[player].x && startY + user[player].h - 23/*<--Höhe des commanders*/>= user[player].y)
                        {
                            switch (player)
                            {
                                case 0:
                                    Drag = true;
                                    handleDragUser(player);
                                    break;
                                case 1:
                                    Drag = true;
                                    handleDragUser(player);
                                    break;
                                case 2:
                                    Drag = true;
                                    handleDragUser(player);
                                    break;
                                case 3:
                                    Drag = true;
                                    handleDragUser(player);
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            dragThread.Interrupt(); //oder player = 4
                        }
                    }
                    else
                    {
                        dragThread.Interrupt(); //oder player = 4
                    }
                    
                    
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
            Drag = false;
            dragThread.Interrupt();
        }
        public override void Update() { }
    }
}
