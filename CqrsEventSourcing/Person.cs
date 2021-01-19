namespace CqrsEventSourcing
{
    //CQRS: Command query responsibility segregation: The idea behind this is the typical component
    //Does not communicate directly or does not give a direct interface for changing something
    //instead it receives a command or query and it responds on the basis of what it received.
    //Command: Command is when you want an object to do or change something.ex send command to ChangeAge etc.
    //Query: Query is when you want to get the data using object.
    //CQS: Command query separation

    //Event Sourcing: The idea here is that all the changes are encapsulated as events. these events are all serializable
    //therefore you can track all the changes that lead to its current value. The advantage is that you can review and rollback
    //to whatever time you want it to be rollbacked to. At any time you can take your system throughout its entire lifetime using
    //event sourcing.


    public class Person {
        private int age;
        EventBroker broker;

        public Person(EventBroker broker)
        {
            this.broker = broker;
            this.broker.Commands += Broker_Commands;
            this.broker.Queries += Broker_Queries;
        }

        private void Broker_Queries(object sender, Query query)
        {
            var aq = query as AgeQuery;
            if (aq!=null && aq.Target== this)
            {
                //since the return type is void here and this is where we need to return the response
                // we need to set it to something in the base Query class
                aq.Result = age;
            }
        }

        private void Broker_Commands(object sender, Command command)
        {
            var cac = command as ChangeAgeCommand;
            if (cac!=null && cac.Target == this)
            {
                if(cac.Register) broker.AllEvents.Add(new AgeChangeEvent(this, age, cac.Age));
                age = cac.Age;
            }
        }
        
    }
}
