using System;
using System.Collections.Generic;

namespace Stat
{
    public class Status_Library
    {
        /// <summary>
        /// Wraps a dictionary value that holds Status objects. 
        /// Key will automaically be generated and set to the value in the Status.name variable.
        /// </summary>

        public Dictionary<string, Status> status_library = new Dictionary<string, Status>();

        public Status_Library() { }

        public Status_Library(List<Status> statuses)
        {
            foreach (Status status in statuses) status_library[status.name] = status;
        } 

        public Status Get_Status(string name)
        {
            return status_library[name];
        }
    }

    public class Status
    {
        /// <summary>
        /// Wrapper class for a string object 'name'. 
        /// Intended to be saved in a 'Status_Library' and used as an element of a ComponentState.
        /// </summary>

        public string name { get; set; }

        public Status(string status)
        {
            this.name = status;
        }
    }
}


