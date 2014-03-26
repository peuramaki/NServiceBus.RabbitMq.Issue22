using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
        private int _count = 0;

        public void Start()
        {
            Console.WriteLine("Enter to run test.");
            Console.ReadLine();

            

            Parallel.For(0, 10, i =>
            {
                this.Bus.Send<IRequestStuff>("ServiceB", stuff =>
                {
                    stuff.AggregateRootId = Guid.NewGuid();
                    Console.WriteLine("sent: ({0}).", i);
                })

                    .Register(msg =>
                    {
                        var response = msg.Messages[0] as IResponseStuff;
                        Console.WriteLine("Here's response for aggregate '{0}' ({1}).", response.AggregateRootId, i);
                        _count ++;

                        if(_count == 10)
                            Console.WriteLine("10 ok");
                    });
            });
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
