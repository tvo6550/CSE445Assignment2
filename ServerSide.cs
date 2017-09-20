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
    public class Decoder
    {
        public OrderClass decode(string order)
        {
            string[] tokens = order.Split('-');
            OrderClass orderObj = new OrderClass(tokens[0], Int32.Parse(tokens[1]), tokens[2], Int32.Parse(tokens[3]), Int32.Parse(tokens[4]));
            return orderObj;
        }
    }
}
