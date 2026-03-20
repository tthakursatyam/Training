using System;
class BankAccount
{
    public long Accno=0;
    public int Balance=0;

    public void Deposit(int amount)
    {
        Balance+=amount;
    }
    public void GetBalance()
    {
        Console.WriteLine($"Your current balance: {Balance}");
    }
}

class Employee
{        
    public string? name="";
    public int salary=0;
    public void DisplayDetails()
    {
        Console.WriteLine($"Name:{name}\nSalary:{salary}");

    }
    
}

class Wallet
{
    private double money;
    public void AddMoney(double amt)
    {
        money+=amt;
    }
    public double getBalance()
    {
        return money;
    }
    public void setmoney(double m)
    {
        money=m;
    }
}