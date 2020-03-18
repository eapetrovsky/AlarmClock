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

namespace AlarmClock
{
    public partial class Alarm : Form
    {
        System.Timers.Timer timer;

        public Alarm()
        {
            InitializeComponent();
        }

        private void Alarm_Load(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += timer_Elapsed;
        }

        delegate void UpdateLabel(Label lbl, string value);
        
        void UpdateDataLable(Label lbl, string value)
        {
            lbl.Text = value;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            DateTime userTime = dateTimePicker.Value;
            if (currentTime.Hour == userTime.Hour && currentTime.Minute == userTime.Minute && currentTime.Second == userTime.Second)
            {
                timer.Stop();
                try
                {
                    UpdateLabel upd = UpdateDataLable;
                    if (lblStatus.InvokeRequired)
                    {
                        Invoke(upd, lblStatus, "Stop");
                    }

                    SoundPlayer player = new SoundPlayer();
                    player.SoundLocation = @"D:\mine\Rob Zombie - Living Dead Girl.wav";
                    player.PlayLooping();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timer.Start();
            lblStatus.Text = "Running...";
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            lblStatus.Text = "Stop";
        }
    }
}
