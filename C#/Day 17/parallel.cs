using System.Threading.Tasks;
class parallel
{
    public static void func1()
    {
        // Parallel.For(0,5,i =>
        // {
        //     Console.WriteLine($"Processing: item {i}");
        // });
        Console.WriteLine($"Started: "+DateTime.Now);
        int[] number = new int[1_000_000];
        for(int i=0;i<number.Length;i++)
        {
            number[i]=i+1;
        }
        long sum=0;
        Parallel.For(0,number.Length,i=>
        {
            sum+=number[i];
            //Thread.Sleep(100);
        });
        Console.WriteLine(sum);
        Console.WriteLine($"Ended: "+DateTime.Now);
        sum=0;
        
        Parallel.For(0,number.Length,()=>0,(i,loopState,localSum) =>
        {
            return localSum+number[i];
        },
        localSum=>
        {
            Interlocked.Add(ref sum,localSum);
        });
        Console.WriteLine("Sum: "+sum);
    }
    async Task<int> GetDataAsync()
    {
        await Task.Delay(1000);
        return 42;
    }
    static async Task func2()
    {
        Console.WriteLine("Start");
        await Task.Delay(2000);
        Console.WriteLine("End");
    }
}