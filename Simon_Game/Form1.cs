using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading; 

namespace Simon_Game
{

    enum Colors{ Red,  Blue,  Yellow,  Green, Buzzer  };



    public partial class Form1 : Form
    {
        //Our Sounds
        SoundPlayer redSound = new SoundPlayer(Properties.Resources.snd_a_High);
        SoundPlayer blueSound = new SoundPlayer(Properties.Resources.snd_a_low);
        SoundPlayer yellowSound = new SoundPlayer(Properties.Resources.snd_middle_c);
        SoundPlayer greenSound = new SoundPlayer(Properties.Resources.snd_E);
        SoundPlayer buzzerSound = new SoundPlayer(Properties.Resources.snd_buzzer);

        int curLight = 0;
        List<Colors> sequence = new List<Colors>();
        Dictionary<Colors, PictureBox> buttonMap = new Dictionary<Colors, PictureBox>();

        public Form1()
        {
            InitializeComponent();

            buttonMap[Colors.Red] = pictureBoxRed;
            buttonMap[Colors.Blue] = pictureBoxBlue;
            buttonMap[Colors.Yellow] = pictureBoxYellow;
            buttonMap[Colors.Green] = pictureBoxGreen;

            Restart();

        }

        private void pictureBoxGreen_Click(object sender, EventArgs e)
        { 
        
            HandleButton(Colors.Green);
        }

        private void pictureBoxRed_Click(object sender, EventArgs e)
        {
            HandleButton(Colors.Red);
         }

        private void pictureBoxBlue_Click(object sender, EventArgs e)
        {
        HandleButton(Colors.Blue);
         }

        private void pictureBoxYellow_Click(object sender, EventArgs e)
        {
         HandleButton(Colors.Yellow);
    }


        void PlaySound(Colors c)
        {
            if (c == Colors.Red)
                redSound.PlaySync();
            else if (c == Colors.Blue)
                blueSound.PlaySync();
            else if (c == Colors.Yellow)
                yellowSound.PlaySync();
            else if (c == Colors.Green)
                greenSound.PlaySync();
            else if (c == Colors.Buzzer)
                buzzerSound.PlaySync();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaySound(Colors.Blue);
            PlaySound(Colors.Red);
            PlaySound(Colors.Yellow);
            PlaySound(Colors.Green);
            PlaySound(Colors.Buzzer);
        }

        void LightOn(PictureBox pb, Colors c)
        {
            if (c == Colors.Red)
                pb.Image = Properties.Resources.img_red_on;
            else if (c == Colors.Blue)
                pb.Image = Properties.Resources.img_blue_on;
            else if (c == Colors.Yellow)
                pb.Image = Properties.Resources.img_yellow_on;
            else if (c == Colors.Green)
                pb.Image = Properties.Resources.img_green_on;


            pb.Refresh();
        }

        void LightOff(PictureBox pb, Colors c)
        {
            if (c == Colors.Red)
                pb.Image = Properties.Resources.img_red_off;
            else if (c == Colors.Blue)
                pb.Image = Properties.Resources.img_blue_off;
            else if (c == Colors.Yellow)
                pb.Image = Properties.Resources.img_yellow_off;
            else if (c == Colors.Green)
                pb.Image = Properties.Resources.img_green_off;

            pb.Refresh();
        }

        void FlashButton(PictureBox pb, Colors c)
        {
            //turn on button
            LightOn(pb, c);
            //play sound
            PlaySound(c);
            //turn off button
            LightOff(pb, c);
        }

        private void testButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FlashButton(pictureBoxRed, Colors.Red);
            FlashButton(pictureBoxBlue, Colors.Blue);
            FlashButton(pictureBoxYellow, Colors.Yellow);
            FlashButton(pictureBoxGreen, Colors.Green);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        //Puts a new color in the sequence
        void AddLight()
        {
            Random r = new Random();
            int next = r.Next(0, 3);
            Colors c = (Colors)next; 
            if(c == Colors.Buzzer)
             {
                MessageBox.Show("Warning: adding  a buzzer");
            }
            sequence.Add((Colors)next);

        }

        void PlaySequence()
        {
            foreach (Colors c in sequence)
            {
                FlashButton(buttonMap[c], c);
                Thread.Sleep(200); 
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart();
        }

        void Restart()
        {
            sequence.Clear();
            curLight = 0;
            AddLight();
            PlaySequence();
        }

        void HandleButton(Colors c)
        {
            if (sequence.Count > 0)
            {
                if(c == sequence[curLight])
                {
                    //matched!
                    FlashButton(buttonMap[c], c);
                    curLight++; 

                    //did the player match the entire sequence? 
                    if(curLight == sequence.Count)
                    {
                        AddLight();
                        curLight = 0; 
                        PlaySequence(); 
                    }
                }
                else
                {
                    //wrong guess!
                    PlaySound(Colors.Buzzer);
                    Restart(); 
                }
            }
            else
            {
                Restart();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Restart(); 
        }
    }
}
