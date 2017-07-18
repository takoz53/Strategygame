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
        // Game Handlers
        public static int TilemapSize; // Size of the play field.
        public static List<Field> GameTilemap; // Tilemap data.
        public List<Player> PlayerList; // Player List
        public DrawManager MainDM, CursorDM, GameInterfaceDM, PlayerDM, ResourceDM;
        public bCursor Cursor; // Mouse Cursor object.

        // Game Mechanics
        public bool AlreadyHarvested = false; // If the user has already harvested this turn.
        public bool AlreadyUpgraded = false; // Only allow 1 upgrade per turn for walls
        public int BazaarBoughtCount = 0; // Make sure the user only buys X amount per turn.
        public int TurnID = 1; // Current Turn

        // U.I. Related
        public bool NewTurn = true; // U.I. refreshes some elements if it is.
        public bool InBazaar = false; // Change the button layout of Bazaar if so.
        public bool UIRefreshQueued = false; // Allows the code to queue a u.i. reload without causing errors.
        public bool ResourceRefreshQueued = false;
        public bool UserBeingHeld; // We can use this to determine whether or not to DisableScrolling.
        public bool PreprocComplete = false; // U.I. refresh calculations.
        public bool DisableScrolling = false; // To prevent scrolling when player is being moved.
        public bool FirstMoveCheck = false; // Fixes a bug related to player movement.
        public float MouseOriginX = 0; // Record the position of the mouse when the click starts.
        public float MouseOriginY = 0;
        public float PlayerOriginX = 0; // Allows us to track the current/previous player position to prevent excessive movement.
        public float PlayerOriginY = 0;
        public float LastPlayerMoveX = 0; // Allows us keep the current player position if they move invalidly. Makes it display smoother.
        public float LastPlayerMoveY = 0;
        public float ScrollX = 0; // The amount the tilemap is scrolled.
        public float ScrollY = 0;
        public int UserIDHeld = -1; // The user that's being dragged / moved.
        public List<Drawable> UIButtons = new List<Drawable>(); // We will only update this per different tile
        public List<Drawable> UIText = new List<Drawable>();  // We will update this per turn
        public List<Drawable> MapUI = new List<Drawable>(); // For highlights.
        public List<Drawable> UITop = new List<Drawable>();

        // Pre-initialized Sprites
        public Sprite Sprite_GoldIcon = new Sprite(GoldIcon, GoldIconPos, 0);
        public Sprite Sprite_WoodIcon = new Sprite(WoodIcon, WoodIconPos, 0);
        public Sprite Sprite_StoneIcon = new Sprite(StoneIcon, StoneIconPos, 0);
        public Sprite Sprite_FoodIcon = new Sprite(FoodIcon, FoodIconPos, 0);
        public Sprite Sprite_SoldierIcon = new Sprite(SoldierIcon, SoldierIconPos, 0);
        public Sprite Sprite_DisabledGold = new Sprite(ButtonType.GetPath(ButtonType.HarvestGold, false, true), ButtonStartPosition-184, ButtonYStartPosition);
        public Sprite Sprite_DisabledStone = new Sprite(ButtonType.GetPath(ButtonType.HarvestStone, false, true), ButtonStartPosition, ButtonYStartPosition);
        public Sprite Sprite_DisabledWood = new Sprite(ButtonType.GetPath(ButtonType.HarvestWood, false, true), ButtonStartPosition, ButtonYStartPosition);
        public Sprite Sprite_DisabledFood = new Sprite(ButtonType.GetPath(ButtonType.HarvestFood, false, true), ButtonStartPosition, ButtonYStartPosition);
        public Sprite Sprite_DisabledBuyStone = new Sprite(ButtonType.GetPath(ButtonType.Bazaar_BuyStone, false, true), ButtonStartPosition, ButtonYStartPosition);
        public Sprite Sprite_DisabledBuyWood = new Sprite(ButtonType.GetPath(ButtonType.Bazaar_BuyWood, false, true), ButtonStartPosition, ButtonYStartPosition + ButtonMarginY * 1);
        public Sprite Sprite_DisabledBuyFood = new Sprite(ButtonType.GetPath(ButtonType.Bazaar_BuyFood, false, true), ButtonStartPosition, ButtonYStartPosition+ButtonMarginY*2);
        public Sprite Sprite_DisabledCreateCity = new Sprite(ButtonType.GetPath(ButtonType.CreateCity, false, true), ButtonStartPosition, ButtonYStartPosition + ButtonMarginY);
        public Sprite Sprite_DisabledWallUpgrade = new Sprite(ButtonType.GetPath(ButtonType.UpgradeWall, false, true), ButtonStartPosition-184, ButtonYStartPosition);
        public Sprite Sprite_DisabledBarracks = new Sprite(ButtonType.GetPath(ButtonType.CreateBarracks, false, true), ButtonStartPosition, ButtonYStartPosition);
        public Sprite Sprite_DisabledSoldiers = new Sprite(ButtonType.GetPath(ButtonType.CreateSoldiers, false, true), ButtonStartPosition, ButtonYStartPosition + ButtonMarginY);
        public bButton Button_HarvestStone = new bButton(ButtonType.GetPath(ButtonType.HarvestStone), ButtonType.GetPath(ButtonType.HarvestStone, true));
        public bButton Button_HarvestWood = new bButton(ButtonType.GetPath(ButtonType.HarvestWood), ButtonType.GetPath(ButtonType.HarvestWood, true));
        public bButton Button_HarvestFood = new bButton(ButtonType.GetPath(ButtonType.HarvestFood), ButtonType.GetPath(ButtonType.HarvestFood, true));
        public bButton Button_HarvestGold = new bButton(ButtonType.GetPath(ButtonType.HarvestGold), ButtonType.GetPath(ButtonType.HarvestGold, true));
        public bButton Button_AccessBazaar = new bButton(ButtonType.GetPath(ButtonType.Bazaar_Access, false), ButtonType.GetPath(ButtonType.Bazaar_Access, true));
        public bButton Button_ExitBazaar = new bButton(ButtonType.GetPath(ButtonType.Bazaar_Exit, false), ButtonType.GetPath(ButtonType.Bazaar_Exit, true));
        public bButton Button_BuyStone = new bButton(ButtonType.GetPath(ButtonType.Bazaar_BuyStone, false), ButtonType.GetPath(ButtonType.Bazaar_BuyStone, true), ButtonStartPosition, ButtonYStartPosition);
        public bButton Button_BuyWood = new bButton(ButtonType.GetPath(ButtonType.Bazaar_BuyWood, false), ButtonType.GetPath(ButtonType.Bazaar_BuyWood, true), ButtonStartPosition, ButtonYStartPosition+ButtonMarginY);
        public bButton Button_BuyFood = new bButton(ButtonType.GetPath(ButtonType.Bazaar_BuyFood, false), ButtonType.GetPath(ButtonType.Bazaar_BuyFood, true), ButtonStartPosition, ButtonYStartPosition+ButtonMarginY*2);
        public bButton Button_NextRound = new bButton(ButtonType.GetPath(ButtonType.NextRound, false), ButtonType.GetPath(ButtonType.NextRound, true), ButtonStartPosition, NextRoundButtonY);
        public bButton Button_CreateCity = new bButton(ButtonType.GetPath(ButtonType.CreateCity, false), ButtonType.GetPath(ButtonType.CreateCity, true), ButtonStartPosition, ButtonYStartPosition + ButtonMarginY);
        public bButton Button_CreateBarracks = new bButton(ButtonType.GetPath(ButtonType.CreateBarracks, false), ButtonType.GetPath(ButtonType.CreateBarracks, true), ButtonStartPosition, ButtonYStartPosition + ButtonMarginY*1);
        public bButton Button_CreateWall = new bButton(ButtonType.GetPath(ButtonType.CreateWall, false), ButtonType.GetPath(ButtonType.CreateWall, true), 0, 0);
        public bButton Button_UpgradeWall = new bButton(ButtonType.GetPath(ButtonType.UpgradeWall, false), ButtonType.GetPath(ButtonType.UpgradeWall, true), 0, 0);
        public bButton Button_CreateSoldiers = new bButton(ButtonType.GetPath(ButtonType.CreateSoldiers, false), ButtonType.GetPath(ButtonType.CreateSoldiers, true), 0, 0);
        public bButton Button_MoveSoldiers = new bButton(ButtonType.GetPath(ButtonType.MoveSoldiers, false), ButtonType.GetPath(ButtonType.MoveSoldiers, true), 0, 0);
        // *** Configuration ***

        // Files
        private const string TeamHighlight = "Resources/TeamHighlight/team_{0}.png";
        private const string CurrentTileHighlight = "Resources/InGame/Highlights/highlight_position.png";
        private const string MoveTileHighlight = "Resources/InGame/Highlights/highlight_move.png";
        private const string UIContainer = "Resources/InGame/UIBottom/UIContainer.png";
        private const string GoldIcon = "Resources/InGame/UITop/money_icon.png";
        private const string WoodIcon = "Resources/InGame/UITop/wood_icon.png";
        private const string StoneIcon = "Resources/InGame/UITop/stone_icon.png";
        private const string FoodIcon = "Resources/InGame/UITop/food_icon.png";
        private const string SoldierIcon = "Resources/InGame/UITop/soldier_icon.png";
        private const string TopBG = "Resources/InGame/UITop/bg.png";

        // Game Mechanics
        private const int BuyAmount = 100; // So FoodCost / BuyAmount = Price Per Unit
        private const int FoodCost = 100;
        private const int StoneCost = 150;
        private const int WoodCost = 100;
        private const int NumberOfPlayers = 4;

        // Map
        private const int MapSize = 10; // Squared value. Width and Height of the map.
        private const float TileSize = 250f; // For math.

        // U.I. Top
        private const int GoldIconPos = 0;
        private const int WoodIconPos = 1680 / 5;
        private const int StoneIconPos = 1680 / 5 * 2;
        private const int FoodIconPos = 1680 / 5 * 3;
        private const int SoldierIconPos = 1680 / 5 * 4;
        private const int TextPaddingLeft = 42;

        // U.I.
        private const int ButtonStartPosition = 800 - 164 - 50; // Container Width - Button Width - 50 (padding)
        private const int ButtonYStartPosition = 30; // Start at y=40px from Container Height.
        private const int ButtonMarginY = 35; // Go down 40px for each button.
        private const int NextRoundButtonY = 100;
        private const int ScrollingEdgeSize = 50; // x pixels at the edge of each side.
        private const float ScrollSpeedPerFrame = 15;   // in pixels.

        private const int TextStartPosition = 60;
        private const int TextStartYPosition = 20;
        private const int TextMarginY = 25;
        private const string PlayerTurn = "Player {0}'s ({1}) Turn";
        private const string TurnNumber = "Turn: {0}";
        private const string ResourceRemaining = "{0} {1} left in field"; // i.e. 50 Wood left in field.

            // ** We did readonly static to avoid being caught by Expression must be constant.
        private readonly static string[] NotEnoughMoney = new string[] { "Not enough money!", "Oops" };
        private readonly static Vertex2 GameInterfaceStartPosition = new Vertex2(440, 850); // X , Y.

        // Initialization constructor.
        public GameScene()
        {
            // Draw Manager Creation and initialization.
            MainDM = new DrawManager();
            MainDM.w = Program.ScreenWidth; // We store this value so we can optimize sprite draw calling, i.e. Don't draw sprites Off-screen.
            MainDM.h = Program.ScreenHeight;
            PlayerDM = new DrawManager();
            PlayerDM.w = Program.ScreenWidth;
            PlayerDM.h = Program.ScreenHeight;
            CursorDM = new DrawManager();
            CursorDM.w = Program.ScreenHeight;
            CursorDM.h = Program.ScreenHeight;
            GameInterfaceDM = new DrawManager();
            GameInterfaceDM.w = Program.ScreenWidth;
            GameInterfaceDM.h = Program.ScreenHeight;
            GameInterfaceDM.x = (float)GameInterfaceStartPosition.x; // Position all U.I. elements to here.
            GameInterfaceDM.y = (float)GameInterfaceStartPosition.y;
            GameInterfaceDM.Add(new Sprite(UIContainer, 0, 0));
            ResourceDM = new DrawManager();
            ResourceDM.w = Program.ScreenWidth;
            ResourceDM.h = Program.ScreenHeight;
            ResourceDM.Add(new Sprite(TopBG, 0, 0));
            PlayField pf = new PlayField(MapSize); // Generate the map.
            TilemapSize = pf.getSize();
            Cursor = new bCursor(); // Mouse Cursor
            GameTilemap = pf.getPlayField();
            Users users = new Users(NumberOfPlayers);
            int Team = 1;
            foreach (Player p in users.getPlayers()) // Make the tile the player is sitting on their territory.
            {
                int TileX = (int)Math.Floor(p.x / TileSize);
                int TileY = (int)Math.Floor(p.y / TileSize);
                Conquer(new Vertex2(TileX, TileY), Team);
                MakeCity(new Vertex2(TileX, TileY), Team);
                Team++;
            }
            PlayerList = users.getPlayers(); // For external access.

            foreach (Field f in GameTilemap)
                MainDM.Add(f);
            foreach (Player p in PlayerList)
                PlayerDM.Add(p);

            ResourceDM.Add(Sprite_GoldIcon);
            ResourceDM.Add(Sprite_WoodIcon);
            ResourceDM.Add(Sprite_StoneIcon);
            ResourceDM.Add(Sprite_FoodIcon);
            ResourceDM.Add(Sprite_SoldierIcon);
            
            Button_HarvestStone.x = ButtonStartPosition;
            Button_HarvestStone.y = ButtonYStartPosition;
            Button_HarvestStone.OnClick += HarvestStone;
            Button_HarvestWood.x = ButtonStartPosition;
            Button_HarvestWood.y = ButtonYStartPosition;
            Button_HarvestWood.OnClick += HarvestWood;
            Button_HarvestFood.x = ButtonStartPosition;
            Button_HarvestFood.y = ButtonYStartPosition;
            Button_HarvestFood.OnClick += HarvestFood;
            Button_HarvestGold.x = ButtonStartPosition - 184;
            Button_HarvestGold.y = ButtonYStartPosition;
            Button_HarvestGold.OnClick += HarvestGold;
            Button_AccessBazaar.x = ButtonStartPosition;
            Button_AccessBazaar.y = ButtonYStartPosition;
            Button_AccessBazaar.OnClick += AccessBazaar;
            Button_ExitBazaar.x = ButtonStartPosition;
            Button_ExitBazaar.y = ButtonYStartPosition + ButtonMarginY * 3;
            Button_ExitBazaar.OnClick += ExitBazaar;
            Button_BuyStone.x = ButtonStartPosition;
            Button_BuyStone.y = ButtonYStartPosition;
            Button_BuyStone.OnClick += BuyStone;
            Button_BuyWood.x = ButtonStartPosition;
            Button_BuyWood.y = ButtonYStartPosition + ButtonMarginY;
            Button_BuyWood.OnClick += BuyWood;
            Button_BuyFood.x = ButtonStartPosition;
            Button_BuyFood.y = ButtonYStartPosition + ButtonMarginY * 2;
            Button_BuyFood.OnClick += BuyFood;
            Button_NextRound.x = ButtonStartPosition + 164 - 73;
            Button_NextRound.y = NextRoundButtonY;
            Button_NextRound.OnClick += NextRound;
            Button_CreateCity.OnClick += CreateCity;
            Button_CreateCity.x = ButtonStartPosition;
            Button_CreateCity.y = ButtonYStartPosition + ButtonMarginY;
            Button_CreateBarracks.x = ButtonStartPosition;
            Button_CreateBarracks.y = ButtonYStartPosition;
            Button_CreateBarracks.OnClick += CreateBarracks;
            Button_CreateWall.x = ButtonStartPosition - 184;
            Button_CreateWall.y = ButtonYStartPosition;
            Button_CreateWall.OnClick += CreateWall;
            Button_UpgradeWall.x = ButtonStartPosition - 184;
            Button_UpgradeWall.y = ButtonYStartPosition; // Same Y as Create because we'll swap buttons.
            Button_UpgradeWall.OnClick += UpgradeWall;
            Button_CreateSoldiers.x = ButtonStartPosition;
            Button_CreateSoldiers.y = ButtonYStartPosition + ButtonMarginY;
            Button_CreateSoldiers.OnClick += CreateSoldiers;
            Button_MoveSoldiers.x = ButtonStartPosition - 184;
            Button_MoveSoldiers.y = ButtonYStartPosition + ButtonMarginY;

            CursorDM.Add(Cursor);
            RefreshTurn();
            RefreshUIElements();
            RefreshResourceBar();
            RecalculateTilemapOccupancy();
        }

        // Button Callbacks
        private void MoveSoldiers (object s, MouseArgs e)
        {

        }

        private void CreateSoldiers (object s, MouseArgs e)
        {
            Field currentTile = GameTilemap[GetTileID(CalculateUserTilePosition(CalculateUserTurn() - 1))];
            int whichUser = CalculateUserTurn() - 1;
            if (true)
            {
                if (PlayerList[whichUser].getCurrentMoney() > 0)
                {
                    PlayerList[whichUser].decreaseMoneyAmount(1); // arbitrary
                    currentTile.Soldiers += Field.SoldierCreationRate;
                    UIRefreshQueued = true;
                    ResourceRefreshQueued = true;
                }
            }
        }
        
        private void CreateWall(object s, MouseArgs e)
        {
            Field currentTile = GameTilemap[GetTileID(CalculateUserTilePosition(CalculateUserTurn() - 1))];
            int whichUser = CalculateUserTurn()-1;
            if (currentTile.WallLevel == 0)
            {
                if (PlayerList[whichUser].getCurrentMoney() > 0)
                {
                    PlayerList[whichUser].decreaseMoneyAmount(1); // arbitrary
                    currentTile.WallLevel++;
                    currentTile.WallPoints += Field.WallUpgrade;
                    UIRefreshQueued = true;
                }
            }
        }

        private void CreateBarracks(object s, MouseArgs e)
        {
            Field currentTile = GameTilemap[GetTileID(CalculateUserTilePosition(CalculateUserTurn() - 1))];
            int whichUser = CalculateUserTurn() - 1;
            if (currentTile.BarracksBuilt == false)
            {
                if (PlayerList[whichUser].getCurrentMoney() > 0)
                {
                    PlayerList[whichUser].decreaseMoneyAmount(1); // arbitrary
                    currentTile.BarracksBuilt = true; // Barracks => Soldier Creation
                    UIRefreshQueued = true;
                }
            }
        }

        private void UpgradeWall(object s, MouseArgs e)
        {
            Field currentTile = GameTilemap[GetTileID(CalculateUserTilePosition(CalculateUserTurn() - 1))];
            int whichUser = CalculateUserTurn() - 1;
            if (currentTile.WallLevel < Field.MaxWallLevel)
            {
                if (PlayerList[whichUser].getCurrentMoney() > 0)
                {
                    PlayerList[whichUser].decreaseMoneyAmount(1); // arbitrary
                    currentTile.WallLevel++;
                    currentTile.WallPoints += Field.WallUpgrade;
                    UIRefreshQueued = true;
                }
            }
        }

        /// <summary>
        /// Callback for Access Bazaar button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void AccessBazaar(object s, MouseArgs e)
        {
            InBazaar = true;
            UIRefreshQueued = true;
        }
        /// <summary>
        /// Callback for Exit Bazaar button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void ExitBazaar(object s, MouseArgs e)
        {
            InBazaar = false;
            UIRefreshQueued = true;
        }
        /// <summary>
        /// Callback for Buy Wood button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void BuyWood (object s, MouseArgs e)
        {
            
            int WhichUser = CalculateUserTurn();
            int AmountOfGold = PlayerList[WhichUser - 1].getCurrentMoney();
            if (AmountOfGold >= WoodCost) // arbitrary amount
            {
                PlayerList[WhichUser - 1].decreaseMoneyAmount(WoodCost);
                PlayerList[WhichUser - 1].increaseWoodAmount(BuyAmount);
                BazaarBoughtCount++;
                ResourceRefreshQueued = true;
                UIRefreshQueued = true;
            } else
            {
                NotificationBox bx = new NotificationBox();
                bx.Notify(NotEnoughMoney[0], NotEnoughMoney[1], NotificationBox.types.OKOnly);
            }
        }
        /// <summary>
        /// Callback for Buy Stone button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void BuyStone(object s, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            int AmountOfGold = PlayerList[WhichUser - 1].getCurrentMoney();
            if (AmountOfGold >= StoneCost) // arbitrary amount
            {
                PlayerList[WhichUser - 1].decreaseMoneyAmount(StoneCost);
                PlayerList[WhichUser - 1].increaseStoneAmount(BuyAmount);
                BazaarBoughtCount++;
                ResourceRefreshQueued = true;
                UIRefreshQueued = true;
            }
            else
            {
                NotificationBox bx = new NotificationBox();
                bx.Notify(NotEnoughMoney[0], NotEnoughMoney[1], NotificationBox.types.OKOnly);

            }
        }
        /// <summary>
        /// Callback for Buy Food button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void BuyFood(object s, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            int AmountOfGold = PlayerList[WhichUser - 1].getCurrentMoney();
            if (AmountOfGold >= FoodCost) // arbitrary amount
            {
                PlayerList[WhichUser - 1].decreaseMoneyAmount(FoodCost);
                PlayerList[WhichUser - 1].increaseFoodAmount(BuyAmount);
                BazaarBoughtCount++;
                ResourceRefreshQueued = true;
                UIRefreshQueued = true;
            }
            else
            {
                NotificationBox bx = new NotificationBox();
                bx.Notify(NotEnoughMoney[0], NotEnoughMoney[1], NotificationBox.types.OKOnly);
            }
        }
        /// <summary>
        /// Callback for Harvest Gold button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void HarvestGold(object sender, MouseArgs e)
        {
            // We calculate the turn so we can get the position of the player.
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            Field fieldObj = null; // Then we search for the tile's data that they're on so we can do math with it.
            foreach (Field f in GameTilemap)
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
                // Game mechanics here.
                if (fieldObj.Gold <= 50)
                {
                    int amountGained = fieldObj.Gold;
                    fieldObj.Gold = 0;
                    PlayerList[WhichUser - 1].increaseMoneyAmount(amountGained);
                }
                else
                {
                    fieldObj.Gold -= 50;
                    PlayerList[WhichUser - 1].increaseMoneyAmount(50);
                }
                ResourceRefreshQueued = true;
                AlreadyHarvested = true;
                UIRefreshQueued = true;
            }
        }
        /// <summary>
        /// Callback for Harvest Wood button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void HarvestWood(object sender, MouseArgs e)
        {
            // We calculate the turn so we can get the position of the player.
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            Field fieldObj = null; // Then we search for the tile's data that they're on so we can do math with it.
            foreach (Field f in GameTilemap)
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
                // Game mechanics here.
                if (fieldObj.Wood <= 50)
                {
                    int amountGained = fieldObj.Wood;
                    fieldObj.Wood = 0;
                    PlayerList[WhichUser - 1].increaseWoodAmount(amountGained);
                } else
                {
                    fieldObj.Wood -= 50;
                    PlayerList[WhichUser - 1].increaseWoodAmount(50);
                }
                ResourceRefreshQueued = true;
                AlreadyHarvested = true;
                UIRefreshQueued = true; // We refresh the U.I. so it updates the button to disabled mode.
            }
        }
        /// <summary>
        /// Callback for Harvest Food button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void HarvestFood(object sender, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            Field fieldObj = null;
            foreach (Field f in GameTilemap)
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
                    PlayerList[WhichUser - 1].increaseFoodAmount(amountGained);
                }
                else
                {
                    fieldObj.Food -= 50;
                    PlayerList[WhichUser - 1].increaseFoodAmount(50);
                }
                ResourceRefreshQueued = true;
                AlreadyHarvested = true;
                UIRefreshQueued = true;
            }
        }
        /// <summary>
        /// Callback for Harvest Stone button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void HarvestStone(object sender, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            Field fieldObj = null;
            foreach (Field f in GameTilemap)
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
                    PlayerList[WhichUser - 1].increaseStoneAmount(amountGained);
                }
                else
                {
                    fieldObj.Stone -= 50;
                    PlayerList[WhichUser - 1].increaseStoneAmount(50);
                }
                ResourceRefreshQueued = true;
                AlreadyHarvested = true;
                UIRefreshQueued = true;
            }
        }

        /// <summary>
        /// Callback for create City Button.
        /// </summary>
        /// <param name="s">Sender</param>
        /// <param name="e">Mouse Parameters</param>
        private void CreateCity(object s, MouseArgs e)
        {
            int WhichUser = CalculateUserTurn();
            Vertex2 Coordinate = CalculateUserTilePosition(WhichUser - 1);
            MakeCity(Coordinate, WhichUser);
            ResourceRefreshQueued = true;
            UIRefreshQueued = true;
            PlayerList[WhichUser - 1].decreaseWoodAmount(150);
            PlayerList[WhichUser - 1].decreaseStoneAmount(100);
        }

        private void NextRound(object sender, MouseArgs e)
        {
            TurnID++;
            AlreadyHarvested = false;
            BazaarBoughtCount = 0;
            ResourceRefreshQueued = true;
            UIRefreshQueued = true;
            NewTurn = true;
            InBazaar = false;
            RefreshTurn();
            FocusOnUser();
        }

        // Helper Functions
        /// <summary>
        /// Grabs the ID of the Tile in the List.
        /// </summary>
        /// <param name="position">Position of Tile</param>
        private int GetTileID (Vertex2 position)
        {
            int index = -1;
            int found = -1;
            foreach (Field f in GameTilemap)
            {
                index++;
                if ((float)f.getCoordinate().x == position.x && (float)f.getCoordinate().y == position.y)
                {
                    found = index;
                    break;
                }
            }
            return found;
        }

        /// <summary>
        /// Grabs the field type.
        /// </summary>
        /// <param name="position">Position of Tile</param>
        /// <returns>FieldType</returns>
        private FieldType GetFieldType (Vertex2 position)
        {
            foreach (Field f in GameTilemap)
            {
                if ((float)f.getCoordinate().x == position.x && (float)f.getCoordinate().y == position.y)
                    return f.fieldType;
            }
            return FieldType.Sea;
        }
        private int GetFieldOccupancy (Vertex2 position)
        {
            foreach (Field f in GameTilemap)
            {
                if ((float)f.getCoordinate().x == position.x && (float)f.getCoordinate().y == position.y)
                    return f.OccupiedByTeam;
            }
            return 0;
        }
        private float GetTilemapWidth()
        {
            return (((float)GameTilemap[GameTilemap.Count - 1].getCoordinate().x + 1) * TileSize);
        }
        private float GetTilemapHeight()
        {
            return (((float)GameTilemap[GameTilemap.Count - 1].getCoordinate().y + 1) * TileSize);
        }
        /// <summary>
        /// Clamps value into screen border.
        /// </summary>
        /// <param name="x">Value</param>
        /// <returns>Clamped Value</returns>
        private float ClampScreenX(float x)
        {
            if (x <= 0) return 0;
            return Math.Min(GetTilemapWidth() - Program.ScreenWidth, x);
        }
        /// <summary>
        /// Clamps value into screen border.
        /// </summary>
        /// <param name="y">Value</param>
        /// <returns>Clamped Value</returns>
        private float ClampScreenY(float y)
        {
            if (y <= 0) return 0;
            return Math.Min(GetTilemapHeight() - Program.ScreenHeight, y);
        }
        /// <summary>
        /// Attempts to center screen on the current user.
        /// </summary>
        private void FocusOnUser()
        {
            int WhichUser = CalculateUserTurn();
            float UserX = PlayerList[WhichUser - 1].x;
            float UserY = PlayerList[WhichUser - 1].y;
            float _scrollX = UserX - (Program.ScreenWidth / 2);
            float _scrollY = UserY - (Program.ScreenHeight / 2);
            float clampX = ClampScreenX(_scrollX);
            float clampY = ClampScreenY(_scrollY);
            ScrollX = clampX;
            ScrollY = clampY;
            MainDM.x = (-clampX);
            MainDM.y = (-clampY);
        }
        private void RecalculateTilemapOccupancy ()
        {
            Dictionary<int, int> OperationsRequested = new Dictionary<int, int>();
            Dictionary<int, Vertex2> PlayerPositions = new Dictionary<int, Vertex2>();
            for (int i=0; i<NumberOfPlayers; i++)
            {
                PlayerPositions.Add(i, CalculateUserTilePosition(i));
            }
            int index = 0;
            // We precalculate what we need to do before doing so, because if an object is modified while you're iterating through it, it will Error.
            foreach (Field f in GameTilemap)
            {
                if (f.Soldiers > 0 && f.OccupiedByTeam > 0)
                {
                    // Ignore this tile. No need to change a tile when it's already fine.
                } else
                {
                    foreach (KeyValuePair<int, Vertex2> playerPosition in PlayerPositions)
                    {
                        if (playerPosition.Value.x == f.getCoordinate().x
                            && playerPosition.Value.y == f.getCoordinate().y) // If a player is on the tile, mark the tile's occupancy.
                        {
                            OperationsRequested.Add(index, playerPosition.Key+1);
                            break;
                        }
                    }
                }
                index++;
            }
            foreach (KeyValuePair<int,int> op in OperationsRequested)
            {
                GameTilemap[op.Key].OccupiedByTeam = op.Value;
            }
        }
        /// <summary>
        /// Calculates the User's tile position.
        /// </summary>
        /// <param name="id">Player ID</param>
        /// <returns>Vertex2 of their position.</returns>
        private Vertex2 CalculateUserTilePosition(int id)
        {
            return new Vertex2(Math.Floor(PlayerList[id].x / TileSize), Math.Floor(PlayerList[id].y / TileSize));
        }
        /// <summary>
        /// Calculates which user's turn it is.
        /// </summary>
        /// <returns>User ID</returns>
        private int CalculateUserTurn()
        {
            return ((TurnID - 1) % PlayerList.Count) + 1;
        }
        /// <summary>
        /// Sets tile territory value.
        /// </summary>
        /// <param name="position">Position of Tile</param>
        /// <param name="TeamID">TeamID</param>
        private void Conquer(Vertex2 position, int TeamID)
        {
            int found = GetTileID(position);
            if (found != -1)
            {
                GameTilemap[found].Team = TeamID;
            }
        }
        /// <summary>
        /// Changes tile into city.
        /// </summary>
        /// <param name="position">Position of Tile</param>
        /// <param name="TeamID">TeamID</param>
        private void MakeCity(Vertex2 position, int TeamID)
        {
            int found = GetTileID(position);
            if (found != -1)
            {
                GameTilemap[found].IsCity = true;
                GameTilemap[found].Team = TeamID;
                UIRefreshQueued = true;
                //gameField[found].setTexture("city texture");
            }
        }

        private void SetSoldiers (Vertex2 Coordinate, int Amount, int TeamID)
        {
            int ID = GetTileID(Coordinate);
            if (ID == -1)
                return; // Invalid tile!
            Field f = GameTilemap[ID];
            f.Soldiers = Amount;
            f.OccupiedByTeam = TeamID;
        }

        private void MoveSoldiers (Vertex2 OriginalCoordinate, Vertex2 NewCoordinate, int amount)
        {
            int ID_1 = GetTileID(OriginalCoordinate);
            int ID_2 = GetTileID(NewCoordinate);
            if (ID_1 == -1 || ID_2 == -1)
                return; // Invalid operation
            Field f1 = GameTilemap[ID_1];
            Field f2 = GameTilemap[ID_2];
            f1.Soldiers -= amount;
            f2.Soldiers += amount;
            f2.OccupiedByTeam = f1.OccupiedByTeam;
            RecalculateTilemapOccupancy();
        }

        // U.I. Processors
        /// <summary>
        /// Resets some values for proper operation.
        /// </summary>
        private void RefreshTurn ()
        {
            int WhichUser = CalculateUserTurn();
            UserIDHeld = WhichUser - 1;
            PlayerOriginX = PlayerList[UserIDHeld].x;
            PlayerOriginY = PlayerList[UserIDHeld].y;
            LastPlayerMoveX = PlayerOriginX;
            LastPlayerMoveY = PlayerOriginY;
            RecalculateTilemapOccupancy();
        }
        /// <summary>
        /// Refreshes the U.I.
        /// </summary>
        private void RefreshUIElements ()
        {
            int WhichUser = CalculateUserTurn();
            Vertex2 CurrentPosition = CalculateUserTilePosition(WhichUser - 1);
            FieldType fieldType = FieldType.Sea;
            Field fieldObj = null;
            foreach (Field f in GameTilemap)
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
                bool isCity = GameTilemap[GetTileID(CalculateUserTilePosition(WhichUser - 1))].IsCity;
                // We need to redo u.i. stuff.
                foreach (Drawable d in UIButtons)
                {
                    GameInterfaceDM.drawables.Remove(d);
                }
                UIButtons = new List<Drawable>();
                if (!isCity)
                {
                    switch (fieldType)
                    {
                        case FieldType.Mountain:
                            if (fieldObj.Stone > 0 && !AlreadyHarvested)
                            {
                                UIButtons.Add(Button_HarvestStone);
                                UIButtons.Add(Button_HarvestGold);
                            } else
                            {
                                UIButtons.Add(Sprite_DisabledStone);
                                UIButtons.Add(Sprite_DisabledGold);
                            }
                            break;
                        case FieldType.Forest:
                            if (fieldObj.Wood > 0 && !AlreadyHarvested)
                            {
                                UIButtons.Add(Button_HarvestWood);
                            } else
                            {
                                UIButtons.Add(Sprite_DisabledWood);
                            }
                            break;
                        case FieldType.Pasture:
                            if (fieldObj.Food > 0 && !AlreadyHarvested)
                            {
                                UIButtons.Add(Button_HarvestFood);
                            } else
                            {
                                UIButtons.Add(Sprite_DisabledFood);
                            }
                            break;
                        case FieldType.Desert:
                            if (!InBazaar) {
                                UIButtons.Add(Button_AccessBazaar);
                            } else
                            {
                                if (BazaarBoughtCount >= 2)
                                {
                                    UIButtons.Add(Sprite_DisabledBuyFood);
                                    UIButtons.Add(Sprite_DisabledBuyStone);
                                    UIButtons.Add(Sprite_DisabledBuyWood);
                                } else
                                {
                                    UIButtons.Add(Button_BuyFood);
                                    UIButtons.Add(Button_BuyStone);
                                    UIButtons.Add(Button_BuyWood);
                                }
                                UIButtons.Add(Button_ExitBazaar);

                                Text t_bazaarPrice_Wood = new Text("Price of Wood: " + WoodCost + " per " + BuyAmount);
                                Text t_bazaarPrice_Stone = new Text("Price of Stone: " + StoneCost + " per " + BuyAmount);
                                Text t_bazaarPrice_Food = new Text("Price of Food: " + FoodCost + " per " + BuyAmount);
                                t_bazaarPrice_Food.x = TextStartPosition;
                                t_bazaarPrice_Food.y = TextStartYPosition + TextMarginY * 3;
                                t_bazaarPrice_Stone.x = TextStartPosition;
                                t_bazaarPrice_Stone.y = TextStartYPosition + TextMarginY * 4;
                                t_bazaarPrice_Wood.x = TextStartPosition;
                                t_bazaarPrice_Wood.y = TextStartYPosition + TextMarginY * 5;
                                Text t_purchases = new Text("Purchases Remaining: " + (2 - BazaarBoughtCount));
                                t_purchases.x = TextStartPosition;
                                t_purchases.y = TextStartYPosition + TextMarginY * 2;
                                UIButtons.Add(t_bazaarPrice_Food);
                                UIButtons.Add(t_bazaarPrice_Stone);
                                UIButtons.Add(t_bazaarPrice_Wood);
                                UIButtons.Add(t_purchases);
                            }
                            break;
                        default:
                            // No buttons.
                            break;
                    }
                    if (!InBazaar)
                    {
                        UIButtons.Add(Button_NextRound);
                    }
                    if (!InBazaar && (PlayerList[WhichUser - 1].getCurrentWood() >= 150 && PlayerList[WhichUser - 1].getCurrentStone() >= 100))
                    {
                        UIButtons.Add(Button_CreateCity);
                    }
                    else
                    {
                        UIButtons.Add(Sprite_DisabledCreateCity);
                    }
                } else
                {

                    Field f = GameTilemap[GetTileID(CalculateUserTilePosition(CalculateUserTurn() - 1))];

                    bool CanUpgradeWall = true;

                    if (f.WallLevel == 0 && CanUpgradeWall)
                        UIButtons.Add(Button_CreateWall);
                    else if (f.WallLevel < Field.MaxWallLevel && CanUpgradeWall)
                        UIButtons.Add(Button_UpgradeWall);
                    else
                        UIButtons.Add(Sprite_DisabledWallUpgrade);

                    bool HasResourcesForBarracks = true;

                    if (f.BarracksBuilt == false && HasResourcesForBarracks)
                        UIButtons.Add(Button_CreateBarracks);
                    else
                        UIButtons.Add(Sprite_DisabledBarracks);

                    bool HasResourcesForSoldiers = true;
                    if (f.BarracksBuilt)
                    {
                        if (HasResourcesForSoldiers)
                        {
                            UIButtons.Add(Button_CreateSoldiers);
                        } else
                        {
                            UIButtons.Add(Sprite_DisabledSoldiers);
                        }
                    }

                    if (f.Soldiers > 0)
                    {
                        UIButtons.Add(Button_MoveSoldiers);
                    }

                    UIButtons.Add(Button_NextRound);
                }
                foreach (Drawable d in UIButtons) // The new buttons are added back.
                {
                    GameInterfaceDM.drawables.Add(d);
                }
            }
            if (true)
            {
                foreach (Drawable d in UIText)
                {
                    GameInterfaceDM.drawables.Remove(d);
                }
                UIText = new List<Drawable>();
                Text turnNumberText = new Text(String.Format(TurnNumber,TurnID));
                Text playerTurnText = new Text(String.Format(PlayerTurn, CalculateUserTurn(), (Team)CalculateUserTurn()).ToString());
                Text resourceText = null;
                Text resourceText2 = null;
                switch (fieldType)
                {
                    case FieldType.Forest:
                        // Wood
                        if (fieldObj != null)
                            resourceText = new Text(String.Format(ResourceRemaining,fieldObj.Wood,"Wood"));
                        break;
                    case FieldType.Pasture:
                        if (fieldObj != null)
                            resourceText = new Text(String.Format(ResourceRemaining, fieldObj.Food, "Food"));
                        break;
                    case FieldType.Mountain:
                        if (fieldObj != null)
                        {
                            resourceText = new Text(String.Format(ResourceRemaining, fieldObj.Stone, "Stone"));
                            resourceText2 = new Text(String.Format(ResourceRemaining, fieldObj.Gold, "Gold"));
                        }
                        break;
                    default:
                        break;
                }
                turnNumberText.x = TextStartPosition;
                turnNumberText.y = TextStartYPosition;
                playerTurnText.x = TextStartPosition;
                playerTurnText.y = TextStartYPosition+TextMarginY;
                if (resourceText != null)
                {
                    resourceText.x = TextStartPosition;
                    resourceText.y = TextStartYPosition+TextMarginY*2;
                }

                if (resourceText2 != null)
                {
                    resourceText2.x = TextStartPosition;
                    resourceText2.y = TextStartYPosition + TextMarginY * 3;
                }
                UIText.Add(turnNumberText);
                UIText.Add(playerTurnText);
                if (resourceText != null)
                    UIText.Add(resourceText);
                if (resourceText2 != null)
                    UIText.Add(resourceText2);
                foreach (Drawable d in UIText)
                {
                    GameInterfaceDM.drawables.Add(d);
                }
                // Map highlights
                if (NewTurn)
                {
                    foreach (Drawable d in MapUI)
                    {
                        MainDM.drawables.Remove(d);
                    }
                    MapUI = new List<Drawable>();
                    bool Up = (CurrentPosition.y > 0);
                    bool Down = (CurrentPosition.y < TilemapSize - 1);
                    bool Left = (CurrentPosition.x > 0);
                    bool Right = (CurrentPosition.x < TilemapSize - 1);
                    Vertex2 cpos = new Vertex2(CurrentPosition.x * TileSize, CurrentPosition.y * TileSize);
                    Vertex2 up_pos = new Vertex2(cpos.x, cpos.y - TileSize);
                    Vertex2 down_pos = new Vertex2(cpos.x, cpos.y + TileSize);
                    Vertex2 left_pos = new Vertex2(cpos.x - TileSize, cpos.y);
                    Vertex2 right_pos = new Vertex2(cpos.x + TileSize, cpos.y);
                    Vertex2 up_pos_d = new Vertex2((float)Math.Floor(up_pos.x / TileSize), (float)Math.Floor(up_pos.y / TileSize));
                    Vertex2 down_pos_d = new Vertex2((float)Math.Floor(down_pos.x / TileSize), (float)Math.Floor(down_pos.y / TileSize));
                    Vertex2 left_pos_d = new Vertex2((float)Math.Floor(left_pos.x / TileSize), (float)Math.Floor(left_pos.y / TileSize));
                    Vertex2 right_pos_d = new Vertex2((float)Math.Floor(right_pos.x / TileSize), (float)Math.Floor(right_pos.y / TileSize));
                    FieldType ft_up = GetFieldType(up_pos_d);
                    FieldType ft_down = GetFieldType(down_pos_d);
                    FieldType ft_left = GetFieldType(left_pos_d);
                    FieldType ft_right = GetFieldType(right_pos_d);
                    int fo_up = GetFieldOccupancy(up_pos_d);
                    int fo_down = GetFieldOccupancy(down_pos_d);
                    int fo_left = GetFieldOccupancy(left_pos_d);
                    int fo_right = GetFieldOccupancy(right_pos_d);
                    Sprite cpos_s = new Sprite(CurrentTileHighlight, (float)cpos.x, (float)cpos.y);
                    MapUI.Add(cpos_s);
                    if (Up && ft_up != FieldType.Sea && (fo_up == WhichUser || fo_up == 0))
                    {
                        Sprite UpSprite = new Sprite(MoveTileHighlight, (float)up_pos.x, (float)up_pos.y);
                        MapUI.Add(UpSprite);
                    }
                    if (Right && ft_right != FieldType.Sea && (fo_right == WhichUser || fo_right == 0))
                    {
                        Sprite RightSprite = new Sprite(MoveTileHighlight, (float)right_pos.x, (float)right_pos.y);
                        MapUI.Add(RightSprite);
                    }
                    if (Left && ft_left != FieldType.Sea && (fo_left == WhichUser || fo_left == 0))
                    {
                        Sprite LeftSprite = new Sprite(MoveTileHighlight, (float)left_pos.x, (float)left_pos.y);
                        MapUI.Add(LeftSprite);
                    }
                    if (Down && ft_down != FieldType.Sea && (fo_down == WhichUser || fo_down == 0))
                    {
                        Sprite DownSprite = new Sprite(MoveTileHighlight, (float)down_pos.x, (float)down_pos.y);
                        MapUI.Add(DownSprite);
                    }
                    foreach (Field f in GameTilemap)
                    {
                        if (f.Team != 0)
                        {
                            Sprite tSpriteColor = new Sprite(String.Format(TeamHighlight, ((Team)f.Team).ToString()), (float)f.x, (float)f.y);
                            MapUI.Add(tSpriteColor);
                        }
                    }
                    foreach (Drawable d in MapUI)
                    {
                        MainDM.Add(d);
                    }
                    NewTurn = false;
                }
            }
        }
        private int CalculateSoldiers (int TeamID)
        {
            int soldiers = 0;
            foreach (Field f in GameTilemap)
            {
                if (f.OccupiedByTeam == TeamID || f.Team == TeamID)
                {
                    if (f.Soldiers > 0)
                        soldiers += f.Soldiers;
                }
            }
            return soldiers;
        }
        private void RefreshResourceBar ()
        {
            foreach (Drawable d in UITop)
                ResourceDM.drawables.Remove(d);
            UITop = new List<Drawable>();
            int WhichUser = CalculateUserTurn();
            Text t_gold = new Text(PlayerList[WhichUser - 1].getCurrentMoney().ToString());
            Text t_stone = new Text(PlayerList[WhichUser - 1].getCurrentStone().ToString());
            Text t_food = new Text(PlayerList[WhichUser - 1].getCurrentFood().ToString());
            Text t_wood = new Text(PlayerList[WhichUser - 1].getCurrentWood().ToString());
            Text t_soldier = new Text(CalculateSoldiers(WhichUser).ToString());
            t_gold.x = GoldIconPos + TextPaddingLeft;
            t_stone.x = StoneIconPos + TextPaddingLeft;
            t_food.x = FoodIconPos + TextPaddingLeft;
            t_wood.x = WoodIconPos + TextPaddingLeft;
            t_soldier.x = SoldierIconPos + TextPaddingLeft;
            UITop.Add(t_gold);
            UITop.Add(t_stone);
            UITop.Add(t_food);
            UITop.Add(t_wood);
            UITop.Add(t_soldier);

            foreach (Drawable d in UITop)
                ResourceDM.Add(d);
        }
        /// <summary>
        /// Allows player movement.
        /// </summary>
        private void UserDragProcessor ()
        {
            if (!PreprocComplete)
            {
                LastPlayerMoveX = 0;
                LastPlayerMoveY = 0;
                PreprocComplete = true;
                FirstMoveCheck = false;
            }
            if (UserIDHeld >= 0)
            {
                DisableScrolling = true; // Disable camera movement while moving player.

                int PlayerTileXOriginal = (int)Math.Floor(PlayerOriginX / 250); // Rounds down always 0.5 = 0
                int PlayerTileYOriginal = (int)Math.Floor(PlayerOriginY / 250); // This calculates current Tile Position of player.

                int MinX = PlayerTileXOriginal * 250 - 250;
                int MaxX = PlayerTileXOriginal * 250 + 250;
                int MinY = PlayerTileYOriginal * 250 - 250;
                int MaxY = PlayerTileYOriginal * 250 + 250;

                float PlayerOffsetX = (UserMouse.getX() + ScrollX - PlayerOriginX);
                float PlayerOffsetY = (UserMouse.getY() + ScrollY - PlayerOriginY);

                int DestinationX_Low = (int)(PlayerOriginX + PlayerOffsetX);
                int DestinationY_Low = (int)(PlayerOriginY + PlayerOffsetY);

                int DestinationX_High = (int)(PlayerOriginX + PlayerOffsetX + PlayerList[0].w);
                int DestinationY_High = (int)(PlayerOriginY + PlayerOffsetY + PlayerList[0].h);

                int DestinationTileX = (int)Math.Floor(DestinationX_Low / 250f);
                int DestinationTileY = (int)Math.Floor(DestinationY_Low / 250f);

                int DestinationTileX_High = (int)Math.Floor(DestinationX_High / 250f);
                int DestinationTileY_High = (int)Math.Floor(DestinationY_High / 250f);

                if ((DestinationTileY - PlayerTileYOriginal != 0 && DestinationTileX - PlayerTileXOriginal != 0)
                    || (DestinationTileX_High - PlayerTileXOriginal != 0 && (DestinationTileY_High - PlayerTileYOriginal != 0 || DestinationTileY - PlayerTileYOriginal != 0))
                    || Math.Abs(DestinationTileY - PlayerTileYOriginal) > 1
                    || Math.Abs(DestinationTileX - PlayerTileXOriginal) > 1
                    || Math.Abs(DestinationTileY_High - PlayerTileYOriginal) > 1
                    || Math.Abs(DestinationTileX_High - PlayerTileXOriginal) > 1
                    || GetFieldType(new Vertex2(DestinationTileX, DestinationTileY)) == FieldType.Sea
                    || GetFieldType(new Vertex2(DestinationTileX_High, DestinationTileY_High)) == FieldType.Sea // No diagonal movement.
                    || (UserMouse.getX() >= GameInterfaceStartPosition.x && UserMouse.getX() <= GameInterfaceStartPosition.x+800 && UserMouse.getY() >= Program.ScreenHeight-200))
                {
                    // Invalid move. Set back position of player!
                    if (FirstMoveCheck)
                    {
                        PlayerList[UserIDHeld].x = LastPlayerMoveX;
                        PlayerList[UserIDHeld].y = LastPlayerMoveY;
                    }
                } else // So if only one value changes, allow movement.
                {
                    float NewPlayerX = PlayerOriginX + PlayerOffsetX;
                    float NewPlayerY = PlayerOriginY + PlayerOffsetY;

                    LastPlayerMoveX = NewPlayerX;
                    LastPlayerMoveY = NewPlayerY;

                    PlayerList[UserIDHeld].x = NewPlayerX;
                    PlayerList[UserIDHeld].y = NewPlayerY;
                    FirstMoveCheck = true;
                    UIRefreshQueued = true;
                }
            }
        }
        /// <summary>
        /// Allows screen to scroll.
        /// </summary>
        private void ScrollingProcessor ()
        {
            float MouseX = UserMouse.getX();
            float MouseY = UserMouse.getY();
            bool CheckLeft = (MouseX >= 0 && MouseX <= ScrollingEdgeSize);
            bool CheckTop = (MouseY >= 0 && MouseY <= ScrollingEdgeSize);
            bool CheckRight = (MouseX >= Program.ScreenWidth - ScrollingEdgeSize);
            bool CheckBottom = (MouseY >= Program.ScreenHeight - ScrollingEdgeSize);
            if (CheckBottom && MouseX >= GameInterfaceStartPosition.x && MouseX <= GameInterfaceStartPosition.x + 800)
                return;

            if (CheckLeft && ScrollX > 0)
                ScrollX = Math.Max(0,ScrollX-ScrollSpeedPerFrame);

            if (CheckTop && ScrollY > 0)
                ScrollY = Math.Max(0, ScrollY - ScrollSpeedPerFrame);

            if (CheckRight && ScrollX < GetTilemapWidth() - Program.ScreenWidth)
                ScrollX = Math.Min(GetTilemapWidth() - Program.ScreenWidth, ScrollX + ScrollSpeedPerFrame);

            if (CheckBottom && ScrollY < GetTilemapHeight() - Program.ScreenHeight)
                ScrollY = Math.Min(GetTilemapHeight() - Program.ScreenHeight, ScrollY + ScrollSpeedPerFrame);

            if (CheckLeft || CheckTop || CheckRight || CheckBottom)
            {
                Cursor.SetCursor(bCursor.cursortype.scroll);
                MainDM.x = -ScrollX;
                MainDM.y = -ScrollY;
            } else
            {
                Cursor.SetCursor(bCursor.cursortype.main);
            }
        }

        // AlphaGFX Processors
        public override void Draw(GameWindow gw)
        {
            if (UIRefreshQueued)
            {
                RefreshUIElements();
                UIRefreshQueued = false;
            }

            if (ResourceRefreshQueued)
            {
                RefreshResourceBar();
                ResourceRefreshQueued = false;
            }

            Program.CalculateFPS();
            if (!DisableScrolling)
            {
                ScrollingProcessor();
            } else
            {
                Cursor.SetCursor(bCursor.cursortype.main);
            }
            PlayerDM.x = MainDM.x;
            PlayerDM.y = MainDM.y;
            MainDM.Draw();
            PlayerDM.Draw();
            GameInterfaceDM.Draw();
            ResourceDM.Draw();
            CursorDM.Draw();
            if (UserBeingHeld)
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
            GameInterfaceDM.OnMouseDown(button);
            UserBeingHeld = true;
            MouseOriginX = UserMouse.getX()+ScrollX;
            MouseOriginY = UserMouse.getY()+ScrollY;
        }
        public override void OnMouseUp(MouseButtonEventArgs button)
        {
            GameInterfaceDM.OnMouseUp(button);
            UserBeingHeld = false;
            PreprocComplete = false;
        }
        public override void Update()
        {
            GameInterfaceDM.Update();
        }
    }
}
