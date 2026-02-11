class D18Practice_task
{
    public static void Func1()
    {
        List<int> ls1 = new List<int>();
        List<int> ls2 = new List<int>();
        List<int> ls3 = new List<int>();

        for(int i=1;i<=100;i++)
        {
            if(i%2==0) ls1.Add(i);
            if(i%3==0) ls2.Add(i);
            if(i%2!=0 && i%3!=0) ls3.Add(i);
        }
        foreach(var i in ls1)
        {
            Console.Write(i+" ");
        }
        Console.WriteLine();
        foreach(var i in ls2)
        {
            Console.Write(i+" ");
        }
        Console.WriteLine();
        foreach(var i in ls3)
        {
            Console.Write(i+" ");
        }
        Console.WriteLine();
    }
}