using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment2
{
    class ClientSide
    {
    }
    public static class Encoder
    {
        public static string encode(OrderClass order)
        {
            string result = "";
            string senderId = order.getSenderId();
            int cardNo = order.getCardNo();
            string receiverId = order.getReceiverId();
            int amount = order.getAmount();
            double unitPrice = order.getUnitPrice();
            result = senderId + "#" + cardNo.ToString() + "#" + receiverId + "#" + amount.ToString() + "#" + unitPrice.ToString();
            return result;
        }
    }
    public class Dealer
    {
        private string dealerName;
        private int cardNo;
        private static int count = 1;
        Random rng = new Random();
        public Dealer()
        {
            this.dealerName = "dealer" + count.ToString();
            count++;
            cardNo = rng.Next(5000, 7000);
        }
        public void dealerCheckOrder()
        {
            for(int i = 0; i < 20; i++)
            {
                double orderTotal = 0;
                Thread.Sleep(1000);
                while (orderTotal == 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine(Thread.CurrentThread.Name + " is waiting");
                    //Pull order from confirmation buffer
                    orderTotal = Driver.confirmBuffer.getConfirm(Thread.CurrentThread.Name);
                }
                Console.WriteLine("Order confirmed for " + Thread.CurrentThread.Name + " at a price of " + orderTotal + " is completed.");
            }
        }
        public string getDealerName()
        {
            return this.dealerName;
        }
        public int getCardNo()
        {
            return this.cardNo;
        }
        public void setDealerName(string name){
            this.dealerName = name;
        }
        public void setCardNo(int card)
        {
            this.cardNo = card;
        }
        public void saleOnCars(string plantName, int prevAmt, double prevPrice, double curPrice)
        {
            //Figure out how many cars to buy
            int curAmt = (int) ((prevAmt * prevPrice) / curPrice);
            int dealerNum = rng.Next(1, 6);
            OrderClass newOrder = new OrderClass("Dealer " + (dealerNum).ToString(), cardNo, plantName, curAmt, curPrice);
            //Place order into the buffer
            Driver.orderBuffer.setOneCell(Encoder.encode(newOrder));
        }
    }
}
