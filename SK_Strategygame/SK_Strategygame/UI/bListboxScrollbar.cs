using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib;
using AGFXLib.Drawables;
using OpenTK.Input;

namespace SK_Strategygame.UI
{
    class bListboxScrollbar : Drawable
    {
        public Rect outline_rect; // I need more cat food brb.
        public Rect bg_rect;
        public Rect scrollbar_rect;

        public int scrollValue = 0;
        public int maxValue = 0;
        public double pixelScale = 0;
        public int maxScroll = 0;
        public bool isGrabbed = false;
        public Vertex2 anchor;

        public bListboxScrollbar ()
        {
            outline_rect = new Rect(new Quad(1, 2, 3, 4), "line", new DrawColor(0, 0, 255));
            bg_rect = new Rect(new Quad(1, 2, 3, 4), "fill", new DrawColor(95, 95, 128, 255));
            scrollbar_rect = new Rect(new Quad(1, 2, 3, 4), "fill", new DrawColor(255, 255, 255));
        }

        public void SetQuad (Quad q)
        {
            outline_rect.x = q.x;
            outline_rect.y = q.y;
            outline_rect.w = q.w;
            outline_rect.h = q.h;
            bg_rect.x = q.x;
            bg_rect.y = q.y;
            bg_rect.w = q.w;
            bg_rect.h = q.h;
            scrollbar_rect.x = q.x;
            scrollbar_rect.w = q.w;
            // height calculations
            // start from max height, so... q.h,
            // q.h * (q.h / (q.h+maxValue))
            scrollbar_rect.h = q.h * (q.h / (q.h + maxValue));
            Console.WriteLine("Setting H to: (" + q.h + " * (" + q.h + " / (" + q.h + " + " + maxValue + ")) (" + scrollbar_rect.h + ")");
            scrollbar_rect.y = q.y;
            maxScroll = (int) q.h - (int) scrollbar_rect.h;
            scrollValue = 0;
            pixelScale = (q.h - scrollbar_rect.h) / maxValue;
        }

        public override void Draw(DrawManager parent)
        {
            if (isGrabbed)
            {
                float dy = UserMouse.getY() - (float)anchor.y;
                if (dy < 0)
                    dy = 0;
                if (dy > maxScroll)
                    dy = maxScroll;
                scrollbar_rect.y = outline_rect.y + dy;
                scrollValue = (int)Math.Floor(dy * pixelScale);
                Console.WriteLine("Setting sv to: " + scrollValue);
            }
            bg_rect.Draw(parent);
            outline_rect.Draw(parent);
            scrollbar_rect.Draw(parent);
            
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key)
        {
        }

        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key)
        {
            
        }

        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button)
        {
            if (scrollbar_rect.matrix.TestCollision(new Vertex2(UserMouse.getX(), UserMouse.getY())))
            {
                
                anchor = new Vertex2(UserMouse.getX(), UserMouse.getY() - (scrollbar_rect.y - outline_rect.y));
                isGrabbed = true;
            }
        }

        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button)
        {
            isGrabbed = false; 
        }
    }
}
