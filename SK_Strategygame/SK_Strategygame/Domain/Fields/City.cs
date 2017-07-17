using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace SK_Strategygame.Gameplay.Field_Creation
{

    class City : Field
    {
        int wallPoints, upgradeLevel;
        bool barrackBuilt=false;
        String cityname;
        NotificationBox nb = new NotificationBox();

        public City(string cityname, string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {
            this.cityname = cityname;
            wallPoints = 0;
            upgradeLevel = 0;
        }

        /*public void upgradeWall()
        {
            
            if(upgradeLevel == 5)
            {
                nb.Notify("The wall is totally upgraded!", "Upgraded to last level!", NotificationBox.types.OKOnly);
            }
            else
            {
                if (player >= 100)
                {
                    //availableStones -= 100;
                    wallPoints = wallPoints + 50;
                    upgradeLevel++;
                }
                else
                    nb.NotifyStone();
            }
        }

        public void buildBarracks()
        {
            if (availableMoney >= 100 && availableWood >= 300)
            {
                availableMoney -= 100;
                availableWood -= 300;
                barrackBuilt = true;
            }
            else
                nb.NotifyWood();
        }

        public void createSoldiers(int amount)
        {
            if(barrackBuilt)
            {
                if(availableMoney >= 25 * amount && availableFood >= 50 * amount && availableWood >= 75 * amount) //50 Food & 25 Money & 75 Wood
                {
                    availableSoldiers += amount;
                    availableMoney -= 25 * amount;
                    availableWood -= 75 * amount;
                    availableFood -= 25 * amount;
                }
                else
                {
                    nb.Notify("You don't have enough Resources!", "Not enough Resources!", NotificationBox.types.OKOnly);
                }
            }
        }
        
    }
}
*/