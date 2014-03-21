using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace ServiceA.Messages
{
    public class IResponseStuff : IMessage
    {
        public Guid AggregateRootId { get; set; }
    }
}
