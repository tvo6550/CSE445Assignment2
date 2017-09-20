using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

/// <summary>
/// Summary description for Class1
/// </summary>
public class OrderClass
{
    private string senderId;
    private int cardNo;
    private string receiverId;
    private int amount;
    private int unitPrice;

	public OrderClass()
	{
        senderId = "";
        cardNo = -1;
        receiverId = "";
        amount = 1;
        unitPrice = -1;
	}
    public OrderClass(string senderId, int cardNo, string receiverId, int amount, int unitPrice)
    {
        this.senderId = senderId;
        this.cardNo = cardNo;
        this.receiverId = receiverId;
        this.amount = amount;
        this.unitPrice = unitPrice;
    }
    public void setSenderId(string senderId){
        this.senderId = senderId;
    }
    public string getSenderId(){
        return this.senderId;
    }
    public void setCardNo(int cardNo){
        this.cardNo = cardNo;
    }
    public int getCardNo(){
        return this.cardNo;
    }
    public void setReceiverId(string receiverId){
        this.receiverId = receiverId;
    }
    public string getReceiverId(){
        return this.receiverId;
    }
    public void setAmount(int amount){
        this.amount = amount;
    }
    public int getAmount(){
        return this.amount;
    }
    public void setUnitPrice(int unitPrice){
        this.unitPrice = unitPrice;
    }
    public int getUnitPrice(){
        return this.unitPrice;
    }
}
