using System;
using System.Diagnostics;
using System.Threading;
class Process_1
{
    public static void func1()
    {
        Process process = Process.GetCurrentProcess();
        Console.WriteLine("Current Process ID: "+process.Id);
        Console.WriteLine("Process Name: "+process.ProcessName);
        Console.WriteLine("Process Time: "+process.StartTime);
        Console.WriteLine("Process Thread: "+process.Threads);
        Console.WriteLine("Process WorkingSet: "+process.WorkingSet64);
        Console.WriteLine("Process Total Time: "+process.TotalProcessorTime);
    }
}


class Process_2
{
    public static void func2()
    {
        // Create a new thread
        Thread worker = new Thread(DoWork);

        // Start the thread
        worker.Start();

        Console.WriteLine("Main thread continues...");

        // Optional: Wait for worker thread to finish
        worker.Join();
        Console.WriteLine("Main thread finished");
    }

    static void DoWork()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine("Worker thread: " + i);
            Thread.Sleep(500); // Simulate work
        }
    }
}
class Process_3
{
    static int counter=0;
    public static void func3()
    {
        // Process.Start("C:\\Riot Games\\Riot Client\\RiotClientServices.exe");
        Thread t1 = new Thread(Increment);
        Thread t2 = new Thread(Increment);
        Console.WriteLine(counter);
        t1.Start();
        Console.WriteLine(counter);
        t2.Start();
        Console.WriteLine(counter);
        t1.Join();
        Console.WriteLine(counter);
        t2.Join();
        Console.WriteLine(counter);
    }
    static void Increment()
    {
        for(int i=0;i<1_00_000;i++)
        {
            counter++;
        }
    }
}
class Process_4
{
    static int counter=0;
    static object lockObj = new object();
    public static void func4()
    {
        // Process.Start("C:\\Riot Games\\Riot Client\\RiotClientServices.exe");
        Thread t1 = new Thread(Increment);
        Thread t2 = new Thread(Increment);
        Console.WriteLine(counter);
        t1.Start();
        Console.WriteLine(counter);
        t2.Start();
        Console.WriteLine(counter);
        t1.Join();              //Main thread will wait for t1 to finish executation
        Console.WriteLine(counter);
        t2.Join();
        Console.WriteLine(counter);
    }
    static void Increment()
    {
        for(int i=0;i<1_00_000;i++)
        {
            lock(lockObj)
            {
                counter++;
            }
        }
    }
}