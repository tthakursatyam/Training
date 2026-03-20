using System;
class Bank
{
    public static void Credit_Debit()
    {
        Console.WriteLine("1.Debit");
        Console.WriteLine("2.Credit");
        Console.WriteLine("3.Exit");

        int n=0;
        while (n != 3)
        {
            Console.Write("Enter your input for main menu:");
            n=Convert.ToInt32(Console.ReadLine());

            switch(n)
            {
                case 1:
                    Console.WriteLine("1.ATM Withdrawal Limit Validation");
                    Console.WriteLine("2.EMI Burden Evaluation");
                    Console.WriteLine("3.Transaction-Based Daily Spending Calculator");
                    Console.WriteLine("4.Minimum Balance Compliance Check");
                    Console.WriteLine("5.exit");
                    int start=0;
                    while(start!=5)
                    {
                        Console.Write("Enter your input for Debit:");
                        start=Convert.ToInt32(Console.ReadLine());
                        switch (start)
                        {
                            case 1:
                                Console.Write("Enter withdrawal amount:");
                                int amount=Convert.ToInt32(Console.ReadLine());
                                
                                if(amount<=40000)
                                {
                                    Console.WriteLine("Withdrawal is allowed.");
                                }
                                else
                                {
                                    Console.WriteLine("Withdrawal is rejected.");
                                }
                            break;

                            case 2:
                                Console.Write("Enter your monthly income:");
                                int monthlyincome=Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter your EMI amount:");
                                int EMI=Convert.ToInt32(Console.ReadLine());

                                if(monthlyincome*0.4<EMI)
                                {
                                    Console.WriteLine("EMI exceeds safe income limit.");
                                }
                                else
                                {
                                    Console.WriteLine("EMI is financially manageable.");
                                }
                            break;

                            case 3:
                                Console.Write("Enter your total no of transaction:");
                                int numbertrans=Convert.ToInt32(Console.ReadLine());

                                int total=0;
                                for(int i = 1; i <= numbertrans; i++)
                                {
                                    Console.Write($"Enter trans no.{i}:");
                                    int trans=Convert.ToInt32(Console.ReadLine());
                                    if(trans<0)
                                    {
                                        Console.Write("Invalid Transaction");
                                        continue;
                                    }
                                    total+=trans;
                                }
                                Console.WriteLine($"Total debit amount for the day:{total}");
                            break;

                            case 4:
                                Console.Write("Enter your currrent bank balance:");
                                int bankbalance=Convert.ToInt32(Console.ReadLine());

                                if (bankbalance < 2000)
                                {
                                    Console.WriteLine("Minimum balance not maintained. Penalty applicable.");
                                }
                                else
                                {
                                    Console.WriteLine("Minimum balance requirement satisfied.");
                                }
                            break;
                        }
                    }
                break;
            
                case 2:
                    Console.WriteLine("1.Net Salary Credit Calculation");
                    Console.WriteLine("2.Fixed Deposit Maturity Calculation");
                    Console.WriteLine("3.Credit Card Reward Points Evaluation");
                    Console.WriteLine("4.Employee Bonus Eligibility Check");
                    Console.WriteLine("5.Exit");

                    int start1=0;

                    while(start1!=5)
                    {
                        Console.Write("Enter your input for Credit:");
                        start1=Convert.ToInt32(Console.ReadLine());

                        switch (start1)
                        {
                            case 1:
                                Console.Write("Enter your gross salary:");
                                int gsalary=Convert.ToInt32(Console.ReadLine());
                                double nsalary=gsalary*0.9;
                                Console.WriteLine($"Net salary credited:{nsalary}");

                            break;

                            case 2:
                                Console.Write("Enter principle:");
                                int principle=Convert.ToInt32(Console.ReadLine());
                                
                                Console.Write("Enter ROI:");
                                int roi=Convert.ToInt32(Console.ReadLine());

                                Console.Write("Enter time:");
                                int time=Convert.ToInt32(Console.ReadLine());

                                double finalamount=principle+(principle*roi*time)/100;
                                Console.WriteLine($"Fixed Deposit maturity amount:{finalamount}");

                            break;

                            case 3:
                                Console.Write("Enter your credit card spendings:");
                                int spends=Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine($"Reward points earned:{spends*0.01}");

                            break;

                            case 4:
                                Console.Write("Enter your annual salary:");
                                int salary = Convert.ToInt32(Console.ReadLine());

                                Console.Write("Enter your year of service:");
                                int serviceyear=Convert.ToInt32(Console.ReadLine());

                                if(salary>=500000 && serviceyear >= 3)
                                {
                                    Console.WriteLine("Employee is eligible for bonus.");
                                }
                                else
                                {
                                    Console.WriteLine("Employee is not eligible for bonus.");
                                }

                            break;
                        }
                    }
                break;
            }
        }
    }
}