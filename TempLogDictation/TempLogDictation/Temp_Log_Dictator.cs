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
using System.IO;
using System.Media;
using Modes;
using Commands;
using TempLogDictation;
using ActionEvents;
using Components;
using Stat;
using GUIStates;
using Instruction;
using System.Threading;

namespace TempLogDictation
{
    //FIXME -> When should sounds be called if both Popups and commands manage them?

    public partial class Temp_Log_Dictator : Form
    {
        private Mode cur_mode;
        private Mode standby;
        private Mode dictate;

        private GUIState_Factory gsfactory;
        private GUIState_Factory gsfactory_snapshot;

        private Command save_temp;
        private Command stop;
        private Command clear;
        private Command send;
        private List<Command> usernames;
        private List<Command> temperatures;

        private Configuration path_config = new Configuration("C:\\Users\\cnsef\\source\\repos\\TempLogDictation\\TempLogDictation\\bin\\Debug\\Configuration\\Path_Config.txt"); //FIXME -> Set relative
        private Configuration tempLog_config;
        private Configuration usernameCmds_config;
        private Configuration tempCmds_config;
        private Configuration email_config;

        private CommandLibrary standby_library;
        private CommandLibrary dictate_library;

        private Status email_on;
        private Email email = new Email();
        private Status tempLog_on;
        private Log tempLog = new Log();

        private SpeechRecognitionEngine srEngine;
        private Task_Sequence cur_task_sequence;

        /**************************/
        /* INITIALIZE             */
        /**************************/

        public Temp_Log_Dictator()
        {
            InitializeComponent();
        }

        private void TempLog_Load(object sender, EventArgs e)
        {
            Initialize_Modes(); //Create Modes here
            Initialize_GUIState_Factory();
            Initialize_Default_Commands();

            Load_Configuration_Files();
            Initialize_Username_Commands();
            Initialize_Temperature_Commands();

            Initialize_Command_Libraries();
            Load_Modes(); //Build Modes here
            Initialize_Email();
            Initialize_TempLog();

            Activate_Voice_Recognition();
        }

        private void Initialize_Modes()
        {
            cur_mode = new Mode();
            standby = new Mode();
            dictate = new Mode();
        }

        private void Initialize_GUIState_Factory()
        {
            ComponentState<RichTextBox> name_box_state = new ComponentState<RichTextBox>(name_rtb, new Status(Component_Assembly.UNFILLED));
            ComponentState<RichTextBox> temp_box_state = new ComponentState<RichTextBox>(temp_rtb, new Status(Component_Assembly.UNFILLED));
            ComponentState<Button> dictate_btn_state = new ComponentState<Button>(dictate_btn, new Status(Component_Assembly.UNCLICKED));
            ComponentState<Button> clear_btn_state = new ComponentState<Button>(clear_btn, new Status(Component_Assembly.UNCLICKED));
            ComponentState<Button> send_btn_state = new ComponentState<Button>(send_btn, new Status(Component_Assembly.UNCLICKED));
            ComponentState<Email> email_state = new ComponentState<Email>(email, new Status(Component_Assembly.OFF));
            ComponentState<Log> tempLog_state = new ComponentState<Log>(tempLog, new Status(Component_Assembly.OFF));

            gsfactory = new GUIState_Factory(name_box_state, temp_box_state, dictate_btn_state, clear_btn_state, send_btn_state, email_state, tempLog_state);
        }

        private void Initialize_Default_Commands()
        {
            save_temp = new Command("Save Temp", new SoundPlayer(@"C:\Windows\Media\Speech On.wav"),
                new List<ActionEvent> { new Set_Dictation_Mode_Active(), new Set_Button_Dicatate()});
            stop = new Command("Stop", new SoundPlayer(@"C:\Windows\Media\Speech Off.wav"),
                new List<ActionEvent> { new Set_Standby_Mode_Active(), new Clear_Name_Temp(), new Set_Button_Standby()});
            clear = new Command("Clear", new SoundPlayer(@"C:\Windows\Media\Windows Balloon.wav"), new List<ActionEvent> { new Clear_Name_Temp()});
            send = new Command("Send", new SoundPlayer(@"C:\Windows\Media\chord.wav"), new List<ActionEvent>{ new Send_TempReport() }); //Create empty command so send is present in cmd_library. Update with correct values in Command_Recognized()
        }

