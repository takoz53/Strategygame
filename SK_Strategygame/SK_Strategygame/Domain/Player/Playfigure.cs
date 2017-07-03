using AGFXLib.Drawables;
using AGFXLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SK_Strategygame.Domain.Player
{
    class Playfigure: Sprite
    {
        private String id;

        public Playfigure ( int x, int y, String id)
        {
            this.x = x;
            this.y = y;
            this.id = id;
        }

        public Coordinate getCoordinate()
        {
            return coordinate;
        }
    }
}
