using System;
class Conversion
{
    public static void FtoCM()
    {
        Console.Write("Enter length in feet:");
        double feet=Convert.ToDouble(Console.ReadLine());
        double CM=feet*30.48;
        Console.Write($"Length in CM:{CM}");
    }
}