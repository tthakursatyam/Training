using System;
namespace BankingSystem
{
    class InsufficientBalanceException:Exception
    {
        public InsufficientBalanceException(string message) : base(message){}
    }
    class BankOperationException:Exception
    {
        public BankOperationException(string message,Exception ex) : base (message,ex){}
    }
    class BankAccount
    {
        public string AccountNumber{get; private set;}
        public double balance {get;private set;}
        public BankAccount(string AcNo,int b)
        {
            if(b<0)
            {
                throw new InsufficientBalanceException("Balance cannot be negative");
            }
            if(string.IsNullOrWhiteSpace(AcNo))
            {
                throw new ArgumentException("Account Number is invalid");
            }
            AccountNumber=AcNo;
            balance=b;
        }
        public void withdraw(double amt)
        {
            try
            {
                if(amt<=0)
                {
                    throw new ArgumentException("Withdrawal Amt cannot be negative");
                }
                if(amt>balance)
                {
                    throw new InsufficientBalanceException("Insufficient Bank Balance");
                }
                balance-=amt;
                Console.WriteLine("Withdrawal Succesfull");
            }
            catch(InsufficientBalanceException ex)
            {
                LogException(ex);
                throw;
            }
            catch(Exception ex)
            {
                throw new BankOperationException("Unexpected error during withdrawal", ex);
                LogException(ex);
            }
            static void LogException(Exception ex)
            {
                File.AppendAllText("error.log",DateTime.Now+" | "+ex.GetType()+" | "+ex.Message+Environment.NewLine);
            }
        }
    }
}