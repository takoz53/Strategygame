using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib.Drawables;
using OpenTK.Input;
using AGFXLib;

namespace SK_Strategygame.UI
{
    class bCursor : Drawable
    {
        public Sprite cursorSprite;

        public bCursor ()
        {
            cursorSprite = new Sprite("Resources/Cursors/Cursor_Main.png");
        }

        public void SetCursor (string path)
        {
            cursorSprite.SetTexture(path);
        }

        public override void Draw(DrawManager parent)
        {
            cursorSprite.x = UserMouse.GetX();
            cursorSprite.y = UserMouse.GetY();
            cursorSprite.Draw(parent);
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button) { }
        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button) { }
    }
}
