using System;
class LoGlo
{
    public void calculator(int a,int b)
    {
        void add()
        {
            Console.WriteLine($"Sum: {a+b}");
        }
        void subs()
        {
            Console.WriteLine($"Sub: {a-b}");
        }
        add();
        subs();
    }
}
class Test
{
    public static int number = 5;

    public static void Calculate()
    {
        static int Square(int x)
        {
            return x * x;
        }
        Console.WriteLine(Square(number));
    }
}