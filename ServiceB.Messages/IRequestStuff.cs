using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace ServiceB.Messages
{
    public class IRequestStuff : IMessage
    {
        public Guid AggregateRootId { get; set; }
    }
}
