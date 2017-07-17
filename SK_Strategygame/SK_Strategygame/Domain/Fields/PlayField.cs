using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class PlayField
    {
        //Declarations
        public List<Field> fields = new List<Field>();
        Random r = new Random();
        public int playfieldsize;
        string forestlink = "Resources/InGame/Fields/fertile/Forests/forest_small.png";
        string desertlink = "Resources/InGame/Fields/infertile/Deserts/test.png";
        string pasturelink = "Resources/InGame/Fields/fertile/Pasture/pasture_small.png";
        string mountainlink = "Resources/InGame/Fields/infertile/Deserts/test.png";
        string sealink = "Resources/InGame/Fields/inpassable/Sea/Wasser.png";

        public PlayField(int playfieldsize)
        {
            this.playfieldsize = playfieldsize;
            for (int x = 0;  x < playfieldsize; x++)
            {
                for(int y = 0; y < playfieldsize; y++)
                {
                    double fieldTypeRandom = r.NextDouble();

                    if(fieldTypeRandom < 0.31) //30% Chance for Forests
                    {
                        fields.Add(new Forest(forestlink, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom > 0.30 && fieldTypeRandom < 0.41) //10% Chance for Seas
                    {
                        fields.Add(new Sea(sealink, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom >= 0.41 && fieldTypeRandom < 0.71) //30% Chance for Pasture
                    {
                        fields.Add(new Pasture(pasturelink, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom >= 0.71 && fieldTypeRandom < 0.96) //25% Chance for Mountains
                    {
                        fields.Add(new Mountain(mountainlink, new Coordinate(x, y), (x + "" + y)));
                    }
                    if(fieldTypeRandom >= 0.96 && fieldTypeRandom <= 1) //5% Chance for Deserts
                    {
                        fields.Add(new Desert(desertlink, new Coordinate(x, y), (x + "" + y)));
                    }
                }
            }
        }

        public List<Field> getPlayField()
        {
            return fields;
        }

        public int getSize()
        {
            return playfieldsize;
        }
    }
}
