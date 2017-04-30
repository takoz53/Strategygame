using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SK_Strategygame.UI
{
    class bListbox : Drawable
    {
        public List<bListboxEntry> bArray = new List<bListboxEntry>();
        public Rect inline_rect;
        public Rect outline_rect;
        public bListboxScrollbar scrollbar;
        public float x;
        public float y;
        public float w;
        public float h;

        public bListbox ()
        {
            inline_rect = new Rect(new Quad(10, 10, 200, 400),"fill", new DrawColor(255,255,255));
            outline_rect = new Rect(new Quad(10, 10, 200, 400), "line", new DrawColor(0, 0, 255));
            scrollbar = new bListboxScrollbar();
            scrollbar.maxValue = 100;
            scrollbar.SetQuad(new Quad(210, 10, 16, 400));
            
            x = 10;
            y = 10;
            w = 200;
            h = 400;
        }

        public void SetQuad (Quad q)
        {
            inline_rect.x = q.x;
            inline_rect.y = q.y;
            inline_rect.w = q.w;
            inline_rect.h = q.h;
            outline_rect.x = q.x;
            outline_rect.y = q.y;
            outline_rect.w = q.w;
            outline_rect.h = q.h;
            x = q.x;
            y = q.y;
            w = q.w;
            h = q.h;
        }

        public void Add (bListboxEntry b)
        {
            bArray.Add(b);
        }

        public override void Draw(DrawManager parent)
        {
            inline_rect.Draw(parent);
            outline_rect.Draw(parent);
            scrollbar.Draw(parent);
            int i = 0;
            foreach (bListboxEntry b in bArray)
            {
                b.SetQuad(new Quad(x, y + (i*30) - scrollbar.scrollValue,w,30));
                b.Draw(parent);
                i++;
            }
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key)
        {
            scrollbar.OnKeyDown(parent, key);
            foreach (bListboxEntry b in bArray)
                b.OnKeyDown(parent, key);
        }

        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key)
        {
            scrollbar.OnKeyUp(parent, key);
            foreach (bListboxEntry b in bArray)
                b.OnKeyUp(parent, key);
        }

        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button)
        {
            scrollbar.OnMouseDown(parent, button);
            foreach (bListboxEntry b in bArray)
                b.OnMouseDown(parent, button);
        }

        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button)
        {
            scrollbar.OnMouseUp(parent, button);
            foreach (bListboxEntry b in bArray)
                b.OnMouseUp(parent, button);
        }
    }
}
