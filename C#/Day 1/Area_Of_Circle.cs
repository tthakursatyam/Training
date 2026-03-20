using System;
    class AOC
{
    public static void Area()
    {
        Console.Write("Enter the radius:");
        double radius = Convert.ToDouble(Console.ReadLine());
        double area = Math.PI*radius*radius;
        Console.Write($"Area:{area}");
        
    }
}