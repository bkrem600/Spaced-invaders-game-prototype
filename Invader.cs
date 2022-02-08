using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Invader : TokenGeneral
    {

        public bool dead = false;

        internal Invader(PointF startLocation)
        {
            location = startLocation;

            sprite = Image.FromFile(Directory.GetCurrentDirectory() + "\\invader.bmp");

            bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);

        }

        internal override void Step(double elapsed)
        {

            switch (Global.InvaderDirection)
            {
                case Directions.Left:
                    location.X -= (float)(elapsed * Global.InvaderSpeed);

                    bounds.X = (int)location.X;
                    break;


                case Directions.Right:

                    location.X += (float)(elapsed * Global.InvaderSpeed);

                    bounds.X = (int)location.X;
                    break;

                // still needs work
                case Directions.Down:
                    location.Y += (float)(elapsed * Global.InvaderSpeed);
                    bounds.Y = (int)location.Y;
                    break;

                default:
                    throw new Exception("Invalid direction for invader!");
            }
        }

        internal PointF GetInvaderBulletStartLocation()
        {
            return new PointF(location.X + (sprite.Width / 2) - (Global.BulletSize.Width / 2), location.Y);
        }

        internal override void Render(Graphics g)
        {
            if (!dead)
            {
                g.DrawImage(sprite, bounds);
            }

        }

        internal void HitByDefenderBullet()
        {
            dead = true;
            Global.Score += 10;
        }
    }
}
