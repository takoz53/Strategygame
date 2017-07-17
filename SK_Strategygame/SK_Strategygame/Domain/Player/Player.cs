using AGFXLib;
using AGFXLib.Drawables;
using SK_Strategygame.Gameplay.Field_Creation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK_Strategygame.Domain.Player
{
    class Player : Sprite
    {
        private String id;
        private int availableMoney, availableWood, availableStone, availableFood;
        private Coordinate coordinate;
        private int x, y;
        public Player(string path, Coordinate coordinate, String id, int startMoney, int startWood, int startStone, int startFood) : base(path, coordinate.getX() * 250, coordinate.getY() * 250)
        {
            availableFood = startFood;
            availableMoney = startMoney;
            availableStone = startStone;
            availableWood = startWood;
            this.coordinate = coordinate;
            this.id = id;
        }

        public Coordinate getCoordinate()
        {
            return coordinate;
        }

        public void move(Coordinate coordinate, float x, float y)
        {
            this.coordinate = coordinate;
        }

        public int setLocationX(float x)
        {
            return this.x;
        }

        public int setLocationY(float x)
        {
            return this.y;
        }

        public int getCurrentWood()
        {
            return availableWood;
        }

        public int getCurrentFood()
        {
            return availableFood;
        }

        public int getCurrentMoney()
        {
            return availableMoney;
        }
        public int getCurrentStone()
        {
            return availableStone;
        }

        public void increaseWoodAmount(int amount)
        {
            availableWood += amount;
        }
        public void decreaseWoodAmount(int amount)
        {
            availableWood -= amount;
        }

        public void increaseMoneyAmount(int amount)
        {
            availableMoney += amount;
        }
        public void decreaseMoneyAmount(int amount)
        {
            availableMoney -= amount;
        }

        public void increaseStoneAmount(int amount)
        {
            availableStone += amount;
        }
        public void decreaseStoneAmount(int amount)
        {
            availableStone -= amount;
        }

        public void increaseFoodAmount(int amount)
        {
            availableFood += amount;
        }
        public void decreaseFoodAmount(int amount)
        {
            availableFood -= amount;
        }
    }
}
