﻿using System;
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

//ATTRIBUTIONS
//https://www.youtube.com/watch?v=AB9lfHDOe5U
//https://docs.microsoft.com/en-us/dotnet/api/system.speech.recognition.speechrecognitionengine.-ctor?view=netframework-4.8

    //FIXME -> Currently logging more than one value at instantion. Possibly a loop somewhere or the "record temp" command 
    //has not been filtered out of a buffer?

namespace TempLogDictation
{
    public partial class TempLog : Form
    {
        //COMMANDS
        private const string SAVE_TEMP = "save temp";
        private const string SEND = "send";
        private const string HELP = "help";
        private const string STOP = "stop";

        private enum Modes { STANDBY, DICTATION};
        private Modes curMode = Modes.STANDBY; //Default to standby mode
        private string[] standby_mode_commands = new string[] { SAVE_TEMP, HELP };
        private List<string> default_dictation_mode_commands = new List<string> { SEND, HELP, STOP };
        private string[] complete_dictation_mode_commands;
        private string[] username_commands;
        private string[] temperature_commands;

        private string username_file_path = "C:\\Users\\cnsef\\Documents\\Names.txt"; //FIXME -> Change to relative path
        private string temperature_file_path = "C:\\Users\\cnsef\\Documents\\Temperatures.txt"; //FIXME -> Change to relative path

        private SpeechRecognitionEngine srEngine = new SpeechRecognitionEngine(new CultureInfo ("en-US"));

/***************************/

        public TempLog()
        {
            InitializeComponent();
        }

        private void TempLog_Load(object sender, EventArgs e)
        {
            srEngine.SetInputToDefaultAudioDevice();
            Setup_Dictation_Mode_Commands();
            ListenFor_Initial_Command();
        }

        private void Setup_Dictation_Mode_Commands()
        {
            username_commands = Load_File_As_Commands(username_file_path);

            foreach (string name in username_commands)
            {
                default_dictation_mode_commands.Add(name);
            }

            temperature_commands = Load_File_As_Commands(temperature_file_path);

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

        private void ListenFor_Initial_Command()
        {
            GrammarBuilder builder = new GrammarBuilder(new Choices(standby_mode_commands));
            Grammar grammar = new Grammar(builder);

            srEngine.LoadGrammarAsync(grammar);
            srEngine.RecognizeAsync(RecognizeMode.Multiple);
            srEngine.SpeechRecognized += React_To_Command;
        }

/**************************/

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

        private void React_To_Command(object sender, SpeechRecognizedEventArgs e)
        {
            srEngine.SpeechRecognized -= React_To_Command; //Prevents commands from being called more than once

            if (curMode == Modes.STANDBY) React_To_Standby_Mode_Command(e.Result.Text);
            else if (curMode == Modes.DICTATION) React_To_Dictation_Mode_Command(e.Result.Text);
        }

        private void React_To_Standby_Mode_Command(string command)
        {
            switch (command)
            {
                case SAVE_TEMP:
                    Set_Mode(Modes.DICTATION); //Always set mode first to allow GUI to appropriately update
                    Update_Gui();
                    Listen_For_Commands(default_dictation_mode_commands.ToArray());
                    break;
            }
        }

        private void React_To_Dictation_Mode_Command(string command)
        {
            switch (command)
            {
                case SEND:
                    if (!Check_For_Name_Missing_Error() && !Check_For_Temp_Missing_Error())
                    {
                        AutoClosingMessageBox.Show("Have a great day!!!", "Temperature sent", 3000);
                        Set_Mode(Modes.STANDBY);
                        Update_Gui();  
                    }
                    Listen_For_Commands(standby_mode_commands);
                    break;
                case STOP:
                    Set_Mode(Modes.STANDBY);
                    Update_Gui();
                    break;
            }

            foreach (string username in username_commands)
            {
                if (command == username)
                {
                    name_TextArea.Text = username;
                    Listen_For_Commands(complete_dictation_mode_commands);
                }
            }

            foreach (string temp in temperature_commands)
            {
                if (command == temp)
                {
                    temperature_textArea.Text = temp;
                    Listen_For_Commands(complete_dictation_mode_commands);
                }
            }
        }

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

/**************************/

        private void Dictate_Click(object sender, EventArgs e)
        {
            if (curMode == Modes.DICTATION) React_To_Dictation_Mode_Command(STOP);
            else if (curMode == Modes.STANDBY) React_To_Standby_Mode_Command(SAVE_TEMP);
        }

        private void Email_Click(object sender, EventArgs e)
        {
            Check_For_Name_Missing_Error();
            Check_For_Temp_Missing_Error();
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

    }
/**************************/

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