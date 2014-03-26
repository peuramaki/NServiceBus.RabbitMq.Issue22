NServiceBus.RabbitMq.Issue22
============================

Issue 22 in NServiceBus.RabbitMQ reproduced

Problem with NServiceBus.RabbitMQ transaction handling is tracked down and reproduced here.

The issue
---------
When custom SQL queries to MS SQL Server (using SqlConnection) are made during the lifetime of a
NServiceBus message handler, non-deterministic behavior is resulted. Sometimes ambient transaction is
never commited, which results in NServiceBus.RabbitMQ never sending out messages that should be sent.

The workaround
--------------
Instead of letting SqlConnection to automatically enlist to ambient transaction, prevent it using 'Enlist=false'
in connection string. Hook up SqlTransaction using NServiceBus'es IManageUnitsOfWork interface.

Prerequisites
-------------
* VS2013
* SQL Server 2012 (older probably fine too)
* RabbitMQ server installed on local box
* Nuget.org accessible
* Two databases ('Issue22' and 'Issue22SagaPersistence') created on the sql server
* Solution items / CreateTables.sql run


To reproduce
------------
In ServiceB.Bootstrapper, include
```
Configure.Component<SqlConnectionManagerWithSqlTransaction>(DependencyLifecycle.InstancePerUnitOfWork);
```
and set connectionstring for ServiceB/SqlConnection (in ServiceB/app.config)
```
<add name="ServiceB/SqlConnection" connectionString="Server=.\SQL2012;Database=Issue22;Trusted_Connection=True;Enlist=false" providerName="System.Data.SqlClient" />
```


To see the workaround
---------------------
In ServiceB.Bootstrapper, include
```
Configure.Component<SqlConnectionManagerWithoutSqlTransaction>(DependencyLifecycle.InstancePerUnitOfWork);
```
and set connectionstring for ServiceB/SqlConnection (in ServiceB/app.config)
```
<add name="ServiceB/SqlConnection" connectionString="Server=.\SQL2012;Database=Issue22;Trusted_Connection=True;Enlist=true" providerName="System.Data.SqlClient" />
```



