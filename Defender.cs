using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Defender : TokenGeneral
    {
        bool exploding = false;
        bool dead = false;

        internal bool Dead { get { return dead; } }

        internal Defender(PointF startLocation)
        {
            location = startLocation;

            sprite = Image.FromFile(Directory.GetCurrentDirectory() + "\\defender.bmp");

            bounds = new Rectangle((int)location.X, (int)location.Y, sprite.Size.Width, sprite.Size.Height);

            imgAttr.SetColorKey(Color.DarkSlateGray, Color.DarkSlateGray);
        }

        internal override void Render(Graphics g)
        {
			if (exploding)
            {
				sprite = Bitmap.FromFile(Directory.GetCurrentDirectory() + "\\explosion.bmp");
				g.DrawImage(sprite, bounds);
				exploding = false;
			}
			else
            {
				sprite = Bitmap.FromFile(Directory.GetCurrentDirectory() + "\\defender.bmp");
				g.DrawImage(sprite, bounds);
			}
        }

		internal override void Step(double elapsed)
		{
			switch (Global.DefenderDirection)
			{
				case Directions.Left:
					location.X -= (float)(elapsed * Global.DefenderSpeed);
					if (location.X < 0)
					{
						location.X = 0;
						Global.DefenderDirection = Directions.None;
					}
					bounds.X = (int)location.X;
					break;

				case Directions.Right:
					location.X += (float)(elapsed * Global.DefenderSpeed);
					if (location.X + Global.DefenderSize > Global.FormSize.Width)
					{
						location.X = Global.FormSize.Width - Global.DefenderSize;

						Global.DefenderDirection = Directions.None;
					}
					bounds.X = (int)location.X;
					break;

				case Directions.None:
					Global.DefenderDirection = Directions.None;
					break;
			}
		}

		internal PointF GetDefenderBulletStartLocation()
		{
			return new PointF(location.X + (sprite.Width / 2) - (Global.BulletSize.Width / 2), location.Y);
		}

        internal void CheckForCollision(InvaderBullet invaderBullet)
        {
			if (invaderBullet.Bounds.IntersectsWith(Bounds)) // not sure
			{
				dead = true;
				exploding = true;
				Global.DefenderDirection = Directions.None;
				Global.LivesRemaining -= 1;
				if (Global.LivesRemaining == 0)
                {
					Global.GameOver = true;
				}
				invaderBullet.Hit();
				return;
			}
		}

		internal void CheckForUFOCollision(UFOBullet ufoBullet)
		{
			if (ufoBullet.Bounds.IntersectsWith(Bounds)) // not sure
			{
				dead = true;
				exploding = true;
				Global.DefenderDirection = Directions.None;
				Global.LivesRemaining -= 1;
				if (Global.LivesRemaining == 0)
				{
					Global.GameOver = true;
				}
				ufoBullet.Hit();
				return;
			}
		}
	}
}