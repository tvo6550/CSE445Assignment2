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
            Plant plant = new Plant();
            //3 airline threads

            Dealer dealer = new Dealer();
            //Add the dealer.saleOnCars to the priceCutEvent

            //Create 5 dealer threads
        }
    }
}
