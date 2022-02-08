using System.Drawing;
using System.IO;


namespace SpaceInvaders
{
    internal class UFO : TokenGeneral
    {
        public int health = 3; 
        public bool dead = false;

        internal UFO(PointF startLocation)
        {
            location = startLocation;


            sprite = Image.FromFile(Directory.GetCurrentDirectory() + "\\UFO.bmp");

            bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);

        }
        internal override void Render(Graphics g)
        {
            if (!dead)
            {
                g.DrawImage(sprite, bounds);
            }
        }

        internal override void Step(double elapsed)
        {
            bounds.X = (int)location.X;
        }

        internal PointF GetUFOBulletStartLocation()
        {
            return new PointF(location.X + (sprite.Width / 2) - (Global.BulletSize.Width / 2), location.Y);
        }

        internal void HitByDefenderBullet()
        {
            health -= 1;
            if (health == 0)
            {
                dead = true;
                Global.Score += 20;
            }
        }
    }
}