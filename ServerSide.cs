using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment2
{
    public delegate void priceCutEvent(string plantName, int amount, double prevPrice, double curPrice);
    class ServerSide
    {
    }
    public static class Decoder
    {
        public static OrderClass decode(string order)
        {
            string[] tokens = order.Split('#');
            OrderClass orderObj = new OrderClass(tokens[0], Int32.Parse(tokens[1]), tokens[2], Int32.Parse(tokens[3]), Int32.Parse(tokens[4]));
            return orderObj;
        }
    }
    public class Plant
    {
        static Random rng = new Random();
        public static event priceCutEvent priceCut;
        private static int p = 0;
        private static double price = 250;
        private static int amountOfUnits = 1000; //default number of units
        private OrderProcessing op = new OrderProcessing();
       
        public int getP()
        {
            return p;
        }

        public static void changePrice(string plantName, int prevAmount, double prevPrice, double newPrice)
        {
            if (newPrice < price)
            {
                if (priceCut != null)
                {
                    Console.WriteLine("price cut for " + plantName + " for " + newPrice);
                    priceCut(plantName, prevAmount, prevPrice, newPrice);
                    p++;
                }
            }
            price = newPrice;
        }

        public void pricingModel(string name)
        {
            while (p < 20)
            {
                Console.WriteLine("Thread for" + name);
                Console.WriteLine("p = " + p);
                Thread.Sleep(500);
                string orderDescrip = "";
                orderDescrip = Driver.orderBuffer.getOneCell();
                if(orderDescrip != "")
                {
                    OrderClass order = Decoder.decode(orderDescrip);
                    if (name == order.getReceiverId())
                    {
                        amountOfUnits = order.getAmount();
                        Thread orderProc = new Thread(new ThreadStart(() => op.procOrder(order)));
                        orderProc.Start();
                        Driver.orderBuffer.eraseOneCell(Encoder.encode(order));
                    }
                }

                double value = rng.Next(50, 500);
                Plant.changePrice(name, amountOfUnits, price, value);
            }
        }

        

    }
    class OrderProcessing
    {
        public void procOrder(OrderClass order)
        {
            int cardNo = order.getCardNo();

            if (cardNo > 5000 && cardNo < 7000)
            {
                //(car price * order amount) + tax
                double orderTotal = (order.getUnitPrice() * order.getAmount()) * (1.080);
                //Place confirmation in the buffer
                Driver.confirmBuffer.setConfirm(order.getSenderId(), orderTotal);
            }
        }
    }
}
