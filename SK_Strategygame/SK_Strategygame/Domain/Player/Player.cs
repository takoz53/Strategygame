using AGFXLib;
using AGFXLib.Drawables;
using SK_Strategygame.Gameplay.Field_Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame.Domain.Player
{
    class Player : Sprite
    {
        private String id;
        private Coordinate coordinate;
        private int x, y;
        public Player(string path, Coordinate coordinate, String id) : base(path, coordinate.getX() * 250, coordinate.getY() * 250)
        {
            this.coordinate = coordinate;
            this.id = id;
        }

        public Coordinate getCoordinate()
        {
            return coordinate;
        }

        public void move(Coordinate coordinate, float x, float y)
        {
            this.coordinate = coordinate;
        }

        public int setLocationX(float x)
        {
            return this.x;
        }

        public int setLocationY(float x)
        {
            return this.y;
        }
    }
}
