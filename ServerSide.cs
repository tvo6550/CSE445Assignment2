using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public delegate void priceCutEvent(double prevPrice, double curPrice);
    class ServerSide
    {
    }
    public static class Decoder
    {
        public static OrderClass decode(string order)
        {
            string[] tokens = order.Split('-');
            OrderClass orderObj = new OrderClass(tokens[0], Int32.Parse(tokens[1]), tokens[2], Int32.Parse(tokens[3]), Int32.Parse(tokens[4]));
            return orderObj;
        }
    }
    public class Plant
    {
        static Random rng = new Random();
        private int stockPrice = rng.Next(5, 10);
        private static event priceCutEvent priceCut;
        private static int p = 0;

        public int getP()
        {
            return p;
        }

        public static void PricingModel(OrderClass order, int stockPrice, int orderAmount)
        {
            if(order.getUnitPrice() == 0)
            {
                order.setUnitPrice(rng.Next(50, 500));
            }
            double prevPrice, curPrice = 0;
            if (stockPrice > 7 && orderAmount > 5)
            {
                order.setUnitPrice(order.getUnitPrice() + 1);
            }
            else if (stockPrice <= 7 && orderAmount <= 5)
            {
                prevPrice = order.getUnitPrice();
                curPrice = order.getUnitPrice() - 1;
                order.setUnitPrice(order.getUnitPrice() - 1);
                priceCut(prevPrice, curPrice);
                p += 1;
            }
        }

        public void plantFunc()
        {
            OrderClass order = Decoder.decode(Driver.orderBuffer.getOneCell());
            PricingModel(order, this.stockPrice, order.getAmount());
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
