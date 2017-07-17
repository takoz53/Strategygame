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
        DrawManager dm, cursor_dm, gameInterface_dm;
        bCursor cursor;
        List<Field> gameField;
        List<Player> user;
        public static int pfSize;
        bool isBeingHeld;
        bool preprocessingComplete = false; // For assignment and calculations.
        float MouseOriginX = 0; // Record the position of the mouse when the click starts.
        float MouseOriginY = 0;
        float PlayerOriginX = 0; //keep track of this for offsets.
        float PlayerOriginY = 0;
        float scrollX = 0;
        float scrollY = 0;
        int userIDHeld = -1;
        bool DisableScrolling = false;
        int TurnID = 1;

        private const int TileSize = 250;
        private const int ScrollingEdgeSize = 80;       // x pixels at the edge of each side.
        private const float ScrollSpeedPerFrame = 15;   // in pixels.

        public GameScene()
        {
            dm = new DrawManager();
            dm.w = Program.ScreenWidth;
            dm.h = Program.ScreenHeight;
            cursor_dm = new DrawManager();
            cursor_dm.w = Program.ScreenHeight;
            cursor_dm.h = Program.ScreenHeight;
            gameInterface_dm = new DrawManager();
            gameInterface_dm.w = Program.ScreenWidth;
            gameInterface_dm.h = Program.ScreenHeight;
            PlayField pf = new Gameplay.Field_Creation.PlayField(10);
            pfSize = pf.getSize();
            Users users = new Users(4);
            cursor = new bCursor();
            gameField = pf.getPlayField();
            user = users.getPlayers();

            foreach (Field f in gameField)
            {
                dm.Add(f);
            }
            foreach (Player p in user)
            {
                dm.Add(p);
            }

            cursor_dm.Add(cursor);
        }

        private int CalculateUserTurn ()
        {
            return ((TurnID-1) % user.Count) + 1;
        }

        private float ClampScreenX (float x)
        {
            if (x <= 0) return 0;
            return Math.Min(GetTilemapWidth() - Program.ScreenWidth, x);
        }

        private float ClampScreenY (float y)
        {
            if (y <= 0) return 0;
            return Math.Min(GetTilemapHeight() - Program.ScreenHeight, y);
        }

        private void FocusOnUser ()
        {
            int WhichUser = CalculateUserTurn();
            float UserX = user[WhichUser-1].x;
            float UserY = user[WhichUser-1].y;
            float _scrollX = UserX - (Program.ScreenWidth / 2);
            float _scrollY = UserY - (Program.ScreenHeight / 2);
            float clampX = ClampScreenX(_scrollX);
            float clampY = ClampScreenY(_scrollY);
            scrollX = clampX;
            scrollY = clampY;
            dm.x = (-clampX);
            dm.y = (-clampY);
        }

        private void UserDragProcessor ()
        {
            if (!preprocessingComplete)
            {
                int index = 0;
                userIDHeld = -1;
                foreach (Player p in user)
                {
                    // Collision processing with mouse to make sure whether or not the user intends to drag the player
                    bool CheckX = ((MouseOriginX >= p.x) && (MouseOriginX <= p.x+p.w));
                    bool CheckY = ((MouseOriginY >= p.y) && (MouseOriginY <= p.y + p.h));
                    bool IsColliding = (CheckX && CheckY);

                    if (IsColliding)
                    {
                        if (index == CalculateUserTurn() - 1)
                        {
                            Console.WriteLine("User " + index + " being held!");
                            userIDHeld = index;
                            PlayerOriginX = p.x;
                            PlayerOriginY = p.y;
                            break;
                        }
                    }
                    index++;
                }
                preprocessingComplete = true;
            }
            if (userIDHeld >= 0)
            {
                DisableScrolling = true; // Disable camera movement while moving player.

                int PlayerTileXOriginal = (int)Math.Floor(PlayerOriginX / 250); // Rounds down always 0.5 = 0
                int PlayerTileYOriginal = (int)Math.Floor(PlayerOriginY / 250); // This calculates current Tile Position of player.

                int MinX = PlayerTileXOriginal * 250 - 250;
                int MaxX = PlayerTileXOriginal * 250 + 250;
                int MinY = PlayerTileYOriginal * 250 - 250;
                int MaxY = PlayerTileYOriginal * 250 + 250;

                float PlayerOffsetX = (UserMouse.getX()+scrollX-MouseOriginX);
                float PlayerOffsetY = (UserMouse.getY()+scrollY-MouseOriginY);
                
                int DestinationX = (int)(PlayerOriginX + PlayerOffsetX + user[0].w);
                int DestinationY = (int)(PlayerOriginY + PlayerOffsetY + user[0].h);
                int DestinationTileX = (int)Math.Floor(DestinationX / 250f);

                int DestinationTileY = (int)Math.Floor(DestinationY / 250f);

                if ((DestinationTileY-PlayerTileYOriginal != 0 && DestinationTileX-PlayerTileXOriginal != 0) 
                    || Math.Abs(DestinationTileY-PlayerTileYOriginal) > 1
                    || Math.Abs(DestinationTileX-PlayerTileXOriginal) > 1) // No diagonal movement.
                {
                    // Invalid move. Set back position of player!
                    user[userIDHeld].x = PlayerOriginX;
                    user[userIDHeld].y = PlayerOriginY;
                } else // So if only one value changes, allow movement.
                {
                    float NewPlayerX = PlayerOriginX + PlayerOffsetX;
                    float NewPlayerY = PlayerOriginY + PlayerOffsetY;

                    user[userIDHeld].x = NewPlayerX;
                    user[userIDHeld].y = NewPlayerY;
                }
            }
        }

        private float GetTilemapWidth ()
        {
            return ((gameField[gameField.Count - 1].getCoordinate().getX() + 1) * TileSize);
        }

        private float GetTilemapHeight ()
        {
            return ((gameField[gameField.Count - 1].getCoordinate().getY() + 1) * TileSize);
        }

        private void ScrollingProcessor ()
        {
            cursor.SetCursor(bCursor.cursortype.scroll);
            float MouseX = UserMouse.getX();
            float MouseY = UserMouse.getY();
            bool CheckLeft = (MouseX >= 0 && MouseX <= ScrollingEdgeSize);
            bool CheckTop = (MouseY >= 0 && MouseY <= ScrollingEdgeSize);
            bool CheckRight = (MouseX >= Program.ScreenWidth - ScrollingEdgeSize);
            bool CheckBottom = (MouseY >= Program.ScreenHeight - ScrollingEdgeSize);

            if (CheckLeft && scrollX > 0)
                scrollX = Math.Max(0,scrollX-ScrollSpeedPerFrame);

            if (CheckTop && scrollY > 0)
                scrollY = Math.Max(0, scrollY - ScrollSpeedPerFrame);

            if (CheckRight && scrollX < GetTilemapWidth() - Program.ScreenWidth)
                scrollX = Math.Min(GetTilemapWidth() - Program.ScreenWidth, scrollX + ScrollSpeedPerFrame);

            if (CheckBottom && scrollY < GetTilemapHeight() - Program.ScreenHeight)
                scrollY = Math.Min(GetTilemapHeight() - Program.ScreenHeight, scrollY + ScrollSpeedPerFrame);

            if (CheckLeft || CheckTop || CheckRight || CheckBottom)
            {
                dm.x = -scrollX;
                dm.y = -scrollY;
            }
        }

        public override void Draw(GameWindow gw)
        {
            Program.CalculateFPS();
            if (!DisableScrolling)
            {
                ScrollingProcessor();
            }
            dm.Draw();
            cursor_dm.Draw();
            if (isBeingHeld)
            {
                UserDragProcessor();
                DisableScrolling = true;
            } else
            {
                DisableScrolling = false;
            }
            
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {
            if (key.Key == OpenTK.Input.Key.Space)
                FocusOnUser();
            if (key.Key == OpenTK.Input.Key.Escape)
                Environment.Exit(0);
        }

        public override void OnKeyUp(KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(MouseButtonEventArgs button)
        {
            isBeingHeld = true;
            MouseOriginX = UserMouse.getX()+scrollX;
            MouseOriginY = UserMouse.getY()+scrollY;
        }
        public override void OnMouseUp(MouseButtonEventArgs button)
        {
            TurnID++; //Debug
            isBeingHeld = false;
            preprocessingComplete = false;
        }
        public override void Update() { }
    }
}
