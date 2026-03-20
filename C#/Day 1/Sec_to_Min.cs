using System;
class Con
{
    public static void StoM()
    {
        Console.Write("Enter the time in Seconds:");
        int s = Convert.ToInt32(Console.ReadLine());
        int min=s/60;
        Console.Write($"Time in Mintues:{min}");
    }
}