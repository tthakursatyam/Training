using System;
class DL
{
    public static void Dl()
    {
        Console.Write("Enter your age:");
        int age = Convert.ToInt32(Console.ReadLine());

        if (age >= 18)
        {
            Console.Write("Dou you have license  already? ");
            string? haslicense = Console.ReadLine();
            if(haslicense=="Yes")
            {
                Console.Write("You are ready to drive");
            }
            else
            {
                Console.Write("You are required to get DL");
            }
        }
        else
        {
            Console.Write("You are not elgibile for DL");
        }
    }
}