using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace NewGame
{
    class Level
    {
        TextBox levelNumber;
        PictureBox life1;
        PictureBox life2;
        PictureBox level;

        int number;
        int countLevel = 1;
        const int startNumber = 3;
        Form1 thisForm;
        Button[] b;
        string[] numsInButtons;
        int[] nums;
        int i = 0, c = 0;
        string checkNum;
        int miliSeconds = 2000;
        List<int> listOfAllowableCoordinatesX = new List<int>();
        List<int> listOfAllowableCoordinatesY = new List<int>();

        public Level(int number, Form1 f)
        {
            this.number = number;
            thisForm = f;
            Create();
        }

        void Create()
        {
            CreateLists();
            b = new Button[number];
            Add();
            thisForm.AddButtons(b);

            life1 = new PictureBox();
            life1.BackgroundImage = Properties.Resources.serdechko;
            life1.Size = new Size(40, 40);
            life1.Location = new Point(5, 3);
            life1.BackColor = Color.FromArgb(0, 0, 0, 0);
            life1.BackgroundImageLayout = ImageLayout.Stretch;

            life2 = new PictureBox();
            life2.BackgroundImage = Properties.Resources.serdechko;
            life2.Size = new Size(40, 40);
            life2.Location = new Point(55, 3);
            life2.BackColor = Color.FromArgb(0, 0, 0, 0);
            life2.BackgroundImageLayout = ImageLayout.Stretch;

            level = new PictureBox();
            level.BackgroundImage = Properties.Resources.Level;
            level.Size = new Size(120, 50);
            level.Location = new Point(800, 3);
            level.BackColor = Color.FromArgb(0, 0, 0, 0);
            level.BackgroundImageLayout = ImageLayout.Stretch;

            levelNumber = new TextBox();
            levelNumber.Location = new Point(935, 3);
            levelNumber.Size = new Size(30, 40);
            levelNumber.Text = Convert.ToString(countLevel);
            levelNumber.Font = new Font("Helvetica", 20);
            levelNumber.Multiline = true;

            thisForm.AddControl(levelNumber);
            thisForm.AddControl(level);
            thisForm.AddControl(life1);
            thisForm.AddControl(life2);
        }

        private void CreateLists()
        {
            for (int i = 10; i < 900; i++)
                listOfAllowableCoordinatesX.Add(i);  
            for (int i = 20; i < 650; i++)
                listOfAllowableCoordinatesY.Add(i);
        }

        private void Add()
        {
            nums = Generate(number);
            Random r = new Random();
            Point FirstPoint = new Point();
            Point p = new Point();
            Size s = new Size(60, 60);
            b[0] = new Button();
            FirstPoint.X = r.Next(10, 900);
            FirstPoint.Y = r.Next(20, 650);
            b[0].Location = FirstPoint;
            b[0].Size = s;
            b[0].Text = Convert.ToString(nums[0]);
            b[0].Font = new Font("Helvetica", 15);
            numsInButtons[0] = Convert.ToString(nums[0]);

            for (int i = 1; i < number; i++)
            {
                b[i] = new Button();
                p = AllowablePoint(b[i - 1].Location.X, b[i - 1].Location.Y);               
                b[i].Location = p;
                b[i].Size = s;
                b[i].Text = Convert.ToString(nums[i]);
                b[i].Font = new Font("Helvetica", 15);
                numsInButtons[i] = Convert.ToString(nums[i]);
            }
        }

        private int[] Generate(int n)
        {
            int[] nums = new int[n];
            numsInButtons = new string[n];
            Random r = new Random();
            nums[0] = r.Next(1, 5);
            for (int i = 1; i < n; i++)
            {
                nums[i] = nums[i - 1] + r.Next(1, 5);
                while (nums[i] == nums[i - 1])
                    nums[i] = i + r.Next(1, 5);
            }
            return nums;
        }

        public void StartGame()
        {
            for (int i = 0; i < b.Length; i++)
                b[i].Click += LogClick;
            i = 0;
            c = 0;
            for (int i = 0; i < b.Length; i++)
            {
                b[i].Enabled = false;
                b[i].BackColor = Color.LightBlue;
            }
            thisForm.timer1.Tick += timer1_tick;
            thisForm.timer1.Start();
        }

        private void ContinueGame(int n)
        {
            number = n;
            Create();
            StartGame();            
        }

        private Point AllowablePoint (int X, int Y)
        {
            int index = 0;

            int borderX = 0, borderY = 0;

            int rangeX = 0, rangeY = 0;
            if ((X + 50) > 900)
            {
                rangeX = 900 - X + 50;
                borderX = 900;
            }
            else
            {
                rangeX = 2 * 50;
                borderX = X + 50;
            }

            if ((Y + 50) > 650)
            {
                rangeY = 650 - Y + 50;
                borderY = 650;
            }
            else
            {
                rangeY = 2 * 50;
                borderY = Y + 50;
            }

            int[] ExeptionCoordinatesX = new int[rangeX];
            int[] ExeptionCoordinatesY = new int[rangeY];

            for (int i = X - 50; i < borderX; i++) //недопустимые координаты по X
            {
                ExeptionCoordinatesX[index] = i;
                index++;
            }
            index = 0;
            for (int i = Y - 50; i < borderY; i++) //недопустимые координаты по Y
            {
                ExeptionCoordinatesY[index] = i;
                index++;
            }

            for (int i = 0; i < ExeptionCoordinatesX.Length; i++)
            {
                for (int j = 0; j < listOfAllowableCoordinatesX.Count; j++)
                {
                    if (listOfAllowableCoordinatesX[j] == ExeptionCoordinatesX[i])
                        listOfAllowableCoordinatesX.Remove(listOfAllowableCoordinatesX[j]);
                }
            }
            for (int i = 0; i < ExeptionCoordinatesY.Length; i++)
            {
                for (int j = 0; j < listOfAllowableCoordinatesY.Count; j++)
                {
                    if (listOfAllowableCoordinatesY[j] == ExeptionCoordinatesY[i])
                        listOfAllowableCoordinatesY.Remove(listOfAllowableCoordinatesY[j]);
                }
            }

            Random r = new Random();
            int randIndexFromX = 0, randIndexFromY = 0;
            if (listOfAllowableCoordinatesX.Count == 0 || listOfAllowableCoordinatesY.Count == 0)
                MessageBox.Show("Негде разместить кнопки");
            else
            {
                randIndexFromX = r.Next(0, listOfAllowableCoordinatesX.Count - 1);
                randIndexFromY = r.Next(0, listOfAllowableCoordinatesY.Count - 1);
            }
            int resultX = listOfAllowableCoordinatesX[randIndexFromX],
                resultY = listOfAllowableCoordinatesY[randIndexFromY];
            Point result = new Point(resultX, resultY);
            return result;
        }

        void timer1_tick(object sender, EventArgs e)
        {
            for (int i = 0; i < b.Length; i++)
            {
                b[i].BackColor = Color.Black;
                b[i].Enabled = true;
            }
            thisForm.timer1.Stop();
        }

        void LogClick(object sender, EventArgs e)
        {
            Button nb = sender as Button;
            checkNum = nb.Text;
            if (checkNum == numsInButtons[i])
            {
                nb.BackColor = Color.LightBlue;
                i++;
            }
            else
            {               
                c++;
                if (c == 1)
                    thisForm.RemoveControl(life2);
                if (c == 2)
                {
                    thisForm.RemoveControl(life1);
                    countLevel = 1;
                }
            }
            if (i == b.Length && c != 2)
            {
                DialogResult result = MessageBox.Show("Правильно! Продолжить?", "Next Level", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    CreateLists();
                    thisForm.ClearForm();
                    number++;
                    countLevel++;
                    ContinueGame(number);
                }
                else
                {                    
                    thisForm.ClearForm();
                    MainMenu mM = new MainMenu(thisForm);
                    countLevel = 1;
                }
            }
            if (c == 2)
            {
                thisForm.ClearForm();
                ContinueGame(startNumber);
            }
        }
    }
}
