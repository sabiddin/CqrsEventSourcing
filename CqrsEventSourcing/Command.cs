using System;

namespace CqrsEventSourcing
{
    public class Command: EventArgs
    {
        public bool Register = true;
    }
}