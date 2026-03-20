using System;
class Engine
{
    public void Start()
    {
        Console.WriteLine("Engine started");
    }
}

class Car
{
    Engine engine = new Engine();

    public void Drive()
    {
        engine.Start();
        Console.WriteLine("Car is driving");
    }
}