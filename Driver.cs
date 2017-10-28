using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment2
{
    class Driver
    {
        public static confirmationBuffer confirmBuffer = new confirmationBuffer();
        public static MultiCellBuffer orderBuffer = new MultiCellBuffer();
        public static void Main(string[] args)
        {
            Thread[] dealerThreads = new Thread[5];
            Plant plant = new Plant();
            Thread[] plantThreads = new Thread[3];
            //Start new plant threads with each their own pricing model
            for (int i = 0; i < plantThreads.Length; i++)
            {
                plantThreads[i] = new Thread(new ThreadStart(() => plant.pricingModel("plant" + (i).ToString())));
                plantThreads[i].Start();
            }
            Dealer dealer = new Dealer();
            //Add the dealers into the price cut event to be notified
            Plant.priceCut += new priceCutEvent(dealer.saleOnCars);
            //Start dealer threads to check for order they have
            for (int i = 0; i < dealerThreads.Length; i++)
            {
                dealerThreads[i] = new Thread(new ThreadStart(() => dealer.dealerCheckOrder()));
                dealerThreads[i].Name = "Dealer " + (i + 1).ToString();
                dealerThreads[i].Start();
            }
            //Will iterate until the plant threads have been terminated, then proceeds to termination of dealer threads
            while (plantThreads[1].IsAlive == true || plantThreads[2].IsAlive || true && plantThreads[0].IsAlive == true)
            {
            }
            //terminate Dealer Threads once the plant threads have terminated
            for (int i = 0; i < dealerThreads.Length; i++)
                dealerThreads[i].Abort();
        }
    }
}
