﻿using System;
using OpenTK;
using AGFXLib.Scenes;
using AGFXLib.Drawables;
using SK_Strategygame.UI;
using SK_Strategygame.Scenes.MainMenu;
using SK_Strategygame.Scenes.InGame;

namespace SK_Strategygame.Scenes
{
    class MainMenuScene : Scene
    {
        public Sprite borderSprite, BackgroundSprite;
        public bCursor cursor;
        public DrawManager dm;
        public bButton newGameButton, optionsButton, exitButton;
        public NotificationBox nb = new NotificationBox();

        public MainMenuScene()
        {
            // init code
            dm = new DrawManager();
            dm.w = Program.ScreenWidth;
            dm.h = Program.ScreenHeight;
            
            borderSprite = new Sprite("Resources/MainMenu/border.png", 0,0);
            borderSprite.x = 1680 / 2 - borderSprite.w / 2;
            borderSprite.y = 180;

            newGameButton = new bButton("Resources/MainMenu/nohover/newgame.png", "Resources/MainMenu/hover/newgame.png");
            newGameButton.x = 1680 / 2 - newGameButton.w / 2;
            newGameButton.y = 230;

            optionsButton = new bButton("Resources/MainMenu/nohover/options.png", "Resources/MainMenu/hover/options.png");
            optionsButton.x = 1680 / 2 - optionsButton.w / 2;
            optionsButton.y = newGameButton.y + newGameButton.h + 50;

            exitButton = new bButton("Resources/MainMenu/nohover/exit.png", "Resources/MainMenu/hover/exit.png");
            exitButton.x = 1680 / 2 - exitButton.w / 2;
            exitButton.y = optionsButton.y + optionsButton.h + 50;

            cursor = new bCursor();

            BackgroundSprite = new Sprite("Resources/MainMenu/background.png", 0, 0);
            BackgroundSprite.w = 1680;
            BackgroundSprite.h = 1050;

            newGameButton.OnClick += Click_ButtonNewGame;
            optionsButton.OnClick += Click_ButtonOptions;
            exitButton.OnClick += Click_ButtonExit;

            //Layer System
            dm.Add(BackgroundSprite);
            dm.Add(borderSprite);
            dm.Add(newGameButton);
            dm.Add(optionsButton);
            dm.Add(exitButton);
            dm.Add(cursor);
            //Draw Cursor Last to have it on top
            Program.aw.Cursor = MouseCursor.Empty;
        }

        private void Click_ButtonExit(object s, MouseArgs k)
        {
            Environment.Exit(0);
        }

        private void Click_ButtonOptions(object s, MouseArgs k)
        {
            Program.aw.scene = new OptionsScene();
        }

        private void Click_ButtonNewGame(object s, MouseArgs k)
        {
            Program.aw.scene = new GameScene();
        }

        public override void Draw(GameWindow gw)
        {
            dm.Draw();
            bool hoveringOnAButton = (
                newGameButton.isHovered() ||
                optionsButton.isHovered() ||
                exitButton.isHovered()
            );
            if (hoveringOnAButton)
                cursor.SetCursor(bCursor.cursortype.mainHover);
            else
                cursor.SetCursor(bCursor.cursortype.main);
        }
        public override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs key)
        {
            if(key.Key == OpenTK.Input.Key.N)
            {
                Program.aw.scene = new GameScene();
            }
            if(key.Key == OpenTK.Input.Key.O)
            {
                Program.aw.scene = new OptionsScene();
            }
            if(key.Key == OpenTK.Input.Key.Escape)
            {
                Environment.Exit(0);
            }
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
