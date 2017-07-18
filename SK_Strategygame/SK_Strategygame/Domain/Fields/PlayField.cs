using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SK_Strategygame.Domain.Fields;
using AGFXLib;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class PlayField
    {
        //Declarations
        public List<Field> fields = new List<Field>();
        Random r = new Random();
        public int playfieldsize;
        string forestlink = "Resources/InGame/Fields/fertile/Forests/forest";
        string desertlink = "Resources/InGame/Fields/infertile/Deserts/desert";
        string pasturelink = "Resources/InGame/Fields/fertile/Pasture/pasture";
        string mountainlink = "Resources/InGame/Fields/infertile/Mountains/mountain";
        string sealink = "Resources/InGame/Fields/inpassable/Sea/water";

        public PlayField(int playfieldsize)
        {
            this.playfieldsize = playfieldsize;
            for (int x = 0;  x < playfieldsize; x++)
            {
                for(int y = 0; y < playfieldsize; y++)
                {
                    double fieldTypeRandom = r.NextDouble();

                    if (fieldTypeRandom <= 0.30) //30% Chance for Forests 0 - 0.3 incl;
                        fields.Add(new Field(forestlink, new Vertex2(x, y), FieldType.Forest, 3));
                    else if(fieldTypeRandom > 0.30 && fieldTypeRandom <= 0.41) //10% Chance for Seas
                        fields.Add(new Field(sealink, new Vertex2(x, y), FieldType.Sea,3));
                    else if(fieldTypeRandom > 0.41 && fieldTypeRandom <= 0.71) //30% Chance for Pasture
                        fields.Add(new Field(pasturelink, new Vertex2(x, y), FieldType.Pasture,3));
                    else if( fieldTypeRandom > 0.71 && fieldTypeRandom <= 0.96) //25% Chance for Mountains
                        fields.Add(new Field(mountainlink, new Vertex2(x, y), FieldType.Mountain,3));
                    else if(fieldTypeRandom > 0.96 && fieldTypeRandom <= 1) //5% Chance for Deserts
                        fields.Add(new Field(desertlink, new Vertex2(x, y), FieldType.Desert));
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
