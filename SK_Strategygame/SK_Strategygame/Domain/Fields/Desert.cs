using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame.Gameplay.Field_Creation
{
    class Desert : Field
    {
        int basarlimit = 2; // 1xKaufen, 1xVerkaufen || 2x Kaufen || 2xVerkaufen pro Runde
        bool ready = true;
        public Desert(string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {

        }

        public void decreaseBasarlimit()
        {
            if (basarlimit == 2 || basarlimit == 1)
            {
                basarlimit -= 1;
                //Kann nicht mehr kaufen, verkaufen für 1xRunde
            }
            else
            {
                ready = false;
            }
        }

        public void readyBasarlimit()
        {
            basarlimit = 2;
            ready = true;
        }

        public int preis (int amount, string product)
        {
            
            switch(product)
            {
                case "Holz":
                    //GesammtPreis = amount*Preis
                    return 0;//GesammtPreis

                case "Stein":
                    return 0;

                case "Nahrung":
                    return 0;

                default:
                    return 0;
            }
        }
    }
}
