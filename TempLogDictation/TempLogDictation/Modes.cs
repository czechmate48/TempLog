using System;
using Commands;

namespace Modes
{
    public class Mode
    {
        public CommandLibrary cmd_library { get; set; }

        public Mode(CommandLibrary cmd_library)
        {
            this.cmd_library = cmd_library;
        }

        public Mode() { } //Needed for initialization

    }
}




