using System;
class Personn
{
    public string Name{get;set;}
    public string Address{get;set;}
    public int Age{get;set;}
}
class PersonImplementation
{
    public static void GetName(List<Personn> person)
    {
        foreach(var i in person)
        {
            Console.Write(i.Name+" "+i.Address+" ");
        }
        Console.WriteLine();
    }

    public static double Average(List<Personn> person)
    {
        int sum=0;
        int t=0;
        foreach(var i in person)
        {
            sum+=i.Age;
            t++;
        }
        return sum*1.0/t;
    }
    public static int Max(List<Personn> person)
    {
        int max=0;
        foreach(var i in person)
        {
            if(max<i.Age) max=i.Age;
        }
        return max;
    }

}
class Day14_task_Program
{
    public static void main()
    {
        List<Personn> p = new List<Personn>();
        p.Add(new Personn{Name="Aarya",Address="A2101",Age=69});
        p.Add(new Personn{Name="Daniel",Address="D104",Age=40});
        p.Add(new Personn{Name="Ira",Address="H801",Age=25});
        p.Add(new Personn{Name="Jennifer",Address="I1704",Age=33});

        PersonImplementation.GetName(p);
        Console.WriteLine(PersonImplementation.Average(p));
        Console.WriteLine(PersonImplementation.Max(p));
    }
}