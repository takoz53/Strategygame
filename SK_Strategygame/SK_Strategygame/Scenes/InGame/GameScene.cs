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
using SK_Strategygame.Domain.Fields;
using AGFXLib;

namespace SK_Strategygame.Scenes.InGame
{
    enum Team
    {
        red = 1,
        blue = 2,
        brown = 3,
        yellow = 4
    }
    class GameScene : Scene
    {
        DrawManager dm, cursor_dm, gameInterface_dm, player_dm;
        bCursor cursor;
        public static List<Field> gameField;
        List<Player> user;
        public static int pfSize;
        bool isBeingHeld;
        bool preprocessingComplete = false; // For assignment and calculations.
        float MouseOriginX = 0; // Record the position of the mouse when the click starts.
        float MouseOriginY = 0;
        float PlayerOriginX = 0; //keep track of this for offsets.
        float PlayerOriginY = 0;
        float LastPlayerMoveX = 0;
        float LastPlayerMoveY = 0;
        float scrollX = 0;
        float scrollY = 0;
        int userIDHeld = -1;
        bool DisableScrolling = false;
        bool AlreadyHarvested = false;
        bool FirstMoveCheck = false; // Fixes a bug.
        bool PendingRefresh = false;
        bool newTurn = true;
        bool inBazaar = false;
        int TurnID = 1;

        public List<Drawable> uiButtons = new List<Drawable>(); // We will only update this per different tile
        public List<Drawable> uiText = new List<Drawable>();  // We will update this per turn
        public List<Drawable> mapUI = new List<Drawable>(); // For highlights.
        public string LastUICheck = "";
        public int LastTurnCheck = 0;
        public int BazaarBoughtCount = 0;

        private const int TileSize = 250;
        private const int ScrollingEdgeSize = 80;       // x pixels at the edge of each side.
        private const float ScrollSpeedPerFrame = 15;   // in pixels.
        private const string TeamHighlight = "Resources/TeamHighlight/team_{0}.png"; // String.Format
        private const string CurrentTileHighlight = "Resources/InGame/Highlights/highlight_position.png";
        private const string MoveTileHighlight = "Resources/InGame/Highlights/highlight_move.png";

        public GameScene()
        {
            dm = new DrawManager();
            dm.w = Program.ScreenWidth;
            dm.h = Program.ScreenHeight;
            player_dm = new DrawManager();
            player_dm.w = Program.ScreenWidth;
            player_dm.h = Program.ScreenHeight;
            cursor_dm = new DrawManager();
            cursor_dm.w = Program.ScreenHeight;
            cursor_dm.h = Program.ScreenHeight;
            gameInterface_dm = new DrawManager();
            gameInterface_dm.w = Program.ScreenWidth;
            gameInterface_dm.h = Program.ScreenHeight;
            gameInterface_dm.x = 440;
            gameInterface_dm.y = 850;
            gameInterface_dm.Add(new Sprite("Resources/InGame/UIBottom/UIContainer.png", 0, 0));
            PlayField pf = new PlayField(10);
            pfSize = pf.getSize();
            cursor = new bCursor();
            gameField = pf.getPlayField();
            Users users = new Users(4);
            int Team = 1;
            foreach (Player p in users.getPlayers())
            {
                int TileX = (int)Math.Floor(p.x / 250f);
                int TileY = (int)Math.Floor(p.y / 250f);
                Conquer(new Vertex2(TileX, TileY), Team);
                Team++;
            }
            user = users.getPlayers();

            foreach (Field f in gameField)
            {
                dm.Add(f);
            }
            foreach (Player p in user)
            {
                player_dm.Add(p);
            }

            cursor_dm.Add(cursor);
            RefreshTurn();
            RefreshUIElements();
        }

        private void AccessBazaar(object s, MouseArgs e)
        {
            inBazaar = true;
            PendingRefresh = true;
        }

        private void ExitBazaar(object s, MouseArgs e)
        {
            inBazaar = false;
            PendingRefresh = true;
            
        }

