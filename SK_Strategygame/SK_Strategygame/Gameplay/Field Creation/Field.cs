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

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class Field : Sprite
    {
        private Sprite image;
        private String id;
        private Coordinate coordinate;
        private List<Warrior> warriors;

        public Field(String path, Coordinate coordinate, String id): base(path, coordinate.getX()*200, coordinate.getY()*200)
        {
            this.coordinate = coordinate;
            this.id = id; 
        }

        public Coordinate getCoordinate()
        {
            return coordinate;
        }

  
    }
}
