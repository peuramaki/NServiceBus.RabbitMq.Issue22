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




