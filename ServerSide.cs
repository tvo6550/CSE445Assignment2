using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
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
