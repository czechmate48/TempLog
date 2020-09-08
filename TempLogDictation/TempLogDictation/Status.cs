using System;
using System.Collections.Generic;

namespace Stat
{
    public class Status_Library
    {
        public Dictionary<string, Status> status_library = new Dictionary<string, Status>();

        public Status_Library() { }

        public Status_Library(List<Status> statuses)
        {
            foreach (Status status in statuses)
            {
                status_library[status.name] = status;
            }
        } 

        public Status Get_Status(string name)
        {
            return status_library[name];
        }
    }

    public class Status
    {
        public string name { get; set; }

        public Status(string status)
        {
            this.name = status;
        }
    }
}