        private void Load_Configuration_Files()
        {
            path_config.Load();
            tempLog_config = new Configuration(path_config.contents["tempLog_config"]);
            usernameCmds_config = new Configuration(path_config.contents["usernameCmds_config"]);
            tempCmds_config = new Configuration(path_config.contents["tempCmds_config"]);
            email_config = new Configuration(path_config.contents["email_config"]);

            tempLog_config.Load();
            usernameCmds_config.Load();
            tempCmds_config.Load();
            email_config.Load();
        }

        private void Initialize_Username_Commands()
        {
            usernames = new List<Command>();

            //Key and Value pair equal same values
            foreach (KeyValuePair<string, string> value in usernameCmds_config.contents)
            {
                usernames.Add(new Command(value.Value, new SoundPlayer(@"C:\Windows\Media\ding.wav"), new List<ActionEvent> { new Fill_Name() }));
            }
        }

        private void Initialize_Temperature_Commands()
        {
            temperatures = new List<Command>();

            //Key and Value pair equal same values
            foreach (KeyValuePair<string, string> value in tempCmds_config.contents)
            {
                temperatures.Add(new Command(value.Value, new SoundPlayer(@"C:\Windows\Media\ding.wav"), new List<ActionEvent> { new Fill_Temp() }));
            }
        }

        private void Initialize_Command_Libraries()
        {
            List<Command> standby_commands = new List<Command> { save_temp };
            standby_library = new CommandLibrary(standby_commands);

            List<Command> dictate_commands = new List<Command> { stop, clear, send};
            foreach (Command cmd in usernames) dictate_commands.Add(cmd);
            foreach (Command cmd in temperatures) dictate_commands.Add(cmd);
            dictate_library = new CommandLibrary(dictate_commands);
        }

        private void Load_Modes()
        {
            standby.cmd_library = standby_library;
            dictate.cmd_library = dictate_library;
            cur_mode = standby;
        }

        private void Initialize_Email()
        {
            if (email_config.contents["email_on"] != "on")
            {
                email_on = new Status(Component_Assembly.OFF); //Used for reverting to original state after send
                return;
            } else
            {
                email_on = new Status(Component_Assembly.ON);
                gsfactory.email_state.status = email_on;
            }

            string server = email_config.contents["server"];
            int port = Convert.ToInt32(email_config.contents["port"]);
            string email_username = email_config.contents["email_username"];
            string email_password = email_config.contents["email_password"];
            string sender_email_address = email_config.contents["sender_email_address"];
            string sender_name = email_config.contents["sender_name"];
            string recipient_email_address = email_config.contents["recipient_email_address"];

            email = new Email(server, port, email_username, email_password, sender_email_address, sender_name,
                recipient_email_address, "Temperature Log", "No data provided"); //msg and subject setup at time of send
        }

        private void Initialize_TempLog()
        {
            if (tempLog_config.contents["tempLog_on"] != "on")
            {
                tempLog_on = new Status(Component_Assembly.OFF); //Used for reverting to original state after send
                return;
            }
            else
            {
                tempLog_on = new Status(Component_Assembly.ON);
                gsfactory.tempLog_state.status = tempLog_on;
            }

            if (tempLog_config.contents["tempLog_on"] != "on") return; //exit
            else gsfactory.tempLog_state.status = new Status(Component_Assembly.ON);

            tempLog.path = tempLog_config.contents["tempLog_savePath"];
        }

        /**************************/
        /* RUN                    */
        /**************************/

        private void Command_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            Command command = cur_mode.cmd_library.Get_Command_From_Cue(e.Result.Text);
            Command_Fire(command);
        }

        private void Command_Fire(Command command)
        {
            cur_task_sequence = command.Generate_Task_Sequence();
            gsfactory_snapshot = gsfactory;
            foreach (Instruction.Task task in cur_task_sequence.tasks) Command_Execute(command, task);
        }

