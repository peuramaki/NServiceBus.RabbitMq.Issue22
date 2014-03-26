using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace ServiceB.Messages
{
    public class ICreateSomething : ICommand
    {
        public Guid AggregateRootId { get; set; }
        public int Count { get; set; }
    }
}