        private void BuyWood (object s, MouseArgs e)
        {
            
            int WhichUser = CalculateUserTurn();
            int AmountOfGold = user[WhichUser - 1].getCurrentMoney();
            if (AmountOfGold >= 100) // arbitrary amount
            {
                user[WhichUser - 1].decreaseMoneyAmount(100);
                user[WhichUser - 1].increaseWoodAmount(100);
                BazaarBoughtCount++;
                PendingRefresh = true;
            } else
            {
                NotificationBox bx = new NotificationBox();
                bx.Notify("Oops!", "Not enough money.", NotificationBox.types.OKOnly);
            }
        }

        private void BuyStone(object s, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            int AmountOfGold = user[WhichUser - 1].getCurrentMoney();
            if (AmountOfGold >= 100) // arbitrary amount
            {
                user[WhichUser - 1].decreaseMoneyAmount(100);
                user[WhichUser - 1].increaseStoneAmount(100);
                BazaarBoughtCount++;
                PendingRefresh = true;
            }
            else
            {
                NotificationBox bx = new NotificationBox();
                bx.Notify("Oops!", "Not enough money.", NotificationBox.types.OKOnly);

            }
        }

        private void BuyFood(object s, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            int AmountOfGold = user[WhichUser - 1].getCurrentMoney();
            if (AmountOfGold >= 100) // arbitrary amount
            {
                user[WhichUser - 1].decreaseMoneyAmount(100);
                user[WhichUser - 1].increaseFoodAmount(100);
                BazaarBoughtCount++;
                PendingRefresh = true;
            }
            else
            {
                NotificationBox bx = new NotificationBox();
                bx.Notify("Oops!", "Not enough money.", NotificationBox.types.OKOnly);
            }
        }

        private void HarvestWood(object sender, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            Field fieldObj = null;
            foreach (Field f in gameField)
            {
                Vertex2 FieldCoord = f.getCoordinate();
                if (FieldCoord.x == CurrentPosition.x && FieldCoord.y == CurrentPosition.y)
                {
                    fieldObj = f;
                    break;
                }
            }
            if (fieldObj != null)
            {
                if (fieldObj.Wood <= 50)
                {
                    int amountGained = fieldObj.Wood;
                    fieldObj.Wood = 0;
                    user[WhichUser - 1].increaseWoodAmount(amountGained);
                } else
                {
                    fieldObj.Wood -= 50;
                    user[WhichUser - 1].increaseWoodAmount(50);
                }
                AlreadyHarvested = true;
                PendingRefresh = true;
            }
        }
        private void HarvestFood(object sender, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            Field fieldObj = null;
            foreach (Field f in gameField)
            {
                Vertex2 FieldCoord = f.getCoordinate();
                if (FieldCoord.x == CurrentPosition.x && FieldCoord.y == CurrentPosition.y)
                {
                    fieldObj = f;
                    break;
                }
            }
            if (fieldObj != null)
            {
                if (fieldObj.Food <= 50)
                {
                    int amountGained = fieldObj.Food;
                    fieldObj.Food = 0;
                    user[WhichUser - 1].increaseFoodAmount(amountGained);
                }
                else
                {
                    fieldObj.Food -= 50;
                    user[WhichUser - 1].increaseFoodAmount(50);
                }
                AlreadyHarvested = true;
                PendingRefresh = true;
            }
        }
        private void HarvestStone(object sender, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            Field fieldObj = null;
            foreach (Field f in gameField)
            {
                Vertex2 FieldCoord = f.getCoordinate();
                if (FieldCoord.x == CurrentPosition.x && FieldCoord.y == CurrentPosition.y)
                {
                    fieldObj = f;
                    break;
                }
            }
            if (fieldObj != null)
            {
                if (fieldObj.Stone <= 50)
                {
                    int amountGained = fieldObj.Stone;
                    fieldObj.Stone = 0;
                    user[WhichUser - 1].increaseStoneAmount(amountGained);
                }
                else
                {
                    fieldObj.Stone -= 50;
                    user[WhichUser - 1].increaseStoneAmount(50);
                }
                AlreadyHarvested = true;
                PendingRefresh = true;
            }
        }

