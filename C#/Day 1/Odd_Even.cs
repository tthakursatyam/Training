using System;
class OE
{
    public static void OddEven()
    {
        Console.Write("Enter a number:");
        int x=Convert.ToInt16(Console.ReadLine());

        if (x % 2 == 0)
        {
            Console.Write("Given number is an Even");
        }
        else
        {
            Console.Write("Given number is a Odd");
        }
    }
}