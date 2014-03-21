using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using ServiceA.Messages;
using ServiceB.Messages;

namespace ServiceA
{
    public class TestRunner : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        public void Start()
        {
            Console.WriteLine("Enter to run test.");
            Console.ReadLine();

            this.Bus.Send<IRequestStuff>("ServiceB", stuff =>
            {
                stuff.AggregateRootId = Guid.NewGuid();
            })
            
            .Register(msg =>
            {
                var response = msg.Messages[0] as IResponseStuff;
                Console.WriteLine("Here's response for aggregate '{0}'.", response.AggregateRootId);
            });
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
