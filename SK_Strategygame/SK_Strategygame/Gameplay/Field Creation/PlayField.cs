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
            string forest = "Resources/InGame/Fields/fertile/Forests/largetest.png";
            string desert = "Resources/InGame/Fields/infertile/Deserts/test.png";
            dm = new DrawManager();

            for (int x = 0;  x < playfieldsize; x++)
            {
                for(int y = 0; y < playfieldsize; y++)
                {
                    double fieldTypeRandom = r.NextDouble();

                    if(fieldTypeRandom < 0.31) //30% Chance for Forests
                    {
                        fields.Add(new Forest(forest, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom > 0.30 && fieldTypeRandom < 0.41) //10% Chance for Deserts
                    {
                        fields.Add(new Desert(desert, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom >= 0.41 && fieldTypeRandom < 0.71) //30% Chance for Pasture
                    {
                        fields.Add(new Pasture(desert, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom >= 0.71 && fieldTypeRandom < 0.96) //25% Chance for Mountains
                    {
                        fields.Add(new Mountain(desert, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom >= 0.96 && fieldTypeRandom <= 1) //5% Chance for Sea
                    {
                        fields.Add(new Pasture(desert, new Coordinate(x, y), (x + "" + y))); 
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
