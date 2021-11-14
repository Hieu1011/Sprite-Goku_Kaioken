using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SongokuFrm
{
    internal class Fire
    {
        private Bitmap sprite_Right, sprite_Left;
        public int indexRight, indexLeft, x, y;
        public int curFrameColumn;
        public int curFrameRow;
        public Fire()
        {
            sprite_Right = new Bitmap("Sprite_Right.png");
            sprite_Left = new Bitmap("Sprite_Left.png");
            indexRight = 0;
            indexLeft = 4;
        }
        public void setRight()
        {
            x += 8;

            if (indexRight >= 25)
                indexRight = 0;
            else
                indexRight++;
        }
        public void Render_R(Bitmap backBuffer, Graphics g)
        {
            g = Graphics.FromImage(backBuffer);
            g.Clear(Color.White);
            //g.SmoothingMode = SmoothingMode.AntiAlias;

            curFrameColumn = indexRight % 5;
            curFrameRow = indexRight / 5;

            g.DrawImage(sprite_Right, x, y, new Rectangle(curFrameColumn * 64, curFrameRow * 64, 64, 64), GraphicsUnit.Pixel);
            //g.Dispose();
            
            setRight();
        }
        private void setLeft()
        {
            x -= 8;
            if (indexLeft >= 25)
                indexLeft = 4;
            else if (indexLeft % 5 == 0)
                indexLeft += 9;
            else
                indexLeft--;
        }
        public void Render_L(Bitmap backBuffer, Graphics g)
        {
            g = Graphics.FromImage(backBuffer);
            g.Clear(Color.White);
            //g.SmoothingMode = SmoothingMode.AntiAlias;

            curFrameColumn = indexLeft % 5;
            curFrameRow = indexLeft / 5;

            g.DrawImage(sprite_Left, x, y, new Rectangle(curFrameColumn * 64, curFrameRow * 64, 64, 64), GraphicsUnit.Pixel);
            //g.Dispose();

            setLeft();
        }

    }
}
