using AGFXLib;
using AGFXLib.Drawables;
using OpenTK.Input;

namespace SK_Strategygame.UI
{
    class bCursor : Drawable
    {
        public Sprite cursorSprite;
        public string lastSprite = "";
        public enum cursortype
        {
            attack,
            attackConfirm,
            build,
            buildConfirm,
            main,
            mainHover,
            scroll
        }

        public bCursor ()
        {
            cursorSprite = new Sprite("Resources/Cursors/Cursor_Main.png", 0, 0);
        }

        public void SetCursor (cursortype type)
        {
            if (type == cursortype.attack)
                SetCursor("Resources/Cursors/Cursor_Attack.png");
            else if (type == cursortype.attackConfirm)
                SetCursor("Resources/Cursors/Cursor_Attack_Confirm.png");
            else if (type == cursortype.build)
                SetCursor("Resources/Cursors/Cursor_Build.png");
            else if (type == cursortype.buildConfirm)
                SetCursor("Resources/Cursors/Cursor_Build_Confirm.png");
            else if (type == cursortype.main)
                SetCursor("Resources/Cursors/Cursor_Main.png");
            else if (type == cursortype.mainHover)
                SetCursor("Resources/Cursors/Cursor_Main_Hover.png");
            else if (type == cursortype.scroll)
                SetCursor("Resources/Cursors/Cursor_Scroll.png");
        }

        private void SetCursor (string path)
        {
            if (path != lastSprite)
                cursorSprite.setTexture(path);
            lastSprite = path;
        }

        public override void Draw(DrawManager parent)
        {
            cursorSprite.x = UserMouse.getX()+parent.x;
            cursorSprite.y = UserMouse.getY()+parent.y;
            cursorSprite.Draw(parent);
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button) { }
        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button) { }
    }
}
