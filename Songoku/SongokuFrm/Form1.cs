using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace SongokuFrm
{
    public partial class Form1 : Form
    {
        private Bitmap backBuffer;
        private Timer timer_Saiyan, timer_Move, timer_Attack, timer_Fire, timer_Jump, timer_Combo;
        public Graphics graphics;
        private WindowsMediaPlayer sound1, sound2, sound3;
        private bool left = false, right = true, active = true;
        Songoku songoku = new Songoku();
        Fire fire = new Fire();

        Keys moveRight = Keys.D;
        Keys moveLeft = Keys.A;
        Keys attack = Keys.J;
        Keys jump = Keys.W;
        Keys saiyan = Keys.L;
        Keys combo = Keys.K;

        public Form1()
        {
            InitializeComponent();

            timer_Saiyan = new Timer();
            timer_Saiyan.Interval = 250;

            timer_Move = new Timer();
            timer_Move.Interval = 150;


            timer_Attack = new Timer();
            timer_Attack.Interval = 100;

            timer_Fire = new Timer();
            timer_Fire.Interval = 30;

            timer_Jump=new Timer();
            timer_Jump.Interval = 120;

            timer_Combo = new Timer();
            timer_Combo.Interval = 120;

            sound1 = new WindowsMediaPlayer();
            sound1.URL = "C:\\Users\\Administrator\\source\\repos\\Songoku\\SongokuFrm\\bin\\Debug\\Kaioken.mp4";
            sound1.controls.stop();

            sound2 = new WindowsMediaPlayer();
            sound2.URL = "C:\\Users\\Administrator\\source\\repos\\Songoku\\SongokuFrm\\bin\\Debug\\Combo.mp4";
            sound2.controls.stop();

            sound3 = new WindowsMediaPlayer();
            sound3.URL = "C:\\Users\\Administrator\\source\\repos\\Songoku\\SongokuFrm\\bin\\Debug\\Move.mp4";
            sound3.controls.stop();
            graphics = this.CreateGraphics();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            backBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Saiyan();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (active)
            {
                if (e.KeyCode == moveRight)
                {
                    right = true;
                    Move();
                    active = false;
                }
                else if (e.KeyCode == moveLeft)
                {
                    right= false;
                    left = true;
                    Move();
                    active = false;
                }
                else if (e.KeyCode == attack)
                {
                    Attack();
                    active = false;
                }
                else if (e.KeyCode == saiyan)
                {
                    Saiyan();
                    active = false;
                }
                else if (e.KeyCode == jump)
                {
                    Jump();
                    active = false;
                }
                else if (e.KeyCode == combo)
                {
                    Combo();
                    active = false;
                }
            }
            else
            {
                if (e.KeyCode == moveRight) 
                {
                    songoku.Move_active = -1;
                    songoku.index = 3;
                    songoku.Render_Move_R(backBuffer);
                    graphics.DrawImageUnscaled(backBuffer, 0, 0);
                }
                else if (e.KeyCode == moveLeft)
                {     
                    songoku.Move_active = -1;
                    songoku.index = 3;
                    songoku.Render_Move_L(backBuffer);
                    graphics.DrawImageUnscaled(backBuffer, 0, 0);
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!active) 
            {
                if (e.KeyCode == moveRight)
                {
                    sound3.controls.play();
                    timer_Move.Tick -= new EventHandler(timer_Move_Tick);
                    songoku.index = 4;
                    songoku.Move_active = 1;
                    Move();
                    active = true;
                }
                else if (e.KeyCode == moveLeft)
                {
                    sound3.controls.play();
                    timer_Move.Tick -= new EventHandler(timer_Move_Tick);
                    songoku.index = 4;
                    songoku.Move_active = 1;
                    Move();
                    active = true;
                }
                else active = true;
            }
        }

        public void Saiyan()
        {
            sound1.controls.play();
            timer_Saiyan.Enabled = true;
            timer_Saiyan.Tick += new EventHandler(timer_Saiyan_Tick);
        }

        private void Move()
        {
            timer_Move.Enabled = true;
            timer_Move.Tick += new EventHandler(timer_Move_Tick);
            timer_Move.Start();
        }

        private void Attack()
        {
            timer_Attack.Enabled = true;
            timer_Attack.Tick += new EventHandler(timer_Attack_Tick);
            timer_Attack.Start();
        }
        private void Combo()
        {
            sound2.controls.play();
            timer_Combo.Enabled = true;
            timer_Combo.Tick += new EventHandler(timer_Combo_Tick);
            timer_Combo.Start();
        }

        private void Jump()
        {
            timer_Jump.Enabled = true;
            timer_Jump.Tick += new EventHandler(timer_Jump_Tick);
            timer_Jump.Start();
        }

        public void Fire()
        {
            timer_Attack.Stop();
            timer_Attack.Tick -= new EventHandler(timer_Attack_Tick);
            timer_Fire.Enabled = true;
            timer_Fire.Tick += new EventHandler(timer_Fire_Tick);
        }

        private void timer_Saiyan_Tick(object sender, EventArgs e)
        {
            if (right)
            {
                songoku.Render_Saiyan_R(backBuffer);
                graphics.DrawImageUnscaled(backBuffer, 0, 0);
                if (songoku.index == 0)
                {
                    timer_Saiyan.Tick -= new EventHandler(timer_Saiyan_Tick);
                    timer_Saiyan.Enabled = false;
                }
            }
            else if (left)
            {
                songoku.Render_Saiyan_L(backBuffer);
                graphics.DrawImageUnscaled(backBuffer, 0, 0);
                if (songoku.index == 0)
                {
                    timer_Saiyan.Tick -= new EventHandler(timer_Saiyan_Tick);
                    timer_Saiyan.Enabled = false;
                }
            }
            
        }
        private void timer_Move_Tick(object sender, EventArgs e)
        {
            if (right)
            {
                songoku.Render_Move_R(backBuffer);
                graphics.DrawImageUnscaled(backBuffer, 0, 0);
                if (songoku.index == 4 || songoku.index == 0)
                {
                    timer_Move.Tick -= new EventHandler(timer_Move_Tick);
                    timer_Move.Enabled = false;
                    //right = false;
                }
            }
            else if(left)
            {
                songoku.Render_Move_L(backBuffer);
                graphics.DrawImageUnscaled(backBuffer, 0, 0);
                if (songoku.index == 4 || songoku.index == 0)
                {
                    timer_Move.Tick -= new EventHandler(timer_Move_Tick);
                    timer_Move.Enabled = false;
                    //left = false;
                }
            }
        }
        private void timer_Jump_Tick(object sender, EventArgs e)
        {
            if (right)
            {
                songoku.Render_Jump_R(backBuffer);
                graphics.DrawImageUnscaled(backBuffer, 0, 0);
                if (songoku.index == 0)
                {
                    timer_Jump.Stop();
                    timer_Jump.Tick -= new EventHandler(timer_Jump_Tick);
                }
            }
            else if (left)
            {

                songoku.Render_Jump_L(backBuffer);
                graphics.DrawImageUnscaled(backBuffer, 0, 0);
                if (songoku.index == 0)
                {
                    timer_Jump.Stop();
                    timer_Jump.Tick -= new EventHandler(timer_Jump_Tick);
                }
            }

        }
        private void timer_Combo_Tick(object sender, EventArgs e)
        {
            if (right)
            {
                songoku.Render_Combo_R(backBuffer);
                graphics.DrawImageUnscaled(backBuffer, 0, 0);
                if (songoku.index == 0)
                {
                    timer_Combo.Stop();
                    timer_Combo.Tick -= new EventHandler(timer_Combo_Tick);
                }
            }
            else if (left)
            {
                songoku.Render_Combo_L(backBuffer);
                graphics.DrawImageUnscaled(backBuffer, 0, 0);
                if (songoku.index == 0)
                {
                    timer_Combo.Stop();
                    timer_Combo.Tick -= new EventHandler(timer_Combo_Tick);
                }
            }
        }
        private void timer_Attack_Tick(object sender, EventArgs e)
        {
            if (right)
            {
                songoku.Render_Attack_R(backBuffer);
                if (songoku.index == 5 || songoku.index == 8)
                {
                    fire.x = songoku.x + 130;
                    fire.y = songoku.y + 65;
                    songoku.Attack_active = -1;
                    graphics.DrawImageUnscaled(backBuffer, 0, 0);
                    Fire();

                }
                else graphics.DrawImageUnscaled(backBuffer, 0, 0);

                if (songoku.index == 0)
                {
                    timer_Attack.Stop();
                    timer_Attack.Tick -= new EventHandler(timer_Attack_Tick);
                }
            }
            else if (left)
            {
                songoku.Render_Attack_L(backBuffer);
                if (songoku.index == 5 || songoku.index == 8)
                {
                    fire.x = songoku.x - 60;
                    fire.y = songoku.y + 65;
                    songoku.Attack_active = -1;
                    graphics.DrawImageUnscaled(backBuffer, 0, 0);
                    Fire();

                }
                else graphics.DrawImageUnscaled(backBuffer, 0, 0);

                if (songoku.index == 0)
                {
                    timer_Attack.Stop();
                    timer_Attack.Tick -= new EventHandler(timer_Attack_Tick);
                }
            }
        }
       
        private void timer_Fire_Tick(object sender, EventArgs e)
        {
            if (right)
            {
                //graphics.DrawImageUnscaled(backBuffer, 0, 0);
                fire.Render_R(backBuffer, graphics);
                songoku.Render_Attack_R(backBuffer);
                graphics.DrawImage(backBuffer, 0, 0);
                if (fire.indexRight == 0)
                {
                    timer_Fire.Tick -= new EventHandler(timer_Fire_Tick);
                    timer_Fire.Stop();
                    songoku.Attack_active = 1;
                    timer_Attack.Tick += new EventHandler(timer_Attack_Tick);
                    timer_Attack.Start();
                }
            }
            else if (left)
            {
                fire.Render_L(backBuffer, graphics);
                songoku.Render_Attack_L(backBuffer);
                graphics.DrawImage(backBuffer, 0, 0);
                if (fire.indexLeft == 4)
                {
                    timer_Fire.Tick -= new EventHandler(timer_Fire_Tick);
                    timer_Fire.Stop();
                    songoku.Attack_active = 1;
                    timer_Attack.Tick += new EventHandler(timer_Attack_Tick);
                    timer_Attack.Start();
                }
            }
        }
    }
}
