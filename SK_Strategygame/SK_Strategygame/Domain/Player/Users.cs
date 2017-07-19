using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SK_Strategygame.Gameplay.Field_Creation;
using SK_Strategygame.Scenes.InGame;
using AGFXLib;

namespace SK_Strategygame.Domain.Player
{
    class Users
    {
        List<Player> userList = new List<Player>();
        Random r = new Random();
        int randomPositionX, randomPositionY;
        const string User1 = "Resources/InGame/Player/player_red.png";
        const string User2 = "Resources/InGame/Player/player_blue.png";
        const string User3 = "Resources/InGame/Player/player_brown.png";
        const string User4 = "Resources/InGame/Player/player_yellow.png";

        public int GetRandomPos (float weight, bool FromRight = false)
        {
            if (!FromRight)
                return r.Next(0, Convert.ToInt32(Math.Ceiling(GameScene.TilemapSize * weight)));
            return r.Next(Convert.ToInt32(Math.Ceiling(GameScene.TilemapSize * weight)), GameScene.TilemapSize);
        }

        public void GetRandomPosXY (float w1, bool fr1, float w2, bool fr2)
        {
            // This will not work properly for small maps beware.
            while (true)
            {
                randomPositionX = GetRandomPos(w1, fr1);
                randomPositionY = GetRandomPos(w2, fr2);
                bool notWater = false;
                foreach (Field f in GameScene.GameTilemap)
                {
                    if ((int)Math.Floor(f.getCoordinate().x) == randomPositionX && (int)Math.Floor(f.getCoordinate().y) == randomPositionY)
                    {
                        if (f.fieldType != Fields.FieldType.Sea)
                        {
                            notWater = true;
                            break;
                        }
                    }
                }
                if (notWater)
                    break;
            }
        }

        public Users(int users)
        {
            for (int i = 0; i < users; i++)
            {
                if(users == 2)
                {
                    if(i == 0)
                    {
                        GetRandomPosXY(0.3f, false, 0.3f, false);
                    }
                    else if(i == 1)
                    {
                        GetRandomPosXY(0.7f, false, 0.7f, false);
                    }
                }
                else if(users == 3)
                {
                    if (i == 0)
                    {
                        GetRandomPosXY(0.3f, false, 0.3f, false);
                    }
                    else if (i == 1)
                    {
                        GetRandomPosXY(0.7f, true, 0.7f, true);
                    }
                    else if (i == 2)
                    {
                        GetRandomPosXY(0.3f, false, 0.7f, true);
                    }
                }
                else if (users == 4)
                {
                    if (i == 0)
                    {
                        GetRandomPosXY(0.3f, false, 0.3f, false);
                    }
                    else if (i == 1)
                    {
                        GetRandomPosXY(0.7f, true, 0.7f, true);
                    }
                    else if (i == 2)
                    {
                        GetRandomPosXY(0.3f, false, 0.7f, true);
                    }
                    else if (i == 3)
                    {
                        GetRandomPosXY(0.7f, true, 0.3f, false);
                    }
                }
                if (i == 0) 
                    userList.Add(new Player(User1, new Vertex2(randomPositionX, randomPositionY), (randomPositionX + "" + randomPositionY), 100, 100, 100, 100));
                else if (i == 1)
                    userList.Add(new Player(User2, new Vertex2(randomPositionX, randomPositionY), (randomPositionX + "" + randomPositionY), 100, 100, 100, 100));
                else if (i == 2)
                    userList.Add(new Player(User3, new Vertex2(randomPositionX, randomPositionY), (randomPositionX + "" + randomPositionY), 100, 100, 100, 100));
                else if (i == 3)
                    userList.Add(new Player(User4, new Vertex2(randomPositionX, randomPositionY), (randomPositionX + "" + randomPositionY), 100, 100, 100, 100));
            }
        }

        public List<Player> getPlayers()
        {
            return userList;
        }
    }
}
