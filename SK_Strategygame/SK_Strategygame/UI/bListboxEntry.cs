using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using AGFXLib;

namespace SK_Strategygame.UI
{
    class bListboxEntry : Clickable
    {
        public Rect highlight_box;
        public Text title_text;

        public bListboxEntry (string title)
        {
            highlight_box = new Rect(new Quad(1,2,3,4), "fill", new DrawColor(0, 0, 0, 20));
            title_text = new Text(title,Text.defaultFont,new DrawColor(0,0,0));
            
        }

        public void SetQuad (Quad q)
        {
            highlight_box.x = q.x;
            highlight_box.y = q.y;
            highlight_box.w = q.w;
            highlight_box.h = q.h;
            title_text.x = q.x + q.w / 2 - title_text.w / 2;
            title_text.y = q.y + q.h / 2 - title_text.h / 2;
            matrix = new Matrix(new Vertex2[]
            {
                new Vertex2(q.x,q.y),
                new Vertex2(q.x+q.w,q.y),
                new Vertex2(q.x+q.w,q.y+q.h),
                new Vertex2(q.x,q.y+q.h)
            });
        }

        public override void Draw(DrawManager parent)
        {
            base.Draw(parent);
            highlight_box.Draw(parent);
            if (highlight_box.matrix.TestCollision(new Vertex2(UserMouse.getX(),UserMouse.getY()))) {
                highlight_box.color.a += 4;
                if (highlight_box.color.a > 80)
                    highlight_box.color.a = 80;
            } else
            {
                highlight_box.color.a -= 4;
                if (highlight_box.color.a < 20)
                    highlight_box.color.a = 20;
            }
            title_text.Draw(parent);
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key)
        {
            base.OnKeyDown(parent, key);
        }

        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key)
        {
            base.OnKeyUp(parent, key);
        }

        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button)
        {
            base.OnMouseDown(parent, button);
        }

        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button)
        {
            base.OnMouseUp(parent, button);
        }
    }
}
