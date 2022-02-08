using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace SpaceInvaders
{
    class MainForm : Form
    {
        private PictureBox pictureBox1;
        static Game game = new Game(); //Game.Instance;

        internal MainForm()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);

            this.ClientSize = Global.FormSize;
            this.BackgroundImage = Bitmap.FromFile(Directory.GetCurrentDirectory() + "\\background.jpg");
            this.Text = "Space Invaders";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new KeyEventHandler(game.OnKeyDown);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        static void Main()
        {
            using (MainForm form = new MainForm())
            {
                form.Show();
                game.Initialize(form);
                game.GameLoop();
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-7, -29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(298, 298);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
