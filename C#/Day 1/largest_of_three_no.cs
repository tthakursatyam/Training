using System;
class Large
{
    public static void Largest()
    {
        Console.WriteLine("Enter three numbers:");
        int a = Convert.ToInt32(Console.ReadLine());
        int b = Convert.ToInt32(Console.ReadLine());
        int c = Convert.ToInt32(Console.ReadLine());

        if(a>b && a>c)
        {
            Console.WriteLine($"{a} is the largest number");
        }
        else if (b>c)
        {
            Console.WriteLine($"{b} is the largest number");
        } 
        else
        {
            Console.WriteLine($"{c} is the largest number");
        }
    }
}