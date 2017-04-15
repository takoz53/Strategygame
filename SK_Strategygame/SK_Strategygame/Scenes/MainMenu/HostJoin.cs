using AGFXLib.Scenes;
using AGFXLib.Drawables;
using OpenTK;
using OpenTK.Input;
using SK_Strategygame.UI;

namespace SK_Strategygame.Scenes.MainMenu
{
    class HostJoin : Scene
    {
        public Sprite borderSprite, BackgroundSprite;
        public bButton HostButton, JoinButton;
        public bCursor cursor;
        public DrawManager dm;

        public HostJoin()
        {

            BackgroundSprite = new Sprite("Resources/MainMenu/background.png");
            BackgroundSprite.width = 1680;
            BackgroundSprite.height = 1050;

            borderSprite = new Sprite("Resources/MainMenu/border.png");
            borderSprite.x = 1680 / 2 - borderSprite.width / 2;
            borderSprite.y = 220;

            HostButton = new bButton("Resources/MainMenu/nohover/host.png", "Resources/MainMenu/hover/host.png");
            HostButton.x = (1680 / 2) - (HostButton.width / 2);
            HostButton.y = 350;

            JoinButton = new bButton("Resources/MainMenu/nohover/join.png", "Resources/MainMenu/hover/join.png");
            JoinButton.x = (1680 / 2) - (JoinButton.width / 2);
            JoinButton.y = HostButton.y + HostButton.texture.Height + 100;

            HostButton.OnClick += Click_HostButton;
            JoinButton.OnClick += Click_JoinButton;
            cursor = new bCursor();
            dm = new DrawManager();
            dm.Add(BackgroundSprite);
            dm.Add(borderSprite);
            dm.Add(HostButton);
            dm.Add(JoinButton);
            dm.Add(cursor);
        }

        private void Click_JoinButton(object s, SpriteClickArgs k)
        {
            Program.aw.scene = new InGame.GameScene();
        }
        private void Click_HostButton(object s, SpriteClickArgs k)
        {
            Program.aw.scene = new InGame.GameScene();
        }
        public override void Draw(GameWindow gw)
        {
            bool hoveringOnAButton = (
            JoinButton.isBeingHovered ||
            HostButton.isBeingHovered
            );
            if (hoveringOnAButton)
                cursor.SetCursor("Resources/Cursors/Cursor_Main_Hover.png");
            else
                cursor.SetCursor("Resources/Cursors/Cursor_Main.png");
            dm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key)
        {
            if (key.Key == Key.Escape)
            {
                Program.aw.scene = new MainMenuScene();
            }
            if (key.Key == Key.H) //Host Hotkey
            {
                Program.aw.scene = new InGame.GameScene();
            }
            if (key.Key == Key.J) //Join Hotkey, goto GameScene because AGFXLib doesnt support ComboBox yet
            {
                Program.aw.scene = new InGame.GameScene();
            }
        }

        public override void OnKeyUp(KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(MouseButtonEventArgs button) { }
        public override void OnMouseUp(MouseButtonEventArgs button) { }
        public override void Update() { }
    }
}
