using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGFXLib.Drawables;
using OpenTK.Input;

namespace SK_Strategygame.UI
{
    class bListbox : Drawable
    {
        public Rectangle rectangle_border;
        public Rectangle rectangle_background;
        public Rectangle selection_rectangle;
        public List<bListboxEntry> entries;
        public int selectedIndex;
        public float x;
        public float y;
        public float w;
        public float h;

        public bListbox ()
        {
            
        }

        public override void Draw(DrawManager parent)
        {
            
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button) { }
        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button) { }
    }
}
