using System;
class Sum
{
    public static void OfDigits()
    {
        Console.Write("Enter a digit:");
        int a=Convert.ToInt32(Console.ReadLine());
        int sum=0;
        while (a != 0)
        {
            sum=a%10+sum;
            a=a/10;
        }
        Console.WriteLine(sum);
    }
}