using System;
class Rev
{
    public static void erse()
    {
        Console.Write("Enter a number:");
        int a = Convert.ToInt32(Console.ReadLine());
        int r=0;
        while(a!=0)
        {
            r=r*10+a%10;
            a=a/10;
        }
        Console.Write(r);
    }
}