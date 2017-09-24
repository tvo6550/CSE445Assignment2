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
        private static int p = 0; //total amount of price cuts
        static int orderTotal = 0; //total amount of orders
        private static double price = 250; //default initial price
        private static int amountOfUnits = 100; //default number of units
        private OrderProcessing op = new OrderProcessing();
       
        //Returns amount of price cuts
        public int getP()
        {
            return p;
        }

        public static void changePrice(string plantName, int prevAmount, double prevPrice, double newPrice)
        {
            //if the new price is lower than the current price then the price cut delegate will be called 
            if (newPrice < price)
            {
                //checks to see if the delegate exists
                if (priceCut != null)
                {
                    Console.WriteLine("price cut for " + plantName + " for " + newPrice);
                    priceCut(plantName, prevAmount, prevPrice, newPrice);
                    p++;
                }
            }
            price = newPrice;
        }

        //Pricing Model takes into account the total amount of orders to the plants and whether the day the order was made during a weekday or a weekend
        public void pricingModel(string name)
        {
            //Once 20 price cuts have been achieved the thread will die
            while (p < 20)
            {
                //Console.WriteLine("Thread for" + name);
                Console.WriteLine("p = " + p);
                Console.WriteLine(name + " total amounts of orders: " + orderTotal);
                Thread.Sleep(500);
                string orderDescrip = "";
                orderDescrip = Driver.orderBuffer.getOneCell();
                
                if(orderDescrip != "")
                {
                    OrderClass order = Decoder.decode(orderDescrip);
                    //if the order receiver ID matches the plant then process the order and start a new thread
                    if (name == order.getReceiverId())
                    {
                        amountOfUnits = order.getAmount();
                        Thread orderProc = new Thread(new ThreadStart(() => op.procOrder(order)));
                        orderProc.Start();
                        Driver.orderBuffer.eraseOneCell(Encoder.encode(order));
                        orderTotal++;
                    }
                }
                double value = 0;
                DayOfWeek day = DateTime.Now.DayOfWeek;//check current day of the week
                //If the total order amount for the plant is greater than 3 and it is a weekday, the new car price will be between 251 and 500
                if (orderTotal > 3 && (day >= DayOfWeek.Monday) && (day <= DayOfWeek.Friday))
                {
                    value = rng.Next(251,500);
                }
                //If the total order amount for the plant is less than or equal to 3 and it is a weekday, the new car price will be between 50 and 250
                else if (orderTotal <= 3 && (day >= DayOfWeek.Monday) && (day <= DayOfWeek.Friday))
                {
                    value = rng.Next(50,250);
                }
                //If the total order amount for the plant is greater than 3 and it is a weekend, the new car price will be between 251 and 500
                else if (orderTotal > 3 && (day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
                {
                    value = rng.Next(251,500);
                }
                //If the total order amount for the plant is less than or equal to 3 and it is a weekend, the new car price will be between 50 and 250
                else if (orderTotal <= 3 && (day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday))
                {
                    value = rng.Next(50,250);
                }

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
