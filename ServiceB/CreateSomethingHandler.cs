using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;
using ServiceB.Messages;

namespace ServiceB
{
    public class CreateSomethingHandler : IHandleMessages<ICreateSomething>
    {
        public IBus Bus { get; set; }

        public void Handle(ICreateSomething message)
        {
            MakeRestCall();
            this.Bus.Publish<ISomethingCreated>(created => created.AggregateRootId = message.AggregateRootId);
        }

        private void MakeRestCall()
        {
            Thread.Sleep(new Random().Next(1000));
        }
    }
}
