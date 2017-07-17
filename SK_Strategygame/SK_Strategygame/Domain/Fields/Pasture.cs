using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class Pasture : Field
    {
        int foodAmount;
        int pastureSize;

        public Pasture(string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {
            Random rd = new Random();
            pastureSize = rd.Next(1, 3);
            foodAmount = rd.Next(10, 100) * pastureSize;
        }

        public Boolean harvestFood(int amount) //Function to harvest Food
        {
            if (foodAmount > amount)
            {
                foodAmount -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
