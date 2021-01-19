using System;
using System.Linq;

namespace CqrsEventSourcing
{
    class Program
    {        
        static void Main(string[] args)
        {
            var eb = new EventBroker();
            Person p = new Person(eb);
            //To set the age of a person we need to issue a command here
            //Our event broker should have an api to subcribe to the events
            int age = 0;            
            eb.Command(new ChangeAgeCommand(p, 81) {});
            foreach (var e in eb.AllEvents)
            {
                Console.WriteLine(e);
            }
            //Running the query to get the age again after update
            age = eb.Query<int>(new AgeQuery() { Target = p });
            Console.WriteLine(age);
            eb.UndoLast();
            foreach (var e in eb.AllEvents)
            {
                Console.WriteLine(e);
            }
            //Running the query to get the age again after undo
            age = eb.Query<int>(new AgeQuery() { Target = p });
            Console.WriteLine(age);
            Console.ReadKey();

        }
    }
}
