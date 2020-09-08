using System;
using Stat;

namespace Components
{
    public class ComponentState<C>
    {
        public C component { get; set; }
        public Status status { get; set; }

        public ComponentState(C component, Status status)
        {
            this.component = component;
            this.status = status;
        }
    }

}
