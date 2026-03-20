using System;
public delegate void Cal(int a, int b);

class D19_Practice_task_4
{
    public static void Func1()
    {
        Console.Clear();
        Cal cal = add;
        cal += sub;
        cal(1, 2);
    }
    public static void add(int a, int b)
    {
        Console.WriteLine("Add:" + (a + b));
    }
    public static void sub(int a, int b)
    {
        Console.WriteLine("Subrract:" + (a - b));
    }
}