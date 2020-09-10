using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Components;
using Modes;
using Stat;
using TempLogDictation;

namespace GUIStates
{
    /****************************/

    public class Component_Assembly
    {
        /// <summary>
        /// Holds all possible ComponentStates for all Components in the program. 
        /// These ComponentStates are compiled into a Status_Library for lookup and retrieval. 
        /// </summary>

        //COMPONENT STATUS
        public const string FILLED = "FILLED";
        public const string UNFILLED = "UNFILLED";
        public const string CLICKED = "CLICKED";
        public const string UNCLICKED = "UNCLICKED";
        public const string STANDBY = "STANDBY";
        public const string DICTATE = "DICTATE";
        public const string FAIL = "FAIL";
        public const string PASS = "PASS";
        public const string ON = "ON"; //email,log on
        public const string OFF = "OFF"; //email,log off

        public Status_Library cs_library { get; set; }

        public Component_Assembly()
        {
            cs_library = new Status_Library(new List<Status> { new Status(FILLED), new Status(UNFILLED), new Status(CLICKED), new Status(UNCLICKED),
                new Status(STANDBY), new Status(DICTATE), new Status(FAIL), new Status(PASS), new Status(ON), new Status(OFF)});
        }
    }

    public class GUIState_Factory
    {
        /// <summary>
        /// Holds all ComponentState objects in the program. Each ComponentState is updated by the program and stored in the factory.
        /// Whenever the program needs a snapshot of what state all items are in, the developer calls Create_GUIState().
        /// Based on ComponentStates at that time, a GUIState is returned that references a popup. 
        /// The popup indicates whether the user action yeilds an error or success.
        /// </summary>

        private Component_Assembly assembler;

        public ComponentState<RichTextBox> Name_box_state { get; set; }
        public ComponentState<RichTextBox> Temp_box_state { get; set; }
        public ComponentState<Button> Dictate_btn_state { get; set; }
        public ComponentState<Button> Clear_btn_state { get; set; }
        public ComponentState<Button> Send_btn_state { get; set; }
        public ComponentState<Email> Email_state { get; set; }
        public ComponentState<Log> TempLog_state { get; set; }

        public GUIState_Factory(ComponentState<RichTextBox> name_box_state, ComponentState<RichTextBox> temp_box_state, ComponentState<Button> dictate_btn_state,
            ComponentState<Button> clear_btn_state, ComponentState<Button> send_btn_state, ComponentState<Email> email_state, ComponentState<Log> tempLog_state)
        {
            this.assembler = new Component_Assembly();
            this.Name_box_state = name_box_state;
            this.Temp_box_state = temp_box_state;
            this.Dictate_btn_state = dictate_btn_state;
            this.Clear_btn_state = clear_btn_state;
            this.Send_btn_state = send_btn_state;
            this.Email_state = email_state;
            this.TempLog_state = tempLog_state;
        }

        public GUIState Create_GUIState()
        {
            if (Name_Error_Present()) return new Name_Error();
            else if (Temp_Error_Present()) return new Temp_Error();
            else if (Email_Error_Present()) return new Email_Error();
            else if (Log_Error_Present()) return new Log_Error();
            else return new No_Error();
        }

        private bool Name_Error_Present()
        {
            Status uf = new Status(Component_Assembly.UNFILLED);
            Status cl = new Status(Component_Assembly.CLICKED);

            return (Name_box_state.Status.name == uf.name && Send_btn_state.Status.name == cl.name);
        }

        private bool Temp_Error_Present()
        {
            Status uf = new Status(Component_Assembly.UNFILLED);
            Status cl = new Status(Component_Assembly.CLICKED);

            return (Temp_box_state.Status.name == uf.name && Send_btn_state.Status.name == cl.name);
        }

        private bool Email_Error_Present()
        {
            Status fl = new Status(Component_Assembly.FAIL);
            Status cl = new Status(Component_Assembly.CLICKED);

            return (Email_state.Status.name == fl.name && Send_btn_state.Status.name == cl.name);
        }

        private bool Log_Error_Present()
        {
            Status fl = new Status(Component_Assembly.FAIL);
            Status cl = new Status(Component_Assembly.CLICKED);

            return (TempLog_state.Status.name == fl.name && Send_btn_state.Status.name == cl.name);
        }
    }

    /****************************/

    public class GUIState
    {
        public PopUp pup;

        public GUIState() { }

        public virtual void DisplayPopUp()
        {
            pup.Display();
        }
    }

    public class Name_Error : GUIState
    {
        public Name_Error()
        {
            pup = PopUp_Factory.Create_PopUp(PopUp_Factory.Type.NAME_MISSING);
        }

        public override void DisplayPopUp()
        {
            pup.Display();
        }
    }

    public class Temp_Error : GUIState
    {
        public Temp_Error()
        {
            pup = PopUp_Factory.Create_PopUp(PopUp_Factory.Type.TEMP_MISSING);
        }

        public override void DisplayPopUp()
        {
            pup.Display();
        }
    }

    public class Email_Error : GUIState
    {
        public Email_Error()
        {
            pup = PopUp_Factory.Create_PopUp(PopUp_Factory.Type.EMAIL_ERROR);
        }

        public override void DisplayPopUp()
        {
            pup.Display();
        }
    }

    public class Log_Error : GUIState
    {
        public Log_Error()
        {
            pup = PopUp_Factory.Create_PopUp(PopUp_Factory.Type.LOG_ERROR);
        }

        public override void DisplayPopUp()
        {
            pup.Display();
        }
    }

    public class No_Error : GUIState
    {
        public No_Error()
        {
            pup = PopUp_Factory.Create_PopUp(PopUp_Factory.Type.SUCCESS);
        }

        public override void DisplayPopUp()
        {
            pup.Display();
        }
    }
}




