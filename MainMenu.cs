using System;
using System.Windows.Forms;
using System.Drawing;

namespace NewGame
{
    public class MainMenu
    {
        PictureBox pb;
        PictureBox pb2;
        public static Button StartButton;
        public bool StartGame { get; private set; }

        public MainMenu(Form1 f)
        {
            f.Text = "TrainBrain";
            f.Icon = Properties.Resources.brain;
            pb = new PictureBox();
            f.AddControl(pb);
            pb.BackColor = Color.FromArgb(0, 0, 0, 0);
            pb.Location = new Point((f.Width / 2) - 200, (f.Height / 2) - 280);
            pb.BackgroundImage = Properties.Resources.Logo;
            pb.Size = new Size(400, 140);
            pb.BackgroundImageLayout = ImageLayout.Stretch;

            StartButton = new Button();
            f.AddControl(StartButton);
            StartButton.Text = "GO";
            StartButton.Font = new Font("Helvetica", 20);
            StartButton.Size = new Size(200, 80);
            StartButton.Location = new Point(pb.Location.X + 105, pb.Location.Y + 350);
            StartButton.BackColor = Color.Firebrick;

            pb2 = new PictureBox();            
            f.AddControl(pb2);
            pb2.BackColor = Color.FromArgb(0, 0, 0, 0);
            pb2.BackgroundImage = Properties.Resources.Instruction;
            pb2.Location = new Point(pb.Location.X - 150, pb.Location.Y + 165);
            pb2.Size = new Size(700, 150);
            pb2.BackgroundImageLayout = ImageLayout.Stretch;
        }
    }
}
