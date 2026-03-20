class D18_task
{
    public static void func()
    {
        try
        {
        Task t = Task.Run(() => throw new Exception("Task error"));
        t.Wait();
        }
        catch (AggregateException ex)
        {
        Console.WriteLine(ex.InnerExceptions[0].Message);
        }
        Task t1 = Task.Run( () => Console.WriteLine("Task 1"));
        Task t2 = Task.Run( () => Console.WriteLine("Task 2"));

        Task.WhenAll(t1,t2).ContinueWith(t => Console.WriteLine("All task Completed"));

        // Task<int> t = Task.Run(() => 42);
        // t.ContinueWith(resulttask => )
    }
}