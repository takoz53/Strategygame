using System;
using AGFXLib.Drawables;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using AGFXLib;

namespace SK_Strategygame.UI
{


    public class bButton : Drawable
    {
        public int TextureID;
        public Bitmap texture;
        public DrawColor color = new DrawColor(255, 255, 255, 255);
        public float x = 0f;
        public float y = 0f;
        public int width = 0;
        public int height = 0;
        public bool beingHeld = false;
        public bool isBeingHovered = false;
        public bool hoveredImageSet = false;
        public string normalPath = "";
        public string hoverPath = "";

        private void GenerateTexture(int tID, string path)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            Bitmap bmp = new Bitmap(path);
            width = bmp.Width;
            height = bmp.Height;
            texture = bmp;
            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        }

        public bButton(string path, string hoverPath)
        {
            TextureID = GL.GenTexture();
            GenerateTexture(TextureID, path);
            normalPath = path;
            this.hoverPath = hoverPath;
        }

        public override void Draw(DrawManager parent)
        {
            float x1 = MatrixHelper.PixelConvert(x + parent.x, false);
            float y1 = MatrixHelper.PixelConvert(y + parent.y, true);
            float w = MatrixHelper.RawPixelConvert(width, false);
            float h = MatrixHelper.RawPixelConvert(height, true);
            float x2 = x1 + w;
            float y2 = y1 - h;
            if (!((x1 > 1 || y1 > 1) || (x2 < -1 || y2 < -1)))
            {
                GL.BindTexture(TextureTarget.Texture2D, TextureID);
                GL.Begin(PrimitiveType.Quads);
                GL.Color4(color.r, color.g, color.b, color.a); GL.TexCoord2(0, 0); GL.Vertex2(x1, y1);
                GL.Color4(color.r, color.g, color.b, color.a); GL.TexCoord2(1, 0); GL.Vertex2(x2, y1);
                GL.Color4(color.r, color.g, color.b, color.a); GL.TexCoord2(1, 1); GL.Vertex2(x2, y2);
                GL.Color4(color.r, color.g, color.b, color.a); GL.TexCoord2(0, 1); GL.Vertex2(x1, y2);
                GL.End();
                GL.BindTexture(TextureTarget.Texture2D, 0);
            }
            float tx = UserMouse.GetX();
            float ty = UserMouse.GetY();
            float sx = x;
            float sy = y;
            float sw = width;
            float sh = height;
            bool isHovered = (
                tx >= sx &&
                tx <= sx + sw &&
                ty >= sy &&
                ty <= sy + sh
            );
            isBeingHovered = isHovered;
            /*if (isBeingHovered)
            {
                Console.WriteLine("HELP, IM BEING TOUCHED INAPPROPRIATELY BY SENPAI");
            } else
            {
                Console.WriteLine("*Sniffles* Senpai, notice me~");
            }*/
            
            if (isBeingHovered && !hoveredImageSet)
            {
                SetTexture(hoverPath);
                hoveredImageSet = true;
            } else if (!isBeingHovered && hoveredImageSet)
            {
                SetTexture(normalPath);
                hoveredImageSet = false;
            }
        }

        public void SetTexture(string path)
        {
            GenerateTexture(TextureID, path);
        }

        public event EventHandler<SpriteClickArgs> OnClick;
        private void SparkOnClick(OpenTK.Input.MouseButtonEventArgs b)
        {
            OnClick?.Invoke(this, new SpriteClickArgs(b));
        }

        public override void OnKeyDown(DrawManager parent, OpenTK.Input.KeyboardKeyEventArgs key) { }
        public override void OnKeyUp(DrawManager parent, OpenTK.Input.KeyboardKeyEventArgs key) { }
        public override void OnMouseDown(DrawManager parent, OpenTK.Input.MouseButtonEventArgs button)
        {
            beingHeld = false;
            OpenTK.Input.MouseButtonEventArgs MS = button;
            float tx = MatrixHelper.MousePixelConvert((float)MS.X, false);
            float ty = MatrixHelper.MousePixelConvert((float)MS.Y, true);
            float tx2 = UserMouse.GetX();
            float ty2 = UserMouse.GetY();
            float sx = x;
            float sy = y;
            float sw = width;
            float sh = height;
            //Console.WriteLine("Calc for: " + normalPath);
            //Console.WriteLine(tx + " v " + tx2 + " && " + ty + " v " + ty2);
            //Console.WriteLine(tx + "," + ty + "," + sx + "," + sy + "," + sw + "," + sh);
            bool isHovered = (
                tx >= sx &&
                tx <= sx + sw &&
                ty >= sy &&
                ty <= sy + sh
            );
            if (isHovered)
                beingHeld = true;
        }
        public override void OnMouseUp(DrawManager parent, OpenTK.Input.MouseButtonEventArgs button)
        {
            OpenTK.Input.MouseButtonEventArgs MS = button;
            float tx = MatrixHelper.MousePixelConvert((float)MS.X, false);
            float ty = MatrixHelper.MousePixelConvert((float)MS.Y, true);
            float sx = x;
            float sy = y;
            float sw = width;
            float sh = height;
            bool isHovered = (
                tx >= sx &&
                tx <= sx + sw &&
                ty >= sy &&
                ty <= sy + sh
            );
            if (beingHeld && isHovered)
                SparkOnClick(button);
            beingHeld = false;
        }
    }
}
