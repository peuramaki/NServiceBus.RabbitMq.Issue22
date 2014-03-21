using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using NServiceBus;

namespace ServiceA
{
    public class BootStrapper : IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Component<OutgoingTransportMessageMutator>(DependencyLifecycle.SingleInstance);
        }
    }
}
