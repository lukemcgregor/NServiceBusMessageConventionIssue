using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			Configure.With()
				.DefineEndpointName("my.service.input")
				.DefiningCommandsAs(t => t.Namespace != null && t.Namespace.Contains(".Commands"))
				.DefiningEventsAs(t => t.Namespace != null && t.Namespace.Contains(".Events"))
				.DefiningMessagesAs(t => t.Namespace != null && t.Namespace.Contains(".Messages"))
				.DefaultBuilder()
				.UseTransport<Msmq>()
					.PurgeOnStartup(false)
				.MsmqSubscriptionStorage("my.service")
				.UnicastBus()
					.LoadMessageHandlers()
					.ImpersonateSender(false)
				.EnablePerformanceCounters()
				.CreateBus()
				.Start(() => Configure.Instance.ForInstallationOn<NServiceBus.Installation.Environments.Windows>().Install());
		}
	}
}
