using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.MessageMutator;

namespace Common
{
    public class OutgoingTransportMessageMutator : IMutateOutgoingTransportMessages
    {
        public void MutateOutgoing(object[] messages, TransportMessage transportMessage)
        {
            Console.WriteLine("Message '{0}', ReplyToAddress '{1}'.", messages[0].GetType().Name, transportMessage.ReplyToAddress);
        }
    }
}
