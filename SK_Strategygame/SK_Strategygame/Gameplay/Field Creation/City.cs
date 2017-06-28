using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class City : Field
    {
        int wallPoints;
        String cityname;

        public City(string cityname, string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {
            this.cityname = cityname;
            wallPoints = 0;
        }

        public void upgradeWall(int upgradeAmount)
        {
            wallPoints = wallPoints + upgradeAmount;
        }
    }
}
