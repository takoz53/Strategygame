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
        public Desert(string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {

        }

        public void decreaseBasarlimit()
        {
            /*if(Kaufen, Verkaufen)
            {
            basarlimit -= 1;
             if(basarlimit == 0)
              {
               Kann nicht mehr kaufen, verkaufen für 1xRunde
              }
            }
            */
        }
    }
}
