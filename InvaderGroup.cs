using System;
using System.Drawing;

namespace SpaceInvaders
{
    class InvaderGroup
    {
        private Invader[,] invaders;
        private int leftMostInvader, rightMostInvader;
		Random random = new Random();
		public double downTime = 1;

		internal InvaderGroup(int cols, int rows)
		{
			invaders = new Invader[cols, rows];

			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					// bring back separation
					PointF location = new Point(x * (Global.InvaderSize.Width) + 10,
						y * (Global.InvaderSize.Height) + 200);

					invaders[x, y] = new Invader(location);
				}
			}
			leftMostInvader = 0;
			rightMostInvader = invaders.GetLength(0);

			Global.InvaderDirection = Directions.Right;
		}

		internal void Step(double elapsed)
		{
			foreach (Invader invader in invaders)
			{
				invader.Step(elapsed);
			}

			CheckInvaderDirection();
		}

		internal void Render(Graphics graphics)
		{
			foreach (Invader invader in invaders)
			{
				invader.Render(graphics);
			}
		}

		private void CheckInvaderDirection()
		{
			if (Global.InvaderDirection == Directions.Left)
			{
				for (int y = 0; y < invaders.GetLength(1); y++)
				{
					Invader invader = invaders[leftMostInvader, y];
					if (invader.Location.X <= 10)
					{
						MoveInvadersDown();
						Global.InvaderDirection = Directions.Right;
						break;
					}
				}
			}
			else
			{
				for (int y = 0; y < invaders.GetLength(1); y++)
				{
					Invader invader = invaders[rightMostInvader - 1, y];

					if (invader.Location.X + Global.InvaderSize.Width >= Global.FormSize.Width - 10)
					{
						if (downTime % 100 != 0)
                        {
							MoveInvadersDown();
                        }
						else
                        {
							Global.InvaderDirection = Directions.Left;
							downTime = 1;
							break;
						}
					}
				}
			}
		}

		internal void MoveInvadersDown()
        {
			Global.InvaderDirection = Directions.Down;
			downTime += 1;
        }

		internal void CheckForCollision(DefenderBullet defenderBullet)
		{
			foreach (Invader invader in invaders)
			{
				if (!invader.dead && defenderBullet.Bounds.IntersectsWith(invader.Bounds))
				{
					invader.HitByDefenderBullet();
					defenderBullet.Hit();
					return;
				}
			}
		}

		// check for defender collision
		internal void CheckForDefenderCollision(Defender defender)
        {
			foreach (Invader invader in invaders)
            {
				if (!invader.dead && defender.Bounds.IntersectsWith(invader.Bounds))
                {
					Global.GameOver = true;
                }
            }
        }


		// check invader location
		internal PointF InvaderLocation()
        {
			int row = random.Next(10);
			int col = random.Next(4);

			Invader invader = invaders[row, col];
			if (!invader.dead)
            {
				return invader.GetInvaderBulletStartLocation();
            } 
			else
            {
				for (int x = 0; x < 4; x++)
                {
					for (int y = 0; y < 10; y++)
                    {
						if (!invaders[x, y].dead)
							InvaderLocation();
					}
                }
            }
			return new PointF(-199, -100);
        }

		internal bool AllInvadersDead()
        {
			foreach(Invader invader in invaders)
            {
				if (!invader.dead)
					return false;
            }
			return true;
        }
	}
}