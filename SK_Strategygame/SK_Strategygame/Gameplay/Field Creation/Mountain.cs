using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class Mountain : Field
    {
        int mountainSize;
        int stoneAmount;
        public Mountain(string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {
            Random rd = new Random();
            mountainSize = rd.Next(1, 3);
            stoneAmount = rd.Next(10, 100) * mountainSize;
        }

        public Boolean harvestFood(int amount) //Function to harvest Food
        {
            if (stoneAmount > amount)
            {
                stoneAmount -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
