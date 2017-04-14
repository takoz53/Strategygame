
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using AGFXLib.Scenes;
using AGFXLib.Drawables;
using SK_Strategygame.UI;
using System.Runtime.InteropServices;
namespace SK_Strategygame.Scenes
{
    class MainMenuScene : Scene
    {
        public Sprite borderSprite, BackgroundSprite, CursorSprite;
        public DrawManager dm;
        public bButton newGameSprite, optionsSprite, exitSprite;
        NotificationBox nb;
        public MainMenuScene()
        {
            // init code
            Console.WriteLine("Initializing MainMenuScene");
            dm = new DrawManager();
            nb = new NotificationBox();
            borderSprite = new Sprite("Resources/MainMenu/border.png");
            borderSprite.x = 1680 / 2 - borderSprite.width / 2;
            borderSprite.y = 180;

            newGameSprite = new bButton("Resources/MainMenu/nohover/newgame.png", "Resources/MainMenu/hover/newgame.png");
            newGameSprite.x = 1680 / 2 - newGameSprite.width / 2;
            newGameSprite.y = 230;

            optionsSprite = new bButton("Resources/MainMenu/nohover/options.png", "Resources/MainMenu/hover/options.png");
            optionsSprite.x = 1680 / 2 - optionsSprite.width / 2;
            optionsSprite.y = newGameSprite.y + newGameSprite.texture.Height + 50;

            exitSprite = new bButton("Resources/MainMenu/nohover/exit.png", "Resources/MainMenu/hover/exit.png");
            exitSprite.x = 1680 / 2 - exitSprite.width / 2;
            exitSprite.y = optionsSprite.y + optionsSprite.texture.Height + 50;

            CursorSprite = new Sprite("Resources/Cursors/Cursor_Main.png");

            newGameSprite.OnClick += Sprite_ClickNewGame;
            optionsSprite.OnClick += Sprite_ClickOptions;
            exitSprite.OnClick += Sprite_ClickExit;
            BackgroundSprite = new Sprite("Resources/MainMenu/background.png");
            BackgroundSprite.width = 1680;
            BackgroundSprite.height = 1050;

            //Layer System
            dm.Add(BackgroundSprite);
            dm.Add(borderSprite);
            dm.Add(newGameSprite);
            dm.Add(optionsSprite);
            dm.Add(exitSprite);
            dm.Add(CursorSprite);
            //Draw Cursor Last to have it on top
        }

        private void Sprite_ClickExit(object s, SpriteClickArgs k)
        {
            Environment.Exit(0);
        }

        private void Sprite_ClickOptions(object s, SpriteClickArgs k)
        {
            nb.Notify("Options doesn't have a functionality yet", "Coming soon...", NotificationBox.types.OKOnly);
        }
        private void Sprite_ClickNewGame(object s, SpriteClickArgs k)
        {
            Program.aw.scene = new SecondScene();
        }

        public override void Draw(GameWindow gw)
        {
            CursorSprite.x = AGFXLib.UserMouse.GetX();
            CursorSprite.y = AGFXLib.UserMouse.GetY();
            dm.Draw();
        }
        public override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs key)
        {
            dm.OnKeyDown(key);
        }
        public override void OnKeyUp(OpenTK.Input.KeyboardKeyEventArgs key)
        {
            dm.OnKeyUp(key);
        }
        public override void OnMouseDown(OpenTK.Input.MouseButtonEventArgs button)
        {
            dm.OnMouseDown(button);
        }
        public override void OnMouseUp(OpenTK.Input.MouseButtonEventArgs button)
        {
            dm.OnMouseUp(button);
        }

        public override void Update() { }
    }
}
