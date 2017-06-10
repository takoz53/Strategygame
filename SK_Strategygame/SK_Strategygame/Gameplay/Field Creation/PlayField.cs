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
                switch (r.Next(0,5))
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

                    case 2:
                        Field q = new Field_Creation.Field("Resources/InGame/FieldsCities/Stadt.png");
                        q.x = i * q.w;
                        q.y = i * q.h;
                        fields.Add(q);
                        break;

                    case 3:
                        Field w = new Field_Creation.Field("Resources/InGame/Fields/fertile/Pasture/Pasture.png");
                        w.x = i * w.w;
                        w.y = i * w.h;
                        fields.Add(w);
                        break;

                    case 4:
                        Field e= new Field_Creation.Field("Resources/InGame/Fields/infertile/Mountains/Mountains.png");
                        e.x = i * e.w;
                        e.y = i * e.h;
                        fields.Add(e);
                        break;

                    case 5:
                        Field r = new Field_Creation.Field("Resources/InGame/Fields/inpassable/Water/Wasser.png");
                        r.x = i * r.w;
                        r.y = i * r.h;
                        fields.Add(r);
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
