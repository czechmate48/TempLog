using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.IO;

/// <summary>
/// This code was built using the following information:
/// https://www.youtube.com/watch?v=AB9lfHDOe5U
/// https://docs.microsoft.com/en-us/dotnet/api/system.speech.recognition.speechrecognitionengine.-ctor?view=netframework-4.8
/// </summary>

/// <summary>
/// EMAIL CONFIGURATION TEXT FILE
/// server
/// port
/// user login name
/// user password
/// sender email address
/// sender name
/// recipient emial address
/// </summary>

namespace TempLogDictation
{
    public partial class TempLog : Form
    {
        //COMMANDS
        private const string SAVE_TEMP = "save temp";
        private const string SEND = "send";
        private const string HELP = "help";
        private const string CLEAR = "clear";
        private const string STOP = "stop";

        private enum Modes { STANDBY, DICTATION};
        private Modes curMode = Modes.STANDBY; //Default to standby mode
        private string[] standby_mode_commands = new string[] { SAVE_TEMP, HELP };
        private List<string> default_dictation_mode_commands = new List<string> { SEND, HELP, STOP, CLEAR };
        private string[] complete_dictation_mode_commands;
        private string[] username_commands;
        private string[] temperature_commands;

        private string usernameCmds_config = ".\\Configuration\\UsernameCmds_Config.txt";
        private string tempCmds_config = ".\\Configuration\\TempCmds_Config.txt"; 
        private string email_config = ".\\Configuration\\Email_Config.txt";
        private string tempLog = ".\\Configuration\\TempLog.txt";
        private string tempLog_config = ".\\Configuration\\TempLog_Config.txt";
        private bool tempLog_on; //Line 0 in config

        private bool email_on; //Line 0 in config
        private string sender_email_address; //Line 5 in config file
        private string sender_name; //Line 6 in config file
        private string recipient_email_address; //Line 7 in config file
        private string email_subject; //Configured the same as the message body

        private SpeechRecognitionEngine srEngine = new SpeechRecognitionEngine(new CultureInfo ("en-US"));

/**************************/
/* INITIALIZE             */
/**************************/

        public TempLog()
        {
            InitializeComponent();
        }

        private void TempLog_Load(object sender, EventArgs e)
        {
            srEngine.SetInputToDefaultAudioDevice();
            Setup_Email_Variables();
            Setup_TempLog();
            Setup_Dictation_Mode_Commands();
            ListenFor_Initial_Command();
        }

        private void Setup_Email_Variables()
        {
            string[] lines = System.IO.File.ReadAllLines(email_config);
            if (lines[0] == "on") email_on = true;
            else email_on = false;
            sender_email_address = lines[5];
            sender_name = lines[6];
            recipient_email_address = lines[7];
        }

        private void Setup_TempLog()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(tempLog_config);
                if (lines[0] == "on") tempLog_on = true;
                else tempLog_on = false;
                tempLog = lines[1];
                Console.WriteLine(lines[1]);
            }
            catch (Exception e)
            {
                //Will save to default location
            } 
        }

        private void Setup_Dictation_Mode_Commands()
        {
            username_commands = Load_File_As_Commands(usernameCmds_config);

            foreach (string name in username_commands)
            {
                default_dictation_mode_commands.Add(name);
            }

            temperature_commands = Load_File_As_Commands(tempCmds_config);

            foreach (string temp in temperature_commands)
            {
                default_dictation_mode_commands.Add(temp);
            }

            complete_dictation_mode_commands = default_dictation_mode_commands.ToArray();
        }

        private string[] Load_File_As_Commands(string filePath)
        {
            string[] lines = System.IO.File.ReadAllLines(@filePath);
            List<string> values = new List<string>();
            foreach (string line in lines)
            {
                values.Add(line);
            };

            return values.ToArray();
        }

