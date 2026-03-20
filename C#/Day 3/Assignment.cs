using System;
class BankAccountt
{
    private int AccountNo;
    private double Balance;
    public static string BankName="State Bank Of India";

    public BankAccountt(int Ac,int Bal)
    {
        AccountNo=Ac;
        Balance=Bal;
    }
    
    public void Deposit(double amount)
    {
        if(amount>0)
        {
            Balance +=amount;
        }
        else
        {
            Console.WriteLine("Invalid Deposit Amount");
        }
    }
    public void Withdraw(double amount)
    {
        if(amount>Balance) 
        {
            Console.WriteLine("Insuffiecent Balance!");
        }
        else 
        {
            Balance-=amount;
            Console.WriteLine("Withdrawal Successful");
        }
    }
    public void DisplayDetails()
    {
        Console.WriteLine($"Your Bank Name:{BankName}");
        Console.WriteLine($"Your Account Number:{AccountNo}");
        Console.WriteLine($"Your Current balance:{Balance}");
    }
    public void getBalance(){
        Console.WriteLine($"Your Current balance:{Balance}");
    }

}