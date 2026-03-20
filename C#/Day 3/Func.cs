using System;
class Example
{
    public static void ex()
    {
        int Square(int x)
        {
            return x * x;
        }
        Func<int,int> sqlambda = x => x*x;

        Console.WriteLine(Square(4));
        Console.WriteLine(sqlambda(2));
    }
}