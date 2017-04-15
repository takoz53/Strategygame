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
        public int index;
        public float px;
        public float py;
        public int pw;
        public int ph;

        public bListboxEntry (bListbox parent, string text)
        {
            px = parent.x;
            py = parent.y;
            pw = (int)parent.w;
            ph = (int)parent.h;
            this.text = new Text(text, new System.Drawing.Font(new System.Drawing.FontFamily("Tahoma"), 14), new DrawColor(0,0,0,255));
            border = new Rectangle(new Quad(0, 0, 1, 1), new DrawColor(128, 128, 128));
        }

        public override void Draw(DrawManager dm_parent)
        {
            border.x = px;
            border.y = py;
            border.w = pw - 16;
            border.h = text.height + 6;
            border.y += (index * border.h);
            text.x = (px + (border.w / 2) - (text.width / 2));
            text.y = (border.y + (border.h / 2) - (text.height / 2));
            border.Draw(dm_parent);
            text.Draw(dm_parent);
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button) { }
        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button) { }
    }
}
