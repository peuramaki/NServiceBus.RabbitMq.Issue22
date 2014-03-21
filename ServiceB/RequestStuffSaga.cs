using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using NServiceBus;
using NServiceBus.Saga;
using ServiceA.Messages;
using ServiceB.Messages;

namespace ServiceB
{
    public class RequestStuffSaga : Saga<RequestStuffSagaData>, IAmStartedByMessages<IRequestStuff>, IHandleMessages<ISomethingCreated>
    {

        public SqlServerClient SqlServerClient { get; set; }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<ISomethingCreated>(created => created.AggregateRootId).ToSaga(s => s.AggregateRootId);
        }

        public void Handle(IRequestStuff message)
        {
            this.Data.AggregateRootId = message.AggregateRootId;
            
            this.SqlServerClient.CraeteSomething(this.Data.AggregateRootId);

            for(var i = 0; i< 15 ; i++)
            { 
                this.SqlServerClient.UpdateSomethingCount(this.Data.AggregateRootId,i);
                this.Bus.Send<ICreateSomething>(something => { something.AggregateRootId = this.Data.AggregateRootId; });
            }
        }

        public void Handle(ISomethingCreated message)
        {
            var count = this.SqlServerClient.GetSomething(this.Data.AggregateRootId).Item2;

            this.Data.ArrivedSomethingCount++;
            if(this.Data.ArrivedSomethingCount < 15)
                return;

            this.ReplyToOriginator<IResponseStuff>(stuff => { stuff.AggregateRootId = this.Data.AggregateRootId; });
        }
    }
}
