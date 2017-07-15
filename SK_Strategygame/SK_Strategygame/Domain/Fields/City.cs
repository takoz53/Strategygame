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
        int availableWood, availableMoney, availableStones, availableFood;
        String cityname;
        NotificationBox nb = new NotificationBox();

        public City(string cityname, string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {
            this.cityname = cityname;
            wallPoints = 0;
        }

        public void upgradeWall(int upgradeAmount)
        {
            wallPoints = wallPoints + upgradeAmount;
        }

        public void buildBarracks()
        {
            if(availableMoney >= 100 && availableWood >= 300)
            {
                availableMoney -= 100;
                availableWood -= 300;
            }
            else
            {
                nb.Notify("You don't have enough wood!", "Not enough wood!", NotificationBox.types.OKOnly);
            }
        }
    }
}
