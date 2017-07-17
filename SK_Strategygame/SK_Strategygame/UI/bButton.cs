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

        public bButton(string path) : base(path, 0, 0)
        {
            nHoverPath = path;
            HoverPath = path;
        }

        public bButton(string path, string hoverPath) : base(path, 0, 0)
        {
            nHoverPath = path;
            HoverPath = hoverPath;
        }

        public bButton(string path, string hoverPath, float x, float y) : base(path, 0, 0)
        {
            nHoverPath = path;
            HoverPath = hoverPath;
        }

        public override void Draw(DrawManager parent)
        {
            base.Draw(parent);
            if (isHovered(parent))
            {
                if (hoverSprite == false)
                {
                    setTexture(HoverPath);
                }
                hoverSprite = true;
            } else
            {
                if (hoverSprite)
                {
                    setTexture(nHoverPath);
                }
                hoverSprite = false;
            }
        }
    }
}
