using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Assignment2
{
    class MultiCellBuffer
    {
        private static Semaphore cellsAvailable = new Semaphore(0,3);
        private static string[] orders = new string[3];

        public MultiCellBuffer()
        {
            cellsAvailable.Release(3);
        }
        //Put an order into the buffer
        public void setOneCell(string order)
        {
            cellsAvailable.WaitOne();
            lock (orders)
            {
                for (int i = 0; i < orders.Length; i++)
                {
                    if (orders[i] == null)
                    {
                        orders[i] = order;
                        break;
                    }
                }
            }
        }
        //Get the oldest order
        public string getOneCell()
        {
            string order = "";
            lock (orders)
            {
                for(int i = 0; i < orders.Length; i++)
                {
                    if (orders[i] != null)
                    {
                        order = orders[i];
                        return order;
                    }
                }
            }
            return order;
        }
        public void eraseOneCell(string order)
        {
            for(int i = 0; i < orders.Length; i++)
            {
                if(orders[i] == order)
                {
                    cellsAvailable.Release();
                    orders[i] = null;
                    break;
                }
            }
        }
    }
}
