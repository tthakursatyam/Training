using System.Threading;
class Threading
{
    public static void main()
    {
        Thread thread = new Thread(new ParameterizedThreadStart(PrintMessage));
        thread.Start("hello from thread");
        // Console.WriteLine(DateTime.Now);
        // Thread.Sleep(10000);
        // Console.WriteLine(DateTime.Now);

        Thread worker = new Thread(DoWork);
        worker.Start();
        Console.WriteLine("Hello from main thread");
    }
    static void PrintMessage(object msg)
    {
    Console.WriteLine(msg);
    }
    static void DoWork()
    {
        for(int i=1;i<=5;i++)
        {
            Console.Write(i+" ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}