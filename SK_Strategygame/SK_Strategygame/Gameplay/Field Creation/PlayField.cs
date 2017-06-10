using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class PlayField : Drawable
    {
        //Declaration and saving fuck
        public List<Field> fields = new List<Field>();
        Random r = new Random();
        DrawManager dm;
        

        public PlayField(int playfieldsize)
        {
            dm = new DrawManager();
         for(int i = 0; i < playfieldsize; i++)
            {
                switch (r.Next(0,1))
                {
                    default:
                        break;

                    case 0:
                            Field f = new Field("Resources/InGame/Fields/fertile/Forests/largetest.png");
                            f.x = i * f.w;
                            f.y = i * f.h;
                        fields.Add(f);
                        break;

                    case 1: Field x = new Field_Creation.Field("Resources/InGame/Fields/infertile/Deserts/test.png");
                        x.x = i * x.w;
                        x.y = i * x.h;
                        fields.Add(x);
                        break;
                }
            }
        }

        public override void Draw(DrawManager parent)
        {
            foreach(Field f in fields)
            {
                dm.Add(f);
            }
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key)
        {

        }

        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key)
        {

        }

        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button)
        {

        }

        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button)
        {

        }
    }
}
