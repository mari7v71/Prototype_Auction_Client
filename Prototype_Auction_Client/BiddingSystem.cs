using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Prototype_Auction_Client
{
    public class BiddingSystem
    {
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        Thread receiveMessagesThread;
        Thread makeBidsThread;

        public void InitializeClient(NetworkStream stream)
        {
            this.stream = stream;
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            ReceiveGoodsInfo();

            ThreadStart receiveMessagesDelegate = new ThreadStart(ReceiveServerMessage);
            receiveMessagesThread = new Thread(receiveMessagesDelegate);
            receiveMessagesThread.Start();

            ThreadStart makeBidsDelegate = new ThreadStart(MakeBid);
            makeBidsThread = new Thread(makeBidsDelegate);
            makeBidsThread.Start();
        }

        void MakeBid()
        {
            while(true)
            {
                string bid = Console.ReadLine();
                try
                {
                    double.Parse(bid);
                    writer.WriteLine(bid);
                }
                catch
                {
                    Console.WriteLine("Wrong input format. Try again...");
                }
            }     
        }

        void ReceiveGoodsInfo()
        {
            string receivedLine = reader.ReadLine();
            Console.WriteLine(receivedLine);
        }
        void ReceiveServerMessage()
        {
            while (true)
            {
                string receivedLine = reader.ReadLine();

                if(receivedLine == "fail")
                {
                    Console.WriteLine("The item was sold to other bidder!");
                    makeBidsThread.Abort();
                    return;
                }
                else if (receivedLine == "sucess")
                {
                    Console.WriteLine("You bought the item!");
                    makeBidsThread.Abort();
                    return;
                }

                Console.WriteLine(receivedLine);
            }
            
        }
    }
}
