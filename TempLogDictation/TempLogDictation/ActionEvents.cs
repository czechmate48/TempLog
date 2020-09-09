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
    public interface ActionEvent
    {
        List<Task> Fire();
    }

    public class Set_Dictation_Mode_Active: ActionEvent
    {
        public List<Task> Fire()
        {
            return new List<Task>() { Task.STOP_VOICE_RECOGNITION, Task.SET_MODE_DICTATE, Task.START_VOICE_RECOGNITION };
        }
    }

    public class Set_Button_Standby: ActionEvent
    {
        public List<Task> Fire()
        {
            return new List<Task>() { Task.STOP_VOICE_RECOGNITION, Task.SET_DICTATE_BUTTON_GREEN,
                Task.SET_BUTTON_TEXT_SAVE_TEMP, Task.START_VOICE_RECOGNITION };
        }
    }

    public class Set_Button_Dicatate: ActionEvent
    {
        public List<Task> Fire()
        {
            return new List<Task>() { Task.STOP_VOICE_RECOGNITION, Task.SET_DICTATE_BUTTON_RED,
                Task.SET_BUTTON_TEXT_STOP, Task.START_VOICE_RECOGNITION };
        }
    }

    public class Set_Standby_Mode_Active: ActionEvent
    {
        public List<Task> Fire()
        {
            return new List<Task>() { Task.STOP_VOICE_RECOGNITION, Task.SET_MODE_STANDBY,
                Task.START_VOICE_RECOGNITION };
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

    public class Send_TempReport: ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
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
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.WRITE_NAME);
            tasks.Add(Task.SET_NAME_FILLED);
            tasks.Add(Task.UPDATE_GUI);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            return tasks;
        }
    }

    public class Fill_Temp : ActionEvent
    {
        public List<Task> Fire()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.STOP_VOICE_RECOGNITION);
            tasks.Add(Task.WRITE_TEMP);
            tasks.Add(Task.SET_TEMP_FILLED);
            tasks.Add(Task.UPDATE_GUI);
            tasks.Add(Task.START_VOICE_RECOGNITION);
            return tasks;
        }
    }
}

