using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib.Drawables;
using OpenTK.Input;

namespace SK_Strategygame.UI
{
    class bScrollbar : Drawable
    {
        public Rect scrollbar_fg;
        public Rect scrollbar_bg;
        public Rect scrollwheel;
        public float x;
        public float y;

        public bScrollbar (int x, int y, int width, int height)
        {
            this.x = (float)x;
            this.y = (float)y;
            scrollbar_bg = new Rect(new Quad(x, y, width, height), "fill", new DrawColor(255, 255, 255));
            scrollbar_fg = new Rect(new Quad(x, y, width, height), "line", new DrawColor(255, 0, 0));
            scrollwheel = new Rect(new Quad(x, y, width, 100), "fill", new DrawColor(128, 128, 128));

        }

        public override void Draw(DrawManager parent)
        {
            scrollbar_bg.Draw(parent);
            scrollbar_fg.Draw(parent);
            scrollwheel.Draw(parent);
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button) { }
        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button) { }
    }
}