        private void Command_Execute(Command command, Instruction.Task task)
        {
            switch (task)
            {
                case Instruction.Task.STOP_VOICE_RECOGNITION:       Disable_Voice_Recognition(); break;
                case Instruction.Task.START_VOICE_RECOGNITION:      Activate_Voice_Recognition(); break;
                case Instruction.Task.PLAY_COMMAND_SOUND:           command.sound.Play(); break;
                case Instruction.Task.CLEAR_NAME:                   name_rtb.Clear(); break;
                case Instruction.Task.CLEAR_TEMP:                   temp_rtb.Clear(); break;
                case Instruction.Task.SET_DICTATE_BUTTON_GREEN:     dictate_btn.BackColor = Color.Green; break;
                case Instruction.Task.SET_DICTATE_BUTTON_RED:       dictate_btn.BackColor = Color.Red; break;
                case Instruction.Task.SET_BUTTON_TEXT_SAVE_TEMP:    dictate_btn.Text = "SAVE TEMP"; break;
                case Instruction.Task.SET_BUTTON_TEXT_STOP:         dictate_btn.Text = "STOP"; break;
                case Instruction.Task.SET_MODE_DICTATE:             cur_mode = dictate; break;
                case Instruction.Task.SET_MODE_STANDBY:             cur_mode = standby; break;
                case Instruction.Task.SET_NAME_FILLED:              gsfactory.name_box_state.status = new Status(Component_Assembly.FILLED); break;
                case Instruction.Task.SET_NAME_UNFILLED:            gsfactory.name_box_state.status = new Status(Component_Assembly.UNFILLED); break;
                case Instruction.Task.SET_TEMP_FILLED:              gsfactory.temp_box_state.status = new Status(Component_Assembly.FILLED); break;
                case Instruction.Task.SET_TEMP_UNFILLED:            gsfactory.temp_box_state.status = new Status(Component_Assembly.UNFILLED); break;
                case Instruction.Task.SET_SEND_BUTTON_CLICKED:      gsfactory.send_btn_state.status = new Status(Component_Assembly.CLICKED); break;
                case Instruction.Task.SET_SEND_BUTTON_UNCLICKED:    gsfactory.send_btn_state.status = new Status(Component_Assembly.UNCLICKED); break;
                case Instruction.Task.WRITE_NAME:                   name_rtb.Text = command.cue; break;   
                case Instruction.Task.WRITE_TEMP:                   temp_rtb.Text = command.cue; break;
                case Instruction.Task.SEND_EMAIL:                   Send_Email(new TempReport(name_rtb.Text,temp_rtb.Text)); break;
                case Instruction.Task.SEND_LOG:                     Send_Log(new TempReport(name_rtb.Text, temp_rtb.Text)); break;
                case Instruction.Task.RESET_EMAIL_STATUS:           gsfactory.email_state.status = email_on; break;
                case Instruction.Task.RESET_LOG_STATUS:             gsfactory.tempLog_state.status = tempLog_on; break;
                case Instruction.Task.UPDATE_GUI:                   gsfactory.Create_GUIState(); break;
                case Instruction.Task.DISPLAY_POPUP:                GUIState gs = gsfactory.Create_GUIState(); gs.DisplayPopUp(); break;
                default: break;
            }
        }

        private void Disable_Voice_Recognition()
        {
            srEngine.SpeechRecognized -= Command_Recognized; 
        }

        private void Activate_Voice_Recognition()
        {
            srEngine = new SpeechRecognitionEngine(new CultureInfo("en-US"));
            srEngine.SetInputToDefaultAudioDevice();
            GrammarBuilder builder = new GrammarBuilder(new Choices(cur_mode.cmd_library.Get_Command_Cues()));
            srEngine.LoadGrammarAsync(new Grammar(builder));
            srEngine.RecognizeAsync(RecognizeMode.Multiple);
            srEngine.SpeechRecognized += Command_Recognized;
        }

        private void Send_Email(TempReport tr)
        {
            if (email_on.name == Component_Assembly.OFF ||
                !gsfactory.Create_GUIState().GetType().Equals(typeof(No_Error))) return; //Meaning there is an error

            try
            {
                email.message = tr.Get_Report();
                email.subject = tr.Get_Report();
                email.Update();
                email.Send();
                gsfactory.email_state.status = new Status(Component_Assembly.PASS);
            }
            catch
            {
                gsfactory.email_state.status = new Status(Component_Assembly.FAIL);
            }
        }

        private void Send_Log(TempReport tr)
        {
            if (tempLog_on.name == Component_Assembly.OFF ||
                !gsfactory.Create_GUIState().GetType().Equals(typeof(No_Error))) return; //Meaning there is an error

            try
            {
                tempLog.Write_TempReport(tr);
                gsfactory.tempLog_state.status = new Status(Component_Assembly.PASS);
            }
            catch
            {
                gsfactory.tempLog_state.status = new Status(Component_Assembly.FAIL);
            }
        }

        private void Dictate_Click(object sender, EventArgs e)
        {
            if (cur_mode == standby) Command_Fire(save_temp);
            else if (cur_mode == dictate) Command_Fire(stop);
        }

        private void Email_Click(object sender, EventArgs e)
        {
            if (name_rtb.TextLength > 0) { gsfactory.name_box_state.status = new Status(Component_Assembly.FILLED); }
            if (temp_rtb.TextLength > 0) { gsfactory.temp_box_state.status = new Status(Component_Assembly.FILLED); }
            Command_Fire(send);
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            Command_Fire(clear);
        }
    }
}



