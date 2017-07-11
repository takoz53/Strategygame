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
        public Player(string path, Coordinate coordinate, String id) : base(path, coordinate.getX() * 250, coordinate.getY() * 250)
        {
            this.coordinate = coordinate;
            this.id = id;
        }

        public Coordinate getCoordinate()
        {
            return coordinate;
        }

        public void move()
        {
            //if(UserMouse.getX() == coordinate.getX() * 250 && UserMouse.getY() == coordinate.getY() * 250) //Too precised, won't work.
            //{
                coordinate.setX(UserMouse.getX());
                coordinate.setY(UserMouse.getY());
            //}
        }
    }
}
