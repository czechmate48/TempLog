using System;
using Commands;

namespace Modes
{
    public class Mode
    {
        /// <summary>
        /// Wrapper class that holds various command libraries
        /// </summary>

        public CommandLibrary Cmd_library { get; set; }

        public Mode() { }

        public Mode(CommandLibrary cmd_library)
        {
            this.Cmd_library = cmd_library;
        }
    }
}




