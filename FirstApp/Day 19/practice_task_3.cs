using System;
abstract class Features
{
    abstract public void First_Gear();
    abstract public void Second_Gear();
    public virtual void Reverse_Camera()
    {
        Console.WriteLine("Reverse Camera");
    }
    public virtual void Audio_System()
    {
        Console.WriteLine("Audio System");

    }
}
class Car_3 : Features
{
    public override void First_Gear()
    {
        Console.WriteLine("First Gear");
    }
    public override void Second_Gear()
    {
        Console.WriteLine("Second Gear");
    }
}
class D19_Practice_task_3
{
    public static void Func1()
    {
        Console.Clear();
        Car_3 car = new Car_3();
        car.First_Gear();
        car.Second_Gear();
        car.Audio_System();
        car.Reverse_Camera();


    }
}