using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Prototype_Auction_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            BiddingSystem biddingSystem = new BiddingSystem();
            TcpClient client = new TcpClient("localhost", 11000);
            Console.WriteLine("Connected to server...");

            NetworkStream stream = client.GetStream();
            biddingSystem.InitializeClient(stream);

            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            
            

            
            

            Console.ReadKey();
        }

        
    }
}
