using System;
class Conditions
{
    public static void Condition()
    {
        Console.Write($"Enter your age:");
        int age = Convert.ToInt16(Console.ReadLine());
        if(age>=18) 
        {
            Console.Write("You are elgibile for DL");
        }
        else 
        {
            Console.Write("You are not elgibile for DL");
        }
    }
}