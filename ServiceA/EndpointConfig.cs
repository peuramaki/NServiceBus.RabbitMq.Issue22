
using NServiceBus.Features;

namespace ServiceA
{
    using NServiceBus;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/

    public class EndpointConfig : IConfigureThisEndpoint, AsA_Client, UsingTransport<RabbitMQ>, IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Serialization.Json();
            Configure.With(AllAssemblies.Except("foobar"));



        }
    }
}