using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class Forest : Field
    {
        int woodAmount; // size * (10,100) --> min 10, max 300
        int forestSize; //size level 1, 2, 3

        public Forest(string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {
            Random rd = new Random();
            forestSize = rd.Next(1, 3);
            woodAmount = rd.Next(10, 100) * forestSize;
        }

        public Boolean harvestWood(int amount) //Function to harvest Wood
        {
            if(woodAmount > amount)
            {
                woodAmount -= amount;
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}



