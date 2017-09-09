using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace NewGame
{
    public partial class Form1 : Form
    {
        public void AddButtons(Button[] arr)
        {
            for (int j = 0; j < arr.Length; j++)
                Controls.Add(arr[j]);
        }

        public void AddControl(Control c)
        {
            Controls.Add(c);
        }

        public void RemoveControl(Control c)
        {
            Controls.Remove(c);
        }

        public void ClearForm()
        {
            Controls.Clear();
        }

        public Form1()
        {           
            InitializeComponent();          
        }

        void PressStart(object sender, EventArgs e)
        {
            Controls.Clear();
            int i = 3;
            Level anyLevel = new Level(i, this);
            anyLevel.StartGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1 f = this;
            f.Size = new Size(1000, 800);
            MainMenu mm = new MainMenu(this);
            MainMenu.StartButton.Click += PressStart;          
        }

    }
}
