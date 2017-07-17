using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SK_Strategygame.Domain.Fields;
/*
namespace SK_Strategygame.Gameplay.Field_Creation
{
    class Pasture : Field
    {
        int foodAmount;
        int pastureSize;

        public Pasture(string xpath, Coordinate coordinate, string id, string path) : base(xpath, coordinate, id)
        {
            Random rd = new Random();
            pastureSize = rd.Next(1, 1);
            foodAmount = rd.Next(10, 100) * pastureSize;
            string filename = "pasture" + ((FieldSize)pastureSize).ToString() + ".png";
            setTexture(path + filename);
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
        public int getPastureSize()
        {
            return pastureSize;
        }
        public void setPastureSize(int pastureSize)
        {
            this.pastureSize = pastureSize;
        }
    }
}
*/