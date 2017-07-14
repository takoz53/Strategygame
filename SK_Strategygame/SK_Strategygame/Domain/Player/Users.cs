using AGFXLib.Drawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SK_Strategygame.Gameplay.Field_Creation;

namespace SK_Strategygame.Domain.Player
{
    class Users : Drawable
    {
        List<Player> userList = new List<Player>();
        DrawManager dm;
        Random r = new Random();
        int randomPositionX, randomPositionY;

        public Users(int users)
        {
            dm = new DrawManager();
            for (int i = 0; i < users; i++)
            {
                randomPositionX = r.Next(1);
                randomPositionY = r.Next(1);
                userList.Add(new Player("Resources/InGame/Player/Playfigure_sold.png", new Coordinate(randomPositionX,randomPositionY), (randomPositionX + "" + randomPositionY)));
            }
        }

        public List<Player> getPlayers()
        {
            return userList;
        }

        public override void Draw(DrawManager parent)
        {

        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key)
        {
            
        }

        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key)
        {

        }

        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button)
        {

        }

        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button)
        {
            
        }
    }
}
