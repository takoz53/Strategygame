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
        public FieldType fieldType;
        // Field Data
        public int Team = 0; // Team it belongs to. 0 = none.
        public int OccupiedByTeam = 0; // Team currently on tile. *Prevents other players moving on the tile.
        public int Food = 0; // Resources
        public int Wood = 0;
        public int Stone = 0;
        public int Gold = 0;
        public bool IsCity = false;
        public bool BarracksBuilt = false;
        public int WallPoints = 100; // Spawn cities with 100 HP
        public int WallLevel = 0;
        public int Soldiers = 0;

        public const int MaxWallLevel = 5;
        public const int WallUpgrade = 100; // We give 100 points for upgrading walls as it is limited. Soldiers can just be created over and over.
        public const int SoldierCreationRate = 25;

         /* Gold, Wood, Stone, Food */
        public readonly static int[] Resources_BuildBarracks = new int[] { 0, 50, 50, 50 }; // 3 Turns
        public readonly static int[] Resources_BuildWall = new int[] { 0, 50, 50, 0 }; // 2 Turns
        public readonly static int[] Resources_UpgradeWall = new int[] { 0, 50, 100, 0 }; // 3 Turns
        public readonly static int[] Resources_CreateSoldiers = new int[] { 0, 0, 0, 50 }; // 1 Turn

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
                    Wood = Program.rd.Next(10, 100) + ((randSize - 1) * 100);
                    break;
                case FieldType.Pasture:
                    Food = Program.rd.Next(10, 100) + ((randSize - 1) * 100);
                    break;
                case FieldType.Mountain:
                    Stone = Program.rd.Next(10, 100) + ((randSize - 1) * 100);
                    Gold = Program.rd.Next(10, 100) + ((randSize - 1) * 100);
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