        private FieldType GetFieldType (Vertex2 position)
        {
            foreach (Field f in gameField)
            {
                if ((float)f.getCoordinate().x == position.x && (float)f.getCoordinate().y == position.y)
                    return f.fieldType;
            }
            return FieldType.Sea;
        }
        private void Conquer(Vertex2 position, int TeamID)
        {
            int index = -1;
            int found = -1;
            foreach (Field f in gameField)
            {
                index++;
                if ((float)f.getCoordinate().x == position.x && (float)f.getCoordinate().y == position.y)
                {
                    found = index;
                    break;
                }
            }
            if (found != -1)
            {
                gameField[found].Team = TeamID;
            }
        }
        private void RefreshTurn ()
        {
            int WhichUser = CalculateUserTurn();
            userIDHeld = WhichUser - 1;
            PlayerOriginX = user[userIDHeld].x;
            PlayerOriginY = user[userIDHeld].y;
            LastPlayerMoveX = PlayerOriginX;
            LastPlayerMoveY = PlayerOriginY;
        }
        private void RefreshUIElements ()
        {
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            FieldType fieldType = FieldType.Sea;
            Field fieldObj = null;
            foreach (Field f in gameField)
            {
                Vertex2 FieldCoord = f.getCoordinate();
                if (FieldCoord.x == CurrentPosition.x && FieldCoord.y == CurrentPosition.y)
                {
                    fieldType = f.fieldType;
                    fieldObj = f;
                    break;
                }
            }
            if (true)
            {
                // We need to redo u.i. stuff.
                foreach (Drawable d in uiButtons)
                {
                    gameInterface_dm.drawables.Remove(d);
                }
                uiButtons = new List<Drawable>();
                switch (fieldType)
                {
                    case FieldType.Mountain:
                        if (fieldObj.Stone > 0 && !AlreadyHarvested)
                        {
                            bButton harvestStone = new bButton(ButtonType.GetPath(ButtonType.HarvestStone), ButtonType.GetPath(ButtonType.HarvestStone, true));
                            harvestStone.x = 800 - 164 - 40;
                            harvestStone.y = 40; // ui
                            harvestStone.OnClick += HarvestStone;
                            uiButtons.Add(harvestStone);
                        } else
                        {
                            Sprite disabledStone = new Sprite(ButtonType.GetPath(ButtonType.HarvestStone, false, true), 800 - 164 - 40, 40);
                            uiButtons.Add(disabledStone);
                        }
                        break;
                    case FieldType.Forest:
                        if (fieldObj.Wood > 0 && !AlreadyHarvested)
                        {
                            bButton harvestWood = new bButton(ButtonType.GetPath(ButtonType.HarvestWood), ButtonType.GetPath(ButtonType.HarvestWood, true));
                            harvestWood.x = 800 - 164 - 40;
                            harvestWood.y = 40; // ui
                            harvestWood.OnClick += HarvestWood;
                            uiButtons.Add(harvestWood);
                        } else
                        {
                            Sprite disabledWood = new Sprite(ButtonType.GetPath(ButtonType.HarvestWood, false, true), 800 - 164 - 40, 40);
                            uiButtons.Add(disabledWood);
                        }
                        break;
                    case FieldType.Pasture:
                        if (fieldObj.Food > 0 && !AlreadyHarvested)
                        {
                            bButton harvestFood = new bButton(ButtonType.GetPath(ButtonType.HarvestFood), ButtonType.GetPath(ButtonType.HarvestFood, true));
                            harvestFood.x = 800 - 164 - 40;
                            harvestFood.y = 40; // ui
                            harvestFood.OnClick += HarvestFood;
                            uiButtons.Add(harvestFood);
                        } else
                        {
                            Sprite disabledFood = new Sprite(ButtonType.GetPath(ButtonType.HarvestFood, false, true), 800 - 164 - 40, 40);
                            uiButtons.Add(disabledFood);
                        }
                        break;
                    case FieldType.Desert:
                        if (!inBazaar) {
                            bButton bazaarAccess = new bButton(ButtonType.GetPath(ButtonType.Bazaar_Access, false), ButtonType.GetPath(ButtonType.Bazaar_Access, true));
                            bazaarAccess.x = 800 - 164 - 40; // 800 edge of the ui container, 164 is the width of the button to make sure it fits, - 40 = padding.
                            bazaarAccess.y = 40;
                            bazaarAccess.OnClick += AccessBazaar;
                            uiButtons.Add(bazaarAccess);
                        } else
                        {
                            Sprite buyStone;
                            Sprite buyFood;
                            Sprite buyWood;
                            if (BazaarBoughtCount < 2) // bButton extends Clickable. so is also a clickable. polymorphism for convenience.
                            {
                                buyStone = new bButton(ButtonType.GetPath(ButtonType.Bazaar_BuyStone, false), ButtonType.GetPath(ButtonType.Bazaar_BuyStone, true),800-164-40,40);
                                buyFood = new bButton(ButtonType.GetPath(ButtonType.Bazaar_BuyFood, false), ButtonType.GetPath(ButtonType.Bazaar_BuyFood, true),800-164-40,80);
                                buyWood = new bButton(ButtonType.GetPath(ButtonType.Bazaar_BuyWood, false), ButtonType.GetPath(ButtonType.Bazaar_BuyWood, true),800-164-40,120);
                            } else
                            {
                                buyStone = new Sprite(ButtonType.GetPath(ButtonType.Bazaar_BuyStone, false, true),800-164-40,40);
                                buyFood = new Sprite(ButtonType.GetPath(ButtonType.Bazaar_BuyFood, false, true), 800 - 164 - 40, 80);
                                buyWood = new Sprite(ButtonType.GetPath(ButtonType.Bazaar_BuyWood, false, true), 800 - 164 - 40, 120);
                            }
                            
                            bButton exitBazaar = new bButton(ButtonType.GetPath(ButtonType.Bazaar_Exit, false), ButtonType.GetPath(ButtonType.Bazaar_Exit, true));
                            buyStone.x = 800 - 164 - 40;
                            buyStone.y = 40;
                            buyFood.x = 800 - 164 - 40;
                            buyFood.y = 80;
                            buyWood.x = 800 - 164 - 40;
                            buyWood.y = 120;
                            exitBazaar.x = 800 - 164 - 40;
                            exitBazaar.y = 160;
                            exitBazaar.OnClick += ExitBazaar;
                            buyStone.OnClick += BuyStone;
                            buyFood.OnClick += BuyFood;
                            buyWood.OnClick += BuyWood;
                            uiButtons.Add(exitBazaar);
                            uiButtons.Add(buyWood);
                            uiButtons.Add(buyStone);
                            uiButtons.Add(buyFood);
                        }
                        break;
                    default:
                        // No buttons.
                        break;
                }
                foreach (Drawable d in uiButtons) // The new buttons are added back.
                {
                    gameInterface_dm.drawables.Add(d);
                }
            }
            if (true)
            {
                foreach (Drawable d in uiText)
                {
                    gameInterface_dm.drawables.Remove(d);
                }
                uiText = new List<Drawable>();
                Text turnNumberText = new Text("Turn: " + TurnID);
                Text playerTurnText = new Text("Player " + CalculateUserTurn() + "'s (" + ((Team)CalculateUserTurn()).ToString() + ") turn!");
                Text resourceText = null;
                switch (fieldType)
                {
                    case FieldType.Forest:
                        // Wood
                        if (fieldObj != null)
                            resourceText = new Text(fieldObj.Wood + " wood left in field");
                        break;
                    case FieldType.Pasture:
                        if (fieldObj != null)
                            resourceText = new Text(fieldObj.Food + " food left in field");
                        break;
                    case FieldType.Mountain:
                        if (fieldObj != null)
                            resourceText = new Text(fieldObj.Stone + " stone left in field");
                        break;
                    default:
                        break;
                }
                turnNumberText.x = 40;
                turnNumberText.y = 20;
                playerTurnText.x = 40;
                playerTurnText.y = 40;
                if (resourceText != null)
                {
                    resourceText.x = 40;
                    resourceText.y = 60;
                }
                uiText.Add(turnNumberText);
                uiText.Add(playerTurnText);
                if (resourceText != null)
                    uiText.Add(resourceText);
                foreach (Drawable d in uiText)
                {
                    gameInterface_dm.drawables.Add(d);
                }
                // Map highlights
                if (newTurn)
                {
                    foreach (Drawable d in mapUI)
                    {
                        dm.drawables.Remove(d);
                    }
                    mapUI = new List<Drawable>();
                    bool Up = (CurrentPosition.y > 0);
                    bool Down = (CurrentPosition.y < pfSize - 1);
                    bool Left = (CurrentPosition.x > 0);
                    bool Right = (CurrentPosition.x < pfSize - 1);
                    Vertex2 cpos = new Vertex2(CurrentPosition.x * 250f, CurrentPosition.y * 250f);
                    Vertex2 up_pos = new Vertex2(cpos.x, cpos.y - 250f);
                    Vertex2 down_pos = new Vertex2(cpos.x, cpos.y + 250f);
                    Vertex2 left_pos = new Vertex2(cpos.x - 250f, cpos.y);
                    Vertex2 right_pos = new Vertex2(cpos.x + 250f, cpos.y);
                    Sprite cpos_s = new Sprite(CurrentTileHighlight, (float)cpos.x, (float)cpos.y);
                    mapUI.Add(cpos_s);
                    if (Up && GetFieldType(new Vertex2((float)Math.Floor(up_pos.x / 250f), (float)Math.Floor(up_pos.y / 250f))) != FieldType.Sea)
                    {
                        Sprite UpSprite = new Sprite(MoveTileHighlight, (float)up_pos.x, (float)up_pos.y);
                        mapUI.Add(UpSprite);
                    }
                    if (Right && GetFieldType(new Vertex2((float)Math.Floor(right_pos.x / 250f), (float)Math.Floor(right_pos.y / 250f))) != FieldType.Sea)
                    {
                        Sprite RightSprite = new Sprite(MoveTileHighlight, (float)right_pos.x, (float)right_pos.y);
                        mapUI.Add(RightSprite);
                    }
                    if (Left && GetFieldType(new Vertex2((float)Math.Floor(left_pos.x / 250f), (float)Math.Floor(left_pos.y / 250f))) != FieldType.Sea)
                    {
                        Sprite LeftSprite = new Sprite(MoveTileHighlight, (float)left_pos.x, (float)left_pos.y);
                        mapUI.Add(LeftSprite);
                    }
                    if (Down && GetFieldType(new Vertex2((float)Math.Floor(down_pos.x / 250f), (float)Math.Floor(down_pos.y / 250f))) != FieldType.Sea)
                    {
                        Sprite DownSprite = new Sprite(MoveTileHighlight, (float)down_pos.x, (float)down_pos.y);
                        mapUI.Add(DownSprite);
                    }
                    foreach (Field f in gameField)
                    {
                        if (f.Team != 0)
                        {
                            Sprite tSpriteColor = new Sprite(String.Format(TeamHighlight, ((Team)f.Team).ToString()), (float)f.x, (float)f.y);
                            mapUI.Add(tSpriteColor);
                        }
                    }
                    foreach (Drawable d in mapUI)
                    {
                        dm.Add(d);
                    }
                    newTurn = false;
                }
            }
            LastUICheck = fieldType.ToString();
            LastTurnCheck = TurnID;
        }
        private Vertex2 CalculateUserTilePosition (int id)
        {
            return new Vertex2(Math.Floor(user[id].x / 250f), Math.Floor(user[id].y / 250f));
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
                LastPlayerMoveX = 0;
                LastPlayerMoveY = 0;
                preprocessingComplete = true;
                FirstMoveCheck = false;
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

                float PlayerOffsetX = (UserMouse.getX()+scrollX-PlayerOriginX);
                float PlayerOffsetY = (UserMouse.getY()+scrollY-PlayerOriginY);
                
                int DestinationX_Low = (int)(PlayerOriginX + PlayerOffsetX);
                int DestinationY_Low = (int)(PlayerOriginY + PlayerOffsetY);

                int DestinationX_High = (int)(PlayerOriginX + PlayerOffsetX + user[0].w);
                int DestinationY_High = (int)(PlayerOriginY + PlayerOffsetY + user[0].h);

                int DestinationTileX = (int)Math.Floor(DestinationX_Low / 250f);
                int DestinationTileY = (int)Math.Floor(DestinationY_Low / 250f);

                int DestinationTileX_High = (int)Math.Floor(DestinationX_High / 250f);
                int DestinationTileY_High = (int)Math.Floor(DestinationY_High / 250f);

                if ((DestinationTileY-PlayerTileYOriginal != 0 && DestinationTileX-PlayerTileXOriginal != 0)
                    || (DestinationTileX_High-PlayerTileXOriginal != 0 && (DestinationTileY_High-PlayerTileYOriginal!=0 || DestinationTileY-PlayerTileYOriginal!=0))
                    || Math.Abs(DestinationTileY-PlayerTileYOriginal) > 1
                    || Math.Abs(DestinationTileX-PlayerTileXOriginal) > 1
                    || Math.Abs(DestinationTileY_High-PlayerTileYOriginal) > 1
                    || Math.Abs(DestinationTileX_High-PlayerTileXOriginal) > 1) // No diagonal movement.
                {
                    // Invalid move. Set back position of player!
                    if (FirstMoveCheck)
                    {
                        user[userIDHeld].x = LastPlayerMoveX;
                        user[userIDHeld].y = LastPlayerMoveY;
                    }
                } else // So if only one value changes, allow movement.
                {
                    float NewPlayerX = PlayerOriginX + PlayerOffsetX;
                    float NewPlayerY = PlayerOriginY + PlayerOffsetY;

                    LastPlayerMoveX = NewPlayerX;
                    LastPlayerMoveY = NewPlayerY;

                    user[userIDHeld].x = NewPlayerX;
                    user[userIDHeld].y = NewPlayerY;
                    FirstMoveCheck = true;
                    PendingRefresh = true;
                }
            }
        }
        private float GetTilemapWidth ()
        {
            return (((float)gameField[gameField.Count - 1].getCoordinate().x + 1) * TileSize);
        }
        private float GetTilemapHeight ()
        {
            return (((float)gameField[gameField.Count - 1].getCoordinate().y + 1) * TileSize);
        }
        private void ScrollingProcessor ()
        {
            
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
                cursor.SetCursor(bCursor.cursortype.scroll);
                dm.x = -scrollX;
                dm.y = -scrollY;
            } else
            {
                cursor.SetCursor(bCursor.cursortype.main);
            }
        }

        public override void Draw(GameWindow gw)
        {
            if (PendingRefresh)
            {
                RefreshUIElements();
                PendingRefresh = false;
            }

            Program.CalculateFPS();
            if (!DisableScrolling)
            {
                ScrollingProcessor();
            } else
            {
                cursor.SetCursor(bCursor.cursortype.main);
            }
            player_dm.x = dm.x;
            player_dm.y = dm.y;
            dm.Draw();
            player_dm.Draw();
            gameInterface_dm.Draw();
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
            gameInterface_dm.OnMouseDown(button);
            isBeingHeld = true;
            MouseOriginX = UserMouse.getX()+scrollX;
            MouseOriginY = UserMouse.getY()+scrollY;
        }
        public override void OnMouseUp(MouseButtonEventArgs button)
        {
            gameInterface_dm.OnMouseUp(button);
            isBeingHeld = false;
            preprocessingComplete = false;
        }
        public override void Update()
        {
            gameInterface_dm.Update();
        }
    }
}
