using System;
using System.Collections.Generic;
using System.Linq;

namespace CqrsEventSourcing
{
    public class EventBroker {
        //1. All Events that happened.
        public IList<Event> AllEvents = new List<Event>();
        //2. Commands
        public event EventHandler<Command> Commands;
        //3. Query 
        public event EventHandler<Query> Queries;

        public void Command(Command  command) {
            Commands?.Invoke(this, command);
        }
        public T Query<T>(Query q)
        {
            Queries?.Invoke(this, q);
            return (T)q.Result;
        }
        public void UndoLast() 
        {
            var e = AllEvents.LastOrDefault();
            var ac = e as AgeChangeEvent;
            if (ac!=null)
            {
                Command(new ChangeAgeCommand(ac.Target, ac.OldValeue) { Register = false});
                AllEvents.Remove(e);
            }
        }
    }
}
