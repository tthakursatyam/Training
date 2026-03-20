using System;
class Do
{
    public static void Whle()
    {
        int count=1;
        do
        {
            Console.WriteLine($"Current Count:{count}");
            count++;
        }
        while(count<=5);
    }
}