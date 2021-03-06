﻿using AGFXLib;
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
        private Vertex2 coordinate;

        public Player(string path, Vertex2 coordinate, String id, int startMoney, int startWood, int startStone, int startFood) : base(path, (float)coordinate.x * 250 + 125, (float)coordinate.y * 250 + 125)
        {
            availableFood = startFood;
            availableMoney = startMoney;
            availableStone = startStone;
            availableWood = startWood;
            this.coordinate = coordinate;
            this.id = id;
        }

        public Vertex2 getCoordinate()
        {
            return coordinate;
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
