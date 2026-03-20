using System;
class ResourceHandler:IDisposable
{
    public ResourceHandler()
    {
        Console.WriteLine("Resource called");
    }
    public void Dispose()
    {
        Console.WriteLine("Dispose method called");
    }
}
class Dispo
{
    public static void ffunc2()
    {
        Console.WriteLine($"Total Memory Before GC: {GC.GetTotalMemory(false)} bytes");

        for (int i = 0; i < 100000; i++)
        {
            object obj = new object(); // Gen 0 allocation
        }

        Console.WriteLine($"Total Memory After Object Creation: {GC.GetTotalMemory(false)} bytes");

        GC.Collect(); 
        GC.WaitForPendingFinalizers();

        Console.WriteLine($"Total Memory After GC: {GC.GetTotalMemory(false)} bytes");
        Console.WriteLine($"Generation of a new object: {GC.GetGeneration(new object())}");
    }
}