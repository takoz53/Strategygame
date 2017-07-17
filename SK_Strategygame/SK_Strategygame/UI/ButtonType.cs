using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame.UI
{
    class ButtonType
    {
        public const string Bazaar_Access = "btn_accessBazaar"; // _hover and .png will be auto
        public const string Bazaar_BuyFood = "btn_buyFood";
        public const string Bazaar_BuyStone = "btn_buyStones";
        public const string Bazaar_BuyWood = "btn_buyWood";
        public const string Bazaar_Exit = "btn_exitBazaar";
        public const string Conquer = "btn_conquerField";
        public const string CreateCity = "btn_createCity";
        public const string HarvestFood = "btn_harvestFood";
        public const string HarvestGold = "btn_harvestGold";
        public const string HarvestStone = "btn_harvestStones";
        public const string HarvestWood = "btn_harvestWood";
        
        public const string NextRound = "btn_nextround";

        public static string GetPath (string Name, bool Hover = false, bool Disabled = false)
        {
            if (Disabled)
                return "Resources/InGame/UIBottom/Buttons/" + Name + "_disabled.png";
            return "Resources/InGame/UIBottom/Buttons/" + Name + (Hover ? "_hover.png" : ".png");
        }
    }
}
