using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class confirmationObject
    {
        private string dealer;
        private double orderTotal;
        public confirmationObject(string dealer, double total)
        {
            this.dealer = dealer;
            this.orderTotal = total;
        }
        public string getDealer(){
            return dealer;
        }
        public double getTotal()
        {
            return orderTotal;
        }
        public void setDealer(string dealer)
        {
            this.dealer = dealer;
        }
        public void setTotal(double total)
        {
            this.orderTotal = total;
        }
    }
    public class confirmationBuffer
    {
        private List<confirmationObject> confirmBuffer = new List<confirmationObject>();
        //Find the confirmation for a dealer
        public double getConfirm(string dealer)
        {
            lock (confirmBuffer)
            {
                for (int i = 0; i < confirmBuffer.Count; i++)
                {
                    if (confirmBuffer[i].getDealer() == dealer)
                    {
                        double total = confirmBuffer[i].getTotal();
                        confirmBuffer.RemoveAt(i);
                        return total;
                    }
                }
            }
            //Dealer not found
            return 0;
        }
        //Place a confirmation into the buffer
        public void setConfirm(string dealer, double total)
        {
            lock (confirmBuffer)
                confirmBuffer.Add(new confirmationObject(dealer, total));
        }
    }
}
