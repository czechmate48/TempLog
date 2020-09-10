using System;
using System.Windows.Forms;
using System.Drawing;
using Components;
using GUIStates;
using Modes;
using Stat;
using System.Collections.Generic;
using Instruction;

namespace ActionEvents
{
    /// <summary>
    /// Action Events create a list of Tasks that are later used to create a Task_Sequence by a Command.
    /// These Tasks should hold significance to the program be used to point to code for executing an programmatic operation. 
    /// </summary>

    public interface ActionEvent
    {
        List<Task> Fire();
    }

    public class Set_Dictation_Mode_Active: ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.SET_MODE_DICTATE);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            tasks.Add(Task.START_TIMEOUT_COUNTER);
            return tasks;
        }
    }

    public class Set_Button_Standby: ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.SET_DICTATE_BUTTON_GREEN);
            tasks.Add(Task.SET_BUTTON_TEXT_SAVE_TEMP);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            return tasks;
        }
    }

    public class Set_Button_Dicatate: ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.SET_DICTATE_BUTTON_RED);
            tasks.Add(Task.SET_BUTTON_TEXT_STOP);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            return tasks;
        }
    }

    public class Set_Standby_Mode_Active: ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.SET_MODE_STANDBY);
            tasks.Add(Task.STOP_TIMEOUT_COUNTER);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            return tasks;
        }
    }

    public class Clear_Name_Temp: ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_VOICE_RECOGNITION); 
            tasks.Add(Task.CLEAR_NAME);
            tasks.Add(Task.CLEAR_TEMP);
            tasks.Add(Task.SET_NAME_UNFILLED);
            tasks.Add(Task.SET_TEMP_UNFILLED);
            tasks.Add(Task.UPDATE_GUI);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            return tasks;
        }
    }

    public class Reset_Timeout_Counter : ActionEvent
    {
        //This class Prevents looping when resetting timeout counter on timeout. 
        //This is because "clear" is called when the timer runs out even if the button is not manually pressed.
        //If it is called but the button is not pressed, the ActionEvent triggers a Dictate-Standby mode switching loop.

        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_TIMEOUT_COUNTER);
            tasks.Add(Task.START_TIMEOUT_COUNTER);
            return tasks;
        }
    }

    public class Send_TempReport: ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_TIMEOUT_COUNTER);
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.SET_SEND_BUTTON_CLICKED);
            tasks.Add(Task.SEND_EMAIL);
            tasks.Add(Task.SEND_LOG);
            tasks.Add(Task.DISPLAY_POPUP);
            tasks.Add(Task.SET_NAME_UNFILLED);
            tasks.Add(Task.SET_TEMP_UNFILLED);
            tasks.Add(Task.RESET_EMAIL_STATUS);
            tasks.Add(Task.RESET_LOG_STATUS);
            tasks.Add(Task.SET_DICTATE_BUTTON_GREEN);
            tasks.Add(Task.SET_BUTTON_TEXT_SAVE_TEMP);
            tasks.Add(Task.SET_MODE_STANDBY);
            tasks.Add(Task.SET_SEND_BUTTON_UNCLICKED);
            tasks.Add(Task.CLEAR_NAME);
            tasks.Add(Task.CLEAR_TEMP);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            return tasks;
        }
    }

    public class Fill_Name: ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_TIMEOUT_COUNTER);
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.WRITE_NAME);
            tasks.Add(Task.SET_NAME_FILLED);
            tasks.Add(Task.UPDATE_GUI);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            tasks.Add(Task.START_TIMEOUT_COUNTER);
            return tasks;
        }
    }

    public class Fill_Temp : ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_TIMEOUT_COUNTER);
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.WRITE_TEMP);
            tasks.Add(Task.SET_TEMP_FILLED);
            tasks.Add(Task.UPDATE_GUI);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            tasks.Add(Task.START_TIMEOUT_COUNTER);
            return tasks;
        }
    }
}

