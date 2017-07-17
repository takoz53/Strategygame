using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SK_Strategygame.Gameplay.Field_Creation;
using SK_Strategygame.Scenes.InGame;

namespace SK_Strategygame.Domain.Player
{
    class Users
    {
        List<Player> userList = new List<Player>();
        Random r = new Random();
        int randomPositionX, randomPositionY;

        public Users(int users)
        {
            for (int i = 0; i < users; i++)
            {
                if(users == 2)
                {
                    if(i == 0)
                    {
                        randomPositionX = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                        randomPositionY = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                    }
                    else if(i == 1)
                    {
                        randomPositionX = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                        randomPositionY = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                    }
                }
                else if(users == 3)
                {
                    if (i == 0)
                    {
                        randomPositionX = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                        randomPositionY = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                    }
                    else if (i == 1)
                    {
                        randomPositionX = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                        randomPositionY = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                    }
                    else if (i == 2)
                    {
                        randomPositionX = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                        randomPositionY = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                    }
                }
                else if (users == 4)
                {
                    if (i == 0)
                    {
                        randomPositionX = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                        randomPositionY = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                    }
                    else if (i == 1)
                    {
                        randomPositionX = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                        randomPositionY = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                    }
                    else if (i == 2)
                    {
                        randomPositionX = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                        randomPositionY = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                    }
                    else if (i == 3)
                    {
                        randomPositionX = r.Next(Convert.ToInt32((Math.Ceiling(GameScene.pfSize * 0.7))), GameScene.pfSize);
                        randomPositionY = r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.pfSize * 0.3)));
                    }
                }
                userList.Add(new Player("Resources/InGame/Player/Playfigure_sold.png", new Coordinate(randomPositionX,randomPositionY), (randomPositionX + "" + randomPositionY)));
            }
        }

        public List<Player> getPlayers()
        {
            return userList;
        }
    }
}
