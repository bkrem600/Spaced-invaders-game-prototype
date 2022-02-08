using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class DefenderBullet : TokenGeneral
    {
        internal DefenderBullet(PointF startLocation)
        {
            location = startLocation;
            sprite = Image.FromFile(Directory.GetCurrentDirectory() + "\\bullet.bmp");
            bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);
        }

        internal override void Render(Graphics g)
        {
            g.DrawImage(sprite, bounds);
        }

        internal override void Step(double elapsed)
        {
            location.Y -= (float)(elapsed * Global.DefenderBulletSpeed);
            bounds.Y = (int)location.Y;
            Global.DefenderBulletFiring = (location.Y >= 0);
        }

        internal void Hit()
        {
            Global.DefenderBulletFiring = false;
        }
    }
}