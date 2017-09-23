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
        private static Semaphore cellsAvailable = new Semaphore(3,3);
        private static string[] orders = new string[3];
        //Position to keep track of order at head
        private static int position = 0;

        //Put an order into the buffer
        public void setOneCell(string order)
        {
            cellsAvailable.WaitOne();
            lock (orders)
            {
                bool placed = false;

                for (int i = 0; i < orders.Length && !placed; i++)
                {
                    if (orders[i] == null)
                    {
                        orders[i] = order;
                        placed = true;
                    }
                }
            }
        }
        //Get the oldest order
        public string getOneCell()
        {
            string order = "";
            bool empty = true;
            //Check if empty
            for (int i = 0; i < orders.Length; i++){
                if (orders[i] != null)
                    empty = false;
            }
            if (!empty)
            {
                while (orders[position] == null)
                    position = (position + 1) % orders.Length;
                //Get the order and change the cell to null
                order = orders[position];
                orders[position] = null;
                position = (position + 1) % orders.Length;
                cellsAvailable.Release();
            }
            return order;
        }

    }
}
