using System;
using System.Drawing;

namespace SpaceInvaders
{
    internal class UFOGroup
    {
		private UFO[,] ufoes;
		Random random = new Random();

		internal UFOGroup(int cols, int rows)
		{
			ufoes = new UFO[cols, rows];

			for (int x = 0; x < cols; x++)
			{
				for (int y = 0; y < rows; y++)
				{
					PointF location = new Point(x * (Global.UFOSize.Width) + 80,
						y * (Global.UFOSize.Height) + 80);

					ufoes[x, y] = new UFO(location);
				}
			}
		}

		internal void Step(double elapsed)
		{
			foreach (UFO ufo in ufoes)
			{
				ufo.Step(elapsed);
			}
		}

		internal void Render(Graphics graphics)
		{
			foreach (UFO ufo in ufoes)
			{
				ufo.Render(graphics);
			}
		}

		internal void CheckForCollision(DefenderBullet defenderBullet)
		{
			foreach (UFO ufo in ufoes)
			{
				if (!ufo.dead && defenderBullet.Bounds.IntersectsWith(ufo.Bounds))
				{
					ufo.HitByDefenderBullet();
					defenderBullet.Hit();
					return;
				}
			}
		}

		// check UFO location
		internal PointF UFOLocation()
		{
			int row = random.Next(10);
			int col = random.Next(2);

			UFO ufo = ufoes[row, col];
			if (!ufo.dead)
			{
				return ufo.GetUFOBulletStartLocation();
			}
			else
			{
				for (int x = 0; x < 2; x++)
				{
					for (int y = 0; y < 10; y++)
					{
						if (!ufoes[x, y].dead)
							UFOLocation();
					}
				}
			}
			return new PointF(-300, -200);
		}
	}
}
