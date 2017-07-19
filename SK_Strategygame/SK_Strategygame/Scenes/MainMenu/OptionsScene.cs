using AGFXLib.Scenes;
using OpenTK;
using OpenTK.Input;
using AGFXLib.Drawables;
using SK_Strategygame.UI;

namespace SK_Strategygame.Scenes.MainMenu
{
    class OptionsScene : Scene
    {
        DrawManager dm, cdm;
        bButton FieldSize_1;
        bButton FieldSize_2;
        bButton FieldSize_3;
        bButton Players_1;
        bButton Players_2;
        bButton Players_3;
        bButton OkButton;
        Sprite PlayerHeader;
        Sprite FieldHeader;
        bCursor cursor;
        int HeaderPos_1 = 1680 / 2 - 800 / 2;
        int HeaderPos_2 = 1680 / 2 - 800 / 2;
        int ButtonPos_1 = 440;
        int ButtonPos_2 = 440+266;
        int ButtonPos_3 = 440+266+267;
        int HeaderY_1 = 25;
        int ButtonY_1 = 125;
        int HeaderY_2 = 425;
        int ButtonY_2 = 525;
        int OKY = 800;
        int OKX = 1680 / 2 - 400 - 10;

        public OptionsScene()
        {
            dm = new DrawManager();
            dm.w = Program.ScreenWidth;
            dm.h = Program.ScreenHeight;
            cdm = new DrawManager();
            cdm.w = Program.ScreenWidth;
            cdm.h = Program.ScreenHeight;
            cursor = new bCursor();
            cdm.Add(cursor);
            Sprite BackgroundSprite = new Sprite("Resources/MainMenu/background.png", 0, 0);
            BackgroundSprite.w = 1680;
            BackgroundSprite.h = 1050;
            dm.Add(BackgroundSprite);
            FieldSize_1 = new bButton("Resources/Options/Field1.png", "Resources/Options/Field1_hover.png");
            FieldSize_1.x = ButtonPos_1;
            FieldSize_1.y = ButtonY_2;
            FieldSize_1.OnClick += FieldSize_1_OnClick;
            FieldSize_2 = new bButton("Resources/Options/Field2.png", "Resources/Options/Field2_hover.png");
            FieldSize_2.x = ButtonPos_2;
            FieldSize_2.y = ButtonY_2;
            FieldSize_2.OnClick += FieldSize_2_OnClick;
            FieldSize_3 = new bButton("Resources/Options/Field3.png", "Resources/Options/Field3_hover.png");
            FieldSize_3.x = ButtonPos_3;
            FieldSize_3.y = ButtonY_2;
            FieldSize_3.OnClick += FieldSize_3_OnClick;
            Players_1 = new bButton("Resources/Options/Player1.png", "Resources/Options/Player1_hover.png");
            Players_1.x = ButtonPos_1;
            Players_1.y = ButtonY_1;
            Players_1.OnClick += Players_1_OnClick;
            Players_2 = new bButton("Resources/Options/Player2.png", "Resources/Options/Player2_hover.png");
            Players_2.x = ButtonPos_2;
            Players_2.y = ButtonY_1;
            Players_2.OnClick += Players_2_OnClick;
            Players_3 = new bButton("Resources/Options/Player3.png", "Resources/Options/Player3_hover.png");
            Players_3.x = ButtonPos_3;
            Players_3.y = ButtonY_1;
            Players_3.OnClick += Players_3_OnClick;
            OkButton = new bButton("Resources/Options/Confirm.png", "Resources/Options/Confirm_hover.png");
            OkButton.x = OKX;
            OkButton.y = OKY;
            OkButton.OnClick += OK_OnClick;
            PlayerHeader = new Sprite("Resources/Options/PlayerHeader.png",0,0);
            PlayerHeader.x = HeaderPos_1;
            PlayerHeader.y = HeaderY_1;
            FieldHeader = new Sprite("Resources/Options/FieldHeader.png",0,0);
            FieldHeader.x = HeaderPos_2;
            FieldHeader.y = HeaderY_2;
            dm.Add(FieldSize_1);
            dm.Add(FieldSize_2);
            dm.Add(FieldSize_3);
            dm.Add(Players_1);
            dm.Add(Players_2);
            dm.Add(Players_3);
            dm.Add(OkButton);
            dm.Add(PlayerHeader);
            dm.Add(FieldHeader);
        }

        private void FieldSize_1_OnClick (object s, MouseArgs e)
        {
            UserSettings.MapSize = 10;
        }

        private void FieldSize_2_OnClick(object s, MouseArgs e)
        {
            UserSettings.MapSize = 20;
        }

        private void FieldSize_3_OnClick(object s, MouseArgs e)
        {
            UserSettings.MapSize = 30;
        }

        private void Players_1_OnClick(object s, MouseArgs e)
        {
            UserSettings.Players = 2;
        }

        private void Players_2_OnClick(object s, MouseArgs e)
        {
            UserSettings.Players = 3;
        }

        private void Players_3_OnClick(object s, MouseArgs e)
        {
            UserSettings.Players = 4;
        }

        private void OK_OnClick(object s, MouseArgs e)
        {
            Program.aw.scene = new MainMenuScene();
        }

        public override void Draw(GameWindow gw)
        {
            dm.Draw();
            cdm.Draw();
        }

        public override void OnKeyDown(KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(KeyboardKeyEventArgs key) { }
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
            cdm.Update();
            dm.Update();
        }
    }
}
