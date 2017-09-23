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
            result = senderId + "-" + cardNo.ToString() + "-" + receiverId + "-" + amount.ToString() + "-" + unitPrice.ToString();
            return result;
        }
    }
    public class Dealer
    {
        private string dealerName;
        private int cardNo;
        private static int count = 1;
        public Dealer()
        {
            this.dealerName = "dealer" + count.ToString();
            count++;
            Random rng = new Random();
            cardNo = rng.Next(5000, 7000);
        }
        public void dealerCheckOrder()
        {
            double orderTotal = 0;
            while (orderTotal == 0)
            {
                Thread.Sleep(1000);
                //Pull order from confirmation buffer
                orderTotal = Driver.confirmBuffer.getConfirm(dealerName);
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
            OrderClass newOrder = new OrderClass(dealerName, cardNo, plantName, curAmt, curPrice);
            Driver.orderBuffer.setOneCell(Encoder.encode(newOrder));
        }
    }
}
