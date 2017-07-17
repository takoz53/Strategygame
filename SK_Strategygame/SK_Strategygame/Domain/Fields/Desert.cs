using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
namespace SK_Strategygame.Gameplay.Field_Creation
{
    class Desert : Field
    {
        int bazaarlimit = 2; // 1xKaufen, 1xVerkaufen || 2x Kaufen || 2xVerkaufen pro Runde
        NotificationBox nb = new NotificationBox();
        public Desert(string path, Coordinate coordinate, string id) : base(path, coordinate, id)
        {

        }

        public enum products
        {
            Wood,
            Stone,
            Food
        }
        public void decreaseBazaarlimit()
        {
            if (bazaarlimit > 0)
                bazaarlimit -= 1;
            else
            {
                //Wait for Next round => Bazaarlimit = 2;
            }
        }

        /*public void buy(int amount, products product)
        {
            if(bazaarlimit > 0)
            {
                if (product == products.Food) //100 Money for 100 Food
                {
                    if (getAvailableMoney() >= 100)
                    {

                    }
                    else
                        nb.NotifyFood();
                }
                else if (product == products.Stone) //Harder to get Stones => 100 Money for 50 Stones
                {
                    if (getAvailableMoney() >= 100)
                    {
                        //Stoneamount += 50;
                        //AvailableMoney -= 100;
                    }
                    else
                        nb.NotifyStone();
                }
                else if (product == products.Wood) //100 Money for 100 Wood
                {
                    if (getAvailableMoney() >= 100)
                    {
                        //Woodamount += 100;
                        //AvailableMoney -= 100;
                    }
                    else
                        nb.NotifyWood();
                }
                decreaseBazaarlimit();
            }
            else
            {
                nb.Notify("You must wait a round to be able to buy new Resources!", "Cooldown for one round", NotificationBox.types.OKOnly);
            }

        }
    }
}
*/