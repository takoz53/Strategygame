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
            String link1 = "Resources/InGame/Fields/fertile/Forests/largetest.png";
            String link2 = "Resources/InGame/Fields/infertile/Deserts/test.png";
            dm = new DrawManager();

            for (int x = 0;  x < playfieldsize; x++)
            {
                for(int y = 0; y < playfieldsize; y++)
                {
                    double fieldTypeRandom = r.NextDouble();

                    if(fieldTypeRandom < 0.16)
                    {
                        fields.Add(new City(randomCityNameGenerator(), link2, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom >= 0.16)
                    {
                        fields.Add(new Forest(link1, new Coordinate(x, y), (x + "" + y)));
                    }
                    
                }
            }
        }

        public List<Field> getPlayField()
        {
            return fields;
        }

        public String randomCityNameGenerator()
        {
            List<String> name1 = new List<String> { "Big", "Black", "Yellow", "Holy", "Windy", "Saint"};
            List<String> name2 = new List<String> { "Castle", "Rock", "Maria", "Stone" };
            return name1[r.Next(name1.Count)] + " " + name2[r.Next(name2.Count)];

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
