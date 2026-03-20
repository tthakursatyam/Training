using System;
class Finance
{
    public static void Manage()
    {
        Console.WriteLine("Enter 1 for loan eligibilty\nEnter 2 for income tax calculation");
        Console.WriteLine("Enter 3 for transaction\nEnter 4 to exit");
        int start=0; 

        while (start != 4)
        {
            Console.WriteLine("Enter your input:");
            start=Convert.ToInt32(Console.ReadLine());
            switch (start)
            {
                case 1:
                    Console.Write("Enter your age:");
                    int age=Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter your monthly income:");
                    int monthlyincome=Convert.ToInt32(Console.ReadLine());

                    if(age>=21 && monthlyincome >=30000)
                    {
                        Console.WriteLine("You are eligible for loan");
                    }
                    else
                    {
                        Console.WriteLine("You are not eligible for loan");
                    }
                    break;


                case 2:
                    Console.Write("Enter your annual income:");
                    int annualincome=Convert.ToInt32(Console.ReadLine());

                    if(annualincome<=250000)
                    {
                        Console.WriteLine("No Income Tax!");
                    }
                    else if(annualincome>250000 && annualincome<=500000)
                    {
                        int tax=5*annualincome/100;
                        Console.WriteLine($"Your income tax will be {tax}");
                    }
                    else if(annualincome>500000 && annualincome<=1000000)
                    {
                        int tax=20*annualincome/100;
                        Console.WriteLine($"Your income tax will be {tax}");
                    }
                    else
                    {
                        int tax=30*annualincome/100;
                        Console.WriteLine($"Your income tax will be {tax}");
                    }
                    break;

                case 3:
                    Console.Write("Enter your bank balance:");
                    int bankbalance=Convert.ToInt32(Console.ReadLine());

                    int count=1;
                    Console.WriteLine("Enter yes if you want to do transaction:");
                    string? str=Console.ReadLine();
                    while(count<=5 && str=="yes")
                    {
                        Console.WriteLine($"Enter trans {count}:");
                        int trans=Convert.ToInt32(Console.ReadLine());
                        bankbalance=bankbalance+trans;
                        if(bankbalance<0)
                        {
                            Console.WriteLine("Your last transaction in invalid!");
                            bankbalance=bankbalance-trans;
                        }
                        Console.WriteLine($"Your updated bank balance:{bankbalance}");
                        if(count!=5)
                        {
                            Console.WriteLine("Enter yes if you want to do transaction:");
                        }
                        str=Console.ReadLine();
                        count++;
                    }
                    break;
            }
        }
    }
}