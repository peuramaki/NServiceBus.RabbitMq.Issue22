using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Common;
using DAL;
using NServiceBus;
using NServiceBus.Config;
using NServiceBus.Persistence.NHibernate;
using NServiceBus.UnitOfWork;


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

            // use Enlist=true in connectionstring for 'ServiceB/SqlConnection'
            Configure.Component<SqlConnectionManagerWithSqlTransaction>(DependencyLifecycle.InstancePerUnitOfWork);
            
            // use Enlist=false in connectionstring for 'ServiceB/SqlConnection'
            //Configure.Component<SqlConnectionManagerWithoutSqlTransaction>(DependencyLifecycle.InstancePerUnitOfWork);

        
        }

    }
}
