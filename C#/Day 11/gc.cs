using System;
class garbage
{
    public static void func1()
    {
        Console.WriteLine("Creatig objects");
        List<object> ls =new List<object>();
        for(int i=0;i<5;i++)
        {
            Myclass obj = new Myclass();
            ls.Add(obj);
        }
        Console.WriteLine("Forcing GC");
        ls.Clear();
        GC.Collect();
        GC.WaitForPendingFinalizers();

        Console.WriteLine("Garbage collection completed");
    }
}
class Myclass
{
    ~Myclass()
    {
        Console.WriteLine("Finalizer called");
    }
}