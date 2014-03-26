
using NServiceBus.Features;



namespace ServiceB
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, UsingTransport<RabbitMQ>, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.With(AllAssemblies.Except("foobar"));
                

            Configure.Features.Enable<Sagas>();
            Configure.Serialization.Json();



        }
    }
}
