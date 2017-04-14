using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib.Drawables;
using OpenTK.Input;

namespace SK_Strategygame.UI
{
    class bListboxEntry : Drawable
    {
        public Rectangle border;
        public Text text;
        public bListbox parent;
        public int index;

        public bListboxEntry (bListbox parent, string text)
        {
            this.parent = parent;
            this.text = new Text(text);
            border = new Rectangle(new Quad(0, 0, 1, 1), new DrawColor(128, 128, 128));
        }

        public override void Draw(DrawManager dm_parent)
        {
            border.x = parent.x;
            border.y = parent.y;
            border.w = parent.w;
            border.h = text.height + 6;
            border.y += (index * border.h);
            text.x = (parent.x + (parent.w / 2) - (text.width / 2));
            text.y = (parent.y + (parent.y / 2) - (text.height / 2));
            border.Draw(dm_parent);
            text.Draw(dm_parent);
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button) { }
        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button) { }
    }
}
