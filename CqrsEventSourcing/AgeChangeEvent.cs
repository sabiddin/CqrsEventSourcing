using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsEventSourcing
{
    public class AgeChangeEvent: Event
    {
        public Person Target;      
        public int OldValeue, NewValue;

        public AgeChangeEvent(Person target, int oldValeue, int newValue)
        {
            Target = target;
            OldValeue = oldValeue;
            NewValue = newValue;            
        }
        public override string ToString()
        {
            return $"Age Changed From: {OldValeue} To: {NewValue}";
        }

    }
}
