﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NBitcoin;
using NBitcoin.Protocol;
using Stratis.Bitcoin.BlockStore;
using Stratis.Bitcoin.Builder;
using Stratis.Bitcoin.Configuration;
using Stratis.Bitcoin.Consensus;
using Stratis.Bitcoin.Logging;
using Stratis.Bitcoin.MemoryPool;
using Stratis.Bitcoin.Miner;
using Stratis.Bitcoin.RPC;

namespace Stratis.StratisD
{
    public class Program
    {
        public static void Main(string[] args)
        {
			Logs.Configure(new LoggerFactory().AddConsole(LogLevel.Trace, false));
			NodeSettings nodeSettings = NodeSettings.FromArguments(args, Network.StratisMain, ProtocolVersion.ALT_PROTOCOL_VERSION);
	        
			// NOTES
			// - for now only download the stratis chain form peers
			// - adding consensus requires bigger changes
			// - running the nodes side by side is not possible yet as the flags for serialization are static

			var node = new FullNodeBuilder()
				.UseNodeSettings(nodeSettings)
				.Build();

			// TODO: bring the logic out of IWebHost.Run()
			node.Start();
			Console.WriteLine("Press any key to stop");
			Console.ReadLine();
			node.Dispose();
		}
    }
}
