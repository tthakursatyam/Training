using System;
interface IGear
{
    void First_Gear();
    void Second_Gear();
    void Third_Gear();
    void Fourth_Gear();
    void Fifth_Gear();
    void Sixth_Gear();
}
class Carr : IGear
{
    public void First_Gear()
    {
        Console.WriteLine("First Gear Tested");
    }
    public void Second_Gear()
    {
        Console.WriteLine("Second Gear Tested");
    }
    public void Third_Gear()
    {
        Console.WriteLine("Third Gear Tested");
    }
    public void Fourth_Gear()
    {
        Console.WriteLine("Fourth Gear Tested");
    }
    public void Fifth_Gear()
    {
        Console.WriteLine("Fifth Gear Tested");
    }
    public void Sixth_Gear()
    {
        Console.WriteLine("Six Gear Tested");
    }
}
class D19_Practice_task_2
{
    public static void Func1()
    {
        Carr car = new Carr();
        car.First_Gear();
        car.Second_Gear();
        car.Third_Gear();
        car.Fourth_Gear();
        car.Fifth_Gear();
        car.Sixth_Gear();
    }
}