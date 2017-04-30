using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib.Scenes;
using AGFXLib.Drawables;
using OpenTK;
using OpenTK.Input;
using SK_Strategygame.UI;

namespace SK_Strategygame.Scenes.MainMenu
{
    class HostJoin : Scene
    {
        public DrawManager dm;
        public bCursor cursor;
        public Sprite BackgroundSprite, borderSprite;
        public bButton hostButton, joinButton;
        public HostJoin ()
        {
            //Create Objects
            dm = new DrawManager();
            BackgroundSprite = new Sprite("Resources/MainMenu/background.png");
            borderSprite = new Sprite("Resources/MainMenu/border.png");
            hostButton = new bButton("Resources/MainMenu/nohover/host.png", "Resources/MainMenu/hover/host.png");
            joinButton = new bButton("Resources/MainMenu/nohover/join.png", "Resources/MainMenu/hover/join.png");
            cursor = new bCursor();

            //Set Positions and W / H
            BackgroundSprite.w = 1680;
            BackgroundSprite.h = 1050;
            borderSprite.x = 1680 / 2 - borderSprite.w / 2;
            borderSprite.y = 180;
            hostButton.x = 1680 / 2 - hostButton.w / 2;
            hostButton.y = 300;
            joinButton.x = 1680 / 2 - hostButton.w / 2;
            joinButton.y = hostButton.y + hostButton.h + 100;

            //Set OnClicks
            joinButton.OnClick += Click_JoinButton;
            hostButton.OnClick += Click_HostButton;

            //Add to Layers
            dm.Add(BackgroundSprite);
            dm.Add(borderSprite);
            dm.Add(joinButton);
            dm.Add(hostButton);
            dm.Add(cursor);
        }


        private void Click_JoinButton(object sender, MouseArgs k)
        {
            //Program.aw.scene = new Lobby.JoinScene();
            Program.aw.scene = new InGame.GameScene();
        }
        private void Click_HostButton(object sender, MouseArgs k)
        {
            //Program.aw.scene = new Lobby.HostScene();
            Program.aw.scene = new InGame.GameScene();
        }
        public override void Draw(GameWindow gw)
        {
            dm.Draw();   
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {
            if(key.Key == Key.H)
            {
                Program.aw.scene = new InGame.GameScene();
            }
            if(key.Key == Key.J)
            {
                Program.aw.scene = new InGame.GameScene();
            }
            if(key.Key == Key.Escape)
            {
                Program.aw.scene = new MainMenuScene();
            }

            dm.OnKeyDown(key);
        }

        public override void OnKeyUp(KeyboardKeyEventArgs key)
        {
            dm.OnKeyUp(key);
        }

        public override void OnMouseDown(MouseButtonEventArgs button)
        {
            dm.OnMouseDown(button);
        }

        public override void OnMouseUp(MouseButtonEventArgs button)
        {
            dm.OnMouseUp(button);
        }

        public override void Update()
        {
        }
    }
}
