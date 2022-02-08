using System;
using System.Drawing;
using System.IO;

namespace SpaceInvaders
{
    class Global
    {
        internal static readonly Size FormSize = new Size(800, 600);

        // Stats related
        internal static int Score = 0;
        internal static bool GameOver = true;
        internal static bool GameWon = false;
        internal static bool LevelFinished;
        internal static int CurrentLevel = 1;
        internal static int LivesRemaining = 3;
        internal static int DefenderSize = 30;

        internal static Directions DefenderDirection = Directions.None;
        internal static float DefenderSpeed = 1f / 5f;

        internal static Directions InvaderDirection = Directions.Right;
        internal static Directions UFODirection = Directions.None;

        internal static float InvaderSpeed = 1f / 20f;
        internal static float UFOSpeed = 1f / 10f;
        internal static float DefenderBulletSpeed = 1f / 1f;
        internal static float UFOBulletSpeed = 1f / 2f;
        internal static bool DefenderBulletFiring = false;
        internal static bool UFOBulletFiring = false;
        internal static readonly Size BulletSize = new Size(9, 30);
        internal static readonly Size InvaderSize = new Size(40, 40);
        internal static readonly Size UFOSize = new Size(60, 60);

        // for testing
        internal const int InvadersRow = 10;
        internal const int InvadersCol = 4;

        internal const int UFOesRow = 10;
        internal const int UFOesCol = 2;

        public static bool InvaderBulletFiring = false;

        internal static float InvaderBulletSpeed = 1f / 4f;
    }
}