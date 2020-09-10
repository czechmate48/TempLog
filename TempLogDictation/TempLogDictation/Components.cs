using System;
using Stat;

namespace Components
{
    /// <summary>
    /// Holds a component object of any state and allows the components "Status" to be changed. 
    /// See Status class for details
    /// </summary>
    /// <typeparam name="C"></typeparam>

    public class ComponentState<C>
    {
        public C Component { get; set; }
        public Status Status { get; set; }

        public ComponentState(C component, Status status)
        {
            this.Component = component;
            this.Status = status;
        }
    }
}
