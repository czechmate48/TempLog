using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TempLogDictation
{
    public partial class PopUp : Form
    {
        private Timer timer;
        private SoundPlayer sound;

        public PopUp() { }

        public PopUp(string header, string subhdr, Color subhdr_color, string msg, int duration, SoundPlayer sound)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.Text = header;
            subheader.Text = subhdr;
            subheader.ForeColor = subhdr_color;
            message.Text = msg;
            this.sound = sound;
            timer = new Timer();
            timer.Interval = duration;
            timer.Tick += new EventHandler(OnElapsedTime);
        }

        private void OnElapsedTime(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }

        public void Display()
        {
            sound.Play();
            timer.Start();
            this.Show();
        }
    }

    public class PopUp_Factory
    {
        public enum Type { NAME_MISSING, TEMP_MISSING, SUCCESS, EMAIL_ERROR, LOG_ERROR, UNKNOWN_ERROR };
        private static SoundPlayer error = new SoundPlayer(@"C:\Windows\Media\Windows Error.wav");
        private static SoundPlayer success = new SoundPlayer(@"C:\Windows\Media\chimes.wav");

        public static PopUp Create_PopUp(Type type)
        {
            switch (type)
            {
                case Type.NAME_MISSING: return new PopUp("Name Missing", "ERROR", Color.Maroon, "Name Missing!\nPlease try again", 3000, error); 
                case Type.TEMP_MISSING: return new PopUp("Temperature Missing", "ERROR", Color.Maroon, "Temperature Missing!\nPlease try again", 3000, error); 
                case Type.SUCCESS: return new PopUp("Temperature Sent", "SUCCESS", Color.Green, "Have a great day!", 3000, success);
                case Type.EMAIL_ERROR: return new PopUp("Email Error", "ERROR", Color.Maroon, "Unable to send email", 3000, error);
                case Type.LOG_ERROR: return new PopUp("Log Error", "ERROR", Color.Maroon, "Unable to save temperature to log", 3000, error);
                case Type.UNKNOWN_ERROR: return new PopUp("Unknown Error", "ERROR", Color.Maroon, "an unknown error has occurred", 3000, error);
                default: return new PopUp("Unknown Error", "ERROR", Color.Red, "An unknown error has occurred", 3000, error);
            }
        }

    }

}
