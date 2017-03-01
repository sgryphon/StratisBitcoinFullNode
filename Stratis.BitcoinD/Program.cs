using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Stratis.Bitcoin;
using Stratis.Bitcoin.Configuration;
using Stratis.Bitcoin.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Stratis.BitcoinD
{
	public class Program
	{
		public static void Main(string[] args)
		{
			NodeArgs initialNodeArgs = NodeArgs.GetInitialArgs(args);

			var loggerFactory = Logs.GetLoggerFactory(initialNodeArgs);
			Logs.Configure(loggerFactory);

			NodeArgs nodeArgs = NodeArgs.GetRemainingArgs(args, initialNodeArgs);
			FullNode node = new FullNode(nodeArgs);
			CancellationTokenSource cts = new CancellationTokenSource();
			new Thread(() =>
			{
				Console.WriteLine("Press one key to stop");
				Console.ReadLine();
				node.Dispose();
			})
			{
				IsBackground = true //so the process terminate
			}.Start();
			node.Start();			
			node.WaitDisposed();
			node.Dispose();
		}
	}
}
