using System;
using System.IO;
class InsufficientBalanaceExceptional:Exception
{
    public InsufficientBalanaceExceptional(string message) : base(message)
    {
        Console.WriteLine("An error has occured!");
    }
}

class BankOperationException:Exception
{
    public BankOperationException(string message)  :base(message)
    {
        Console.WriteLine("Some error has occured!");
    }
}
class exc_hand
{
    public static void func()
    {
        int a=10;
        int b=0;
        try
        {
            int result = a/b;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine(DateTime.Now);
        }
    }
    public void func1()
    {
        int a = 10;
        int b = 0;
        try
        {
            Console.WriteLine("Enter the amount: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            //int divisioncheck = a/b;

            string data = File.ReadAllText("Account.txt");
        }
        catch(FormatException ex)
        {
                LogException(ex);
        }
        catch(DivideByZeroException ex)
        {
            LogException(ex);
        }
        catch(FileNotFoundException ex)
        {
            // LogException(ex);
            // Console.WriteLine(ex.StackTrace);
        }
        catch(Exception ex)
        {
            LogException(ex);
        }
        finally
        {
            Console.WriteLine("Exceptinal Handling");
        }

        static void LogException(Exception ex)
        {
            File.AppendAllText("error.log",DateTime.Now+" | "+ex.GetType()+" | "+ex.Message+" | "+ex.StackTrace+Environment.NewLine);
        }
    }
    public void func2()
    {
        FileStream file = null;
        try
        {
            file = new FileStream("data.txt",FileMode.Open);
            int data = file.ReadByte();
        }
        catch(FileNotFoundException ex)
        {
            LogException(ex);
            Console.WriteLine("File not found: "+ex.Message);
        }
        finally
        {
            if(file!=null)
            {
                file.Close();
                Console.WriteLine("File Stream closed in finally block");
            }
        }
        static void LogException(Exception ex)
        {
            File.AppendAllText("error.log",DateTime.Now+" | "+ex.GetType()+" | "+ex.Message+Environment.NewLine);
        }
    }
    public void func3()
    {
        try
        {
            try
            {
                File.ReadAllText("transactions.txt");
            }
            catch (IOException ioEx)
            {
                throw new ApplicationException("Unable to load transaction data",ioEx);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Message: " + ex.Message);
            Console.WriteLine("Root Cause: " + ex.InnerException.Message);
        }
    }
}


// class BankAcc
// {
//     public double balance {get;private set;}=5000;
//     public void Withdraw(double amount)
//     {
//         if(amount<0)
//         {
//             throw new Exception("Withdrawal must be greater than 0");
//         }
//         if(amount > balance)
//         {
//             throw new InsufficientBalanaceExceptional("Bank Balance is insuffient");
//         }
//         balance-=amount;
//     }
// }

public class BankAcco
{
    public decimal Balance { get; private set; }

    public BankAcco(decimal initialBalance)
    {
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative", nameof(initialBalance));

        Balance = initialBalance;
    }

    public void Withdraw(decimal amount)
    {
        // Validate numeric range
        if (amount <= 0)
            throw new ArgumentException(
                "Withdrawal amount must be greater than zero",
                nameof(amount));

        // Enforce business rule
        if (amount > Balance)
            throw new InsufficientBalanaceExceptional($"Cannot withdraw {amount:C}. Available balance: {Balance:C}");

        Balance -= amount;
    }
}