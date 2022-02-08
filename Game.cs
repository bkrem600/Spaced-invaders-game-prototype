using System;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.IO;

namespace SpaceInvaders
{
    class Game
    {
        private Image buffer;
        private Graphics bufferGraphics;
        private Graphics displayGraphics;

        private Form form;
        private readonly Font font = new Font("Impact", 14);
        private readonly Font largeFont = new Font("Impact", 26);
        private readonly Brush fontBrush = Brushes.DarkRed;


        private Defender defender;
        private DefenderBullet defenderBullet;
        private InvaderBullet invaderBullet;
        private UFOBullet ufoBullet;
        private InvaderGroup invaders;
        private UFOGroup ufoes;

        internal Game() { }
        private static readonly Game instance = new Game();
        internal static Game Instance { get { return instance; } }

        internal void Initialize(Form mainForm)
        {
            this.form = mainForm;

            //Set up the off-screen buffer used for double-buffering
            buffer = new Bitmap(mainForm.Width, mainForm.Height);
            bufferGraphics = Graphics.FromImage(buffer);
            displayGraphics = mainForm.CreateGraphics();

        }

        internal void GameLoop()
        {
            DateTime start;
            double elapsed = 0d;
            while (form.Created)
            {
                start = DateTime.Now;
                Render();
                Application.DoEvents();
                if (!Global.GameOver)
                {
                    Step(elapsed);
                    DetectCollision();
                    InvaderShoot();
                    UfoesShoot();
                }
                elapsed = (DateTime.Now - start).TotalMilliseconds; 
            }
        }

        private void Step(double elapsed)
        {
            defender.Step(elapsed);
            invaders.Step(elapsed);
            ufoes.Step(elapsed);
            if (Global.DefenderBulletFiring)
            {
                defenderBullet.Step(elapsed);
            }
            if (Global.InvaderBulletFiring)
            {
                invaderBullet.Step(elapsed);
            }
            if (Global.UFOBulletFiring)
            {
                ufoBullet.Step(elapsed);
            }    
        }

        internal void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Environment.Exit(0);
            // if game is not active
            if (Global.GameOver)
            {
                if (e.KeyCode == Keys.F5)
                    StartNewGame();
            }
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Global.DefenderDirection = Directions.Left;
                    break;
                case Keys.Right:
                    Global.DefenderDirection = Directions.Right;
                    break;
                case Keys.Down:
                    Global.DefenderDirection = Directions.None;
                    break;
                case Keys.Up:
                    Global.DefenderDirection = Directions.None;
                    break;
                case Keys.Space:
                    {
                        if (!Global.DefenderBulletFiring)
                        {
                            Global.DefenderBulletFiring = true;
                            defenderBullet = new DefenderBullet(defender.GetDefenderBulletStartLocation());
                        }
                        break;
                    }
            }
        }

        private void StartNewGame()
        {
            Global.GameOver = false;
            Global.Score = 0;
            Global.LivesRemaining = 3;

            defender = new Defender(new Point(form.ClientSize.Width / 2 - 20, form.ClientSize.Height - 50));
            invaders = new InvaderGroup(Global.InvadersRow, Global.InvadersCol);
            ufoes = new UFOGroup(Global.UFOesRow, Global.UFOesCol);
        }

        private void Render()  //(double elapsed)
        {
            bufferGraphics.Clear(Color.Black);

            if (!Global.GameOver)
            {
                defender.Render(bufferGraphics);
                invaders.Render(bufferGraphics);
                ufoes.Render(bufferGraphics);
                if (Global.DefenderBulletFiring)
                    defenderBullet.Render(bufferGraphics);
                if (Global.InvaderBulletFiring)
                    invaderBullet.Render(bufferGraphics);
                if (Global.UFOBulletFiring)
                    ufoBullet.Render(bufferGraphics);
            }
            else
            {
                // Show "Game Over" message in the center of the screen
                bufferGraphics.DrawString("Game Over", largeFont, fontBrush, 310, 290);
                bufferGraphics.DrawString("Final Score: " + Global.Score, font, fontBrush, 350, 350);
                bufferGraphics.DrawString("Press F5 to start a new game", font, fontBrush, 278, 400);
            }

            if (Global.Score == 800)
            {
                // display Game Won screen when all enemies cleared
                Global.GameWon = true;
                bufferGraphics.DrawString("Game Won", largeFont, fontBrush, 330, 290);
                bufferGraphics.DrawString("Final Score: " + Global.Score, font, fontBrush, 350, 350);
                bufferGraphics.DrawString("Press Esc to exit", font, fontBrush, 350, 400);
            }

            // Display banner
            bufferGraphics.DrawString("Score: " + Global.Score, font, fontBrush, 10, 10);
            bufferGraphics.DrawString("Level: " + Global.CurrentLevel, font, fontBrush, 630, 10);
            bufferGraphics.DrawString("Lives: " + Global.LivesRemaining, font, fontBrush, 710, 10);

            // Blit the off-screen buffer on to the display
            displayGraphics.DrawImage(buffer, 0, 0);
        }

        private void DetectCollision()
        {
            if (Global.DefenderBulletFiring)
                invaders.CheckForCollision(defenderBullet);
            if (Global.InvaderBulletFiring)
                defender.CheckForCollision(invaderBullet);
            if (Global.UFOBulletFiring)
                defender.CheckForUFOCollision(ufoBullet);
            if (Global.DefenderBulletFiring)
                ufoes.CheckForCollision(defenderBullet);
            // did invader touch defender
            invaders.CheckForDefenderCollision(defender);
        }

        private void InvaderShoot()
        {
            if (!Global.InvaderBulletFiring)
            {
                try
                {
                    Global.InvaderBulletFiring = true;
                    invaderBullet = new InvaderBullet(invaders.InvaderLocation());
                } catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private void UfoesShoot()
        {
            if (!Global.UFOBulletFiring)
            {
                try
                {
                    Global.UFOBulletFiring = true;
                    ufoBullet = new UFOBullet(ufoes.UFOLocation());
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}