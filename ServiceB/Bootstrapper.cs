using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Common;
using DAL;
using NServiceBus;
using NServiceBus.Persistence.NHibernate;

namespace ServiceB
{
    /// <summary>
    /// Centralized place for NServiceBus dependency injection config.
    /// </summary>
    public class BootStrapper : IWantCustomInitialization
    {
        public void Init()
        {
            //use NHibernate (Sql server) for persistence instead of local RavenDB
            NHibernatePersistence.UseAsDefault();

            Configure.Component<OutgoingTransportMessageMutator>(DependencyLifecycle.SingleInstance);
            Configure.Component<SqlServerClient>(DependencyLifecycle.InstancePerUnitOfWork);

        }
    }
}
