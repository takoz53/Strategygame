using AGFXLib.Drawables;
using AGFXLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace SK_Strategygame.Domain.Player
{
    class Playfigure: Drawable
    {
        public Sprite player;
        public DrawManager dm = new DrawManager();

        //In dieser Klasse soll ein Objekt erstellt werden, den man auf dem Spielfeld, durch Drag-and-Drop bewegen kann
        public Playfigure ()
        { }

        //Hier wird entschieden wo ein Soldat oder ein Kommandant erstellt wird
        public void Soldier_or_not(bool _false_for_Commander,int x, int y)
        {
            if(_false_for_Commander==false)
            {
                player = new Sprite("Domain/Player/Playfigure_test.png",250,250);
                player.x = x;
                player.y = y;

            }
            else
            {
                player = new Sprite("Domain/Player/Playfigure_sold.png",250,250);
                player.x = x;
                player.y = y;
            }

        }
        
        public override void Draw(DrawManager parent)
        {
            dm.Draw();
        }

        public override void OnKeyDown(DrawManager parent, KeyboardKeyEventArgs key)
        {
        }

        public override void OnKeyUp(DrawManager parent, KeyboardKeyEventArgs key)
        {
        }

        public override void OnMouseDown(DrawManager parent, MouseButtonEventArgs button)
        {

            //Durch das anclicken des Sprites wird die Spielfigure etwas größer und kann bewegt sich mit dem Cursor
            player.x = UserMouse.GetX();
            player.y = UserMouse.GetY();
            player.w += 50;
            player.h += 50;
            player.Draw(parent);
        }

        public override void OnMouseUp(DrawManager parent, MouseButtonEventArgs button)
        {
        }
    }
}
