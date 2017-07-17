using AGFXLib.Scenes;
using AGFXLib.Drawables;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;
using SK_Strategygame.UI;
using System;
using AGFXLib;
using SK_Strategygame.Domain.Fields;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class Field : Sprite
    {
        private Vertex2 coordinate;
        private List<Warrior> warriors; // players?
        public FieldType fieldType;
        // Field Data
        public int Team = 0; // Team it belongs to. 0 = none.
        public int Food = 0; // Resources
        public int Wood = 0;
        public int Stone = 0;
        public bool IsCity = false;
        public bool BarracksBuilt = false;
        public int WallPoints = 0;
        public int Soldiers = 0;

        // What is the ID for?
        public Field(string path, Vertex2 coordinate, FieldType fieldType, int maxSize = 1): base("Resources/InGame/Fields/inpassable/Sea/water_small.png", (float)coordinate.x*250, (float)coordinate.y*250)
        {
            this.coordinate = coordinate;
            
            int randSize = Program.rd.Next(1, maxSize);
            Console.WriteLine(((FieldSize)randSize).ToString() + " was chosen for whatever.");
            string tFilename = path + ((FieldSize)randSize).ToString() + ".png";
            setTexture(tFilename);
            this.fieldType = fieldType;
            switch (fieldType)
            {
                case FieldType.Forest:
                    Wood = Program.rd.Next(10, 100) * randSize;
                    break;
                case FieldType.Pasture:
                    Food = Program.rd.Next(10, 100) * randSize;
                    break;
                case FieldType.Mountain:
                    Stone = Program.rd.Next(10, 100) * randSize;
                    break;
            }
        }

        public void SetOwner (int team)
        {
            Team = team;
        }

        public Vertex2 getCoordinate()
        {
            return coordinate;
        }
    }
}
