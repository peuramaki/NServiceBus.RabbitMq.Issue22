﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL;
using NServiceBus;
using ServiceB.Messages;

namespace ServiceB
{
    public class CreateSomethingHandler : IHandleMessages<ICreateSomething>
    {
        public IBus Bus { get; set; }
        public SqlServerClient SqlServerClient { get; set; }

        public void Handle(ICreateSomething message)
        {
            MakeRestCall();
            
            this.Bus.Publish<ISomethingCreated>(created => created.AggregateRootId = message.AggregateRootId);
        }

        private void MakeRestCall()
        {
            Thread.Sleep(1);
        }
    }
}
