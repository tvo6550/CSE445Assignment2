using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class ClientSide
    {
    }
    public class Encoder
    {
        public string encode(OrderClass order)
        {
            string result = "";
            string senderId = order.getSenderId();
            int cardNo = order.getCardNo();
            string receiverId = order.getReceiverId();
            int amount = order.getAmount();
            int unitPrice = order.getUnitPrice();
            result = senderId + "-" + cardNo.ToString() + "-" + receiverId + "-" + amount.ToString() + "-" + unitPrice.ToString();
            return result;
        }
    }
}
