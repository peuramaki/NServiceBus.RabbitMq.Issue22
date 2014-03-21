using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus.Saga;

namespace ServiceB
{
    public class RequestStuffSagaData : IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual string Originator { get; set; }
        public virtual string OriginalMessageId { get; set; }

        public virtual Guid AggregateRootId { get; set; }

        public virtual int ArrivedSomethingCount { get; set; }

    }
}
