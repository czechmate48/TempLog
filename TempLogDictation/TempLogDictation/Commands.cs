using ActionEvents;
using Instruction;
using System;
using System.Collections.Generic;
using System.Media;

namespace Commands
{
    public class CommandLibrary
    {
        public Dictionary<string, Command> cmd_library { get; set; }

        public CommandLibrary(List<Command> cmds)
        {
            cmd_library = new Dictionary<string, Command>();

            foreach (Command cmd in cmds)
            {
                cmd_library[cmd.cue] = cmd;
            }
        }

        public string[] Get_Command_Cues()
        {
            List<string> cues = new List<string>();
            foreach (Command command in cmd_library.Values) cues.Add(command.cue);
            return cues.ToArray();
        }

        public Command Get_Command_From_Cue(string cue)
        {
            foreach (Command command in cmd_library.Values)
            {
                if (command.cue == cue) return command;
            }

            return null;
        }
    }

    public class Command
    {
        public string cue { get; set; }
        public SoundPlayer sound { get; }
        public List<ActionEvent> actions { get; }

        public Command(string cue, SoundPlayer sound, List<ActionEvent> actions)
        {
            this.cue = cue;
            this.sound = sound;
            this.actions = actions;
        }

        public Command(string cue, List<ActionEvent> actions)
        {
            this.cue = cue;
            this.actions = actions;
        }

        public Command(string cue)
        {
            this.cue = cue;
        }

        public void Add_ActionEvent(ActionEvent action)
        {
            actions.Add(action);
        }

        public Task_Sequence Generate_Task_Sequence()
        {
            List<Task> tasks = new List<Task>();

            foreach (ActionEvent action in actions)
            {
                foreach (Task task in action.Fire())
                {
                    tasks.Add(task);
                }
            }

            return new Task_Sequence(tasks);

        }

        public void PlaySound()
        {
            if (sound != null)
            {
                sound.Play();
            }
        }
    }
}

    






