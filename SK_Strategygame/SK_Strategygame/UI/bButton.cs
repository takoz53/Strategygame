using System;
using AGFXLib.Drawables;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using AGFXLib;

namespace SK_Strategygame.UI
{
    class bButton : Sprite
    {
        public string nHoverPath;
        public string HoverPath;
        public bool hoverSprite = false;

        public bButton(string path) : base(path)
        {
            nHoverPath = path;
            HoverPath = path;
        }

        public bButton(string path, string hoverPath) : base(path)
        {
            nHoverPath = path;
            HoverPath = hoverPath;
        }

        public override void Draw(DrawManager parent)
        {
            base.Draw(parent);
            if (isHovered())
            {
                if (hoverSprite == false)
                {
                    setTexture(HoverPath);
                    Console.WriteLine("Texture changed to hover (path: \"" + HoverPath + "\"");
                }
                hoverSprite = true;
            } else
            {
                if (hoverSprite)
                {
                    setTexture(nHoverPath);
                    Console.WriteLine("Texture changed to normal (path: \"" + nHoverPath + "\"");
                }
                hoverSprite = false;

            }
        }
    }
}
