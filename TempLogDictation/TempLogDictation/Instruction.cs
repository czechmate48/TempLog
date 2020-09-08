using System;
using System.Collections.Generic;

namespace Instruction
{
    public enum Task { SET_MODE_STANDBY, SET_MODE_DICTATE, SET_DICTATE_BUTTON_RED, SET_DICTATE_BUTTON_GREEN,
        SET_BUTTON_TEXT_SAVE_TEMP, SET_BUTTON_TEXT_STOP, CLEAR_NAME, CLEAR_TEMP, SET_NAME_FILLED,
        SET_NAME_UNFILLED, SET_TEMP_FILLED, SET_TEMP_UNFILLED, UPDATE_GUI, WRITE_NAME, WRITE_TEMP,
        SEND_EMAIL, SEND_LOG, RESET_EMAIL_STATUS, RESET_LOG_STATUS, DISPLAY_POPUP, STOP_VOICE_RECOGNITION,
        START_VOICE_RECOGNITION, SET_SEND_BUTTON_CLICKED, SET_SEND_BUTTON_UNCLICKED, PLAY_COMMAND_SOUND
        }

    public class Task_Sequence
    {
        public List<Task> tasks;

        public Task_Sequence(List<Task> tasks)
        {
            this.tasks = tasks;
        }

        public Task_Sequence()
        {
            this.tasks = new List<Task>();
        }

        public void Add_Task(Task task)
        {
            tasks.Add(task);
        }
    }
}