/**************************/
/* COMMANDS               */
/**************************/

        private void ListenFor_Initial_Command()
        {
            GrammarBuilder builder = new GrammarBuilder(new Choices(standby_mode_commands));
            Grammar grammar = new Grammar(builder);

            srEngine.LoadGrammarAsync(grammar);
            srEngine.RecognizeAsync(RecognizeMode.Multiple);
            srEngine.SpeechRecognized += React_To_Command;
        }

        private void Listen_For_Commands(string[] commands)
        {
            GrammarBuilder builder = new GrammarBuilder(new Choices(commands));
            Grammar grammar = new Grammar(builder);

            srEngine.LoadGrammarAsync(grammar);
            srEngine.SpeechRecognized += React_To_Command;
        }

        private void Set_Mode(Modes mode)
        {
            curMode = mode;
        }

        /****/

        private void React_To_Command(object sender, SpeechRecognizedEventArgs e)
        {
            srEngine.SpeechRecognized -= React_To_Command; //Prevents commands from being called more than once

            if (curMode == Modes.STANDBY) React_To_Command_Standby(e.Result.Text);
            else if (curMode == Modes.DICTATION) React_To_Command_Dictation(e.Result.Text);
        }

        private void React_To_Command_Standby(string command)
        {
            switch (command)
            {
                case SAVE_TEMP:
                    Set_Mode(Modes.DICTATION); //Always set mode first to allow GUI to appropriately update
                    Update_Gui();
                    Listen_For_Commands(complete_dictation_mode_commands);
                    break;
                default:
                    Listen_For_Commands(standby_mode_commands);
                    break;
            }
        }

        private void React_To_Command_Dictation(string command)
        {
            switch (command)
            {
                case SEND:
                    if (!Check_For_Name_Missing_Error() && !Check_For_Temp_Missing_Error())
                    {
                        Set_Mode(Modes.STANDBY);
                        Send_Temp();
                        Listen_For_Commands(standby_mode_commands);
                    }
                    else Listen_For_Commands(complete_dictation_mode_commands);
                    return;
                case STOP:
                    Set_Mode(Modes.STANDBY);
                    Update_Gui();
                    Listen_For_Commands(standby_mode_commands);
                    return;
                case CLEAR:
                    name_TextArea.Clear();
                    temperature_textArea.Clear();
                    Listen_For_Commands(complete_dictation_mode_commands);
                    return;
            }

            foreach (string username in username_commands)
            {
                if (command == username)
                {
                    name_TextArea.Text = username;
                    Listen_For_Commands(complete_dictation_mode_commands);
                    return;
                }
            }

            foreach (string temp in temperature_commands)
            {
                if (command == temp)
                {
                    temperature_textArea.Text = temp;
                    Listen_For_Commands(complete_dictation_mode_commands);
                    return;
                }
            }
        }

        /***/

        private void Send_Temp()
        {
            try
            {
                string message = name_TextArea.Text + " " + temperature_textArea.Text;
                if (email_on) Email_Temp(message);
                if (tempLog_on) Log_Temp(message);
                AutoClosingMessageBox.Show("Have a great day!!!", "Temperature sent", 3000);
                Set_Mode(Modes.STANDBY);
                Update_Gui();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                AutoClosingMessageBox.Show("Please send name and temperature manually", "Unable to send message", 3000);
                Set_Mode(Modes.STANDBY);
                Update_Gui();
            }
        }

        private void Email_Temp(string message)
        {
            email_subject = message;
            Email email = new Email(email_config, sender_email_address, sender_name, recipient_email_address, email_subject, message);
            email.Send();
        }

        private void Log_Temp(string message)
        {
            StreamWriter file = new StreamWriter(tempLog, true);
            file.WriteLine(DateTime.Now + " : " + message);
            file.Flush();
            file.Close();
        }

        private bool Check_For_Name_Missing_Error()
        {
            if (name_TextArea.TextLength == 0)
            {
                AutoClosingMessageBox.Show("Please input a name", "Name Missing!", 3000);
                return true;
            }
            return false;
        }

        private bool Check_For_Temp_Missing_Error()
        {
            if (temperature_textArea.TextLength == 0)
            {
                AutoClosingMessageBox.Show("Please input a temperature", "Temp Missing!", 3000);
                return true;
            }
            return false;
        }

/**************************/
/* GUI                    */
/**************************/

        private void Update_Gui()
        {
            switch (curMode)
            {
                case Modes.STANDBY:
                    Set_Standby_Gui();
                    break;
                case Modes.DICTATION:
                    Set_Dictation_Gui();
                    break;
            }
        }

        private void Set_Standby_Gui()
        {
            Dictate.Text = "Save Temp";
            name_TextArea.Clear();
            temperature_textArea.Clear();
            Dictate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192))))); //Green
        }

        private void Set_Dictation_Gui()
        {
            Dictate.Text = "Stop";
            Dictate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))); //Red
        }

        private void Dictate_Click(object sender, EventArgs e)
        {
            if (curMode == Modes.DICTATION) React_To_Command_Dictation(STOP);
            else if (curMode == Modes.STANDBY) React_To_Command_Standby(SAVE_TEMP);
        }

        private void Email_Click(object sender, EventArgs e)
        {
            if (!Check_For_Name_Missing_Error() && !Check_For_Temp_Missing_Error()) Send_Temp();
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            name_TextArea.Clear();
            temperature_textArea.Clear();
        }

        private void Create_Temp_File()
        {
            StreamWriter file2 = new StreamWriter(tempCmds_config, true);

            for (float i = 0; i < 125; i += .1f)
            {
                file2.WriteLine(Math.Round(i, 1));
            }

            file2.Flush();
            file2.Close();
        }

    }

    /****************************************************************************/

    /// <summary>
    /// This code was built after following a tutorial posted by Fox Learn on July 21, 2015
    /// https://www.youtube.com/watch?v=4lzZ0wzEK14
    /// </summary>

    public class Email
    {
        private SmtpClient client;
        private MailMessage msg;

        public Email(string config_FilePath, string sender_email_address, string sender_name, string recipient_email_address, string subject, string message)
        {
            client = Setup_Client(config_FilePath);
            msg = Setup_Message(sender_email_address, sender_name, recipient_email_address, message, subject);
        }

        private SmtpClient Setup_Client(string config_FilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(@config_FilePath);
            SmtpClient client = new SmtpClient(lines[1]); //Server
            client.Port = Convert.ToInt32(lines[2]); //Port
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(lines[3], lines[4]);
            return client;
        }

        private MailMessage Setup_Message(string sender_email_address, string sender_name, string recipient_email_address, string message, string subject)
        {
            MailAddress from = new MailAddress(sender_email_address, sender_name);
            MailAddress to = new MailAddress(recipient_email_address);
            MailMessage msg = new MailMessage (from,to);
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            return msg;
        }

        public void Send()
        {
            client.SendAsync(msg,"sending...");
        }
    }

/****************************************************************************/

    /// <summary>
    /// This code is a direct duplication of the code posted on stack overflow posted by DmitryG on January 25, 2013
    /// https://stackoverflow.com/questions/14522540/close-a-messagebox-after-several-seconds/14522952
    /// </summary>

    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;

        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer)
                MessageBox.Show(text, caption);
        }

        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }

        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }

        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}
