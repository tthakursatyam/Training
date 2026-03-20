using System;
class Generic<T>
{
    public T data;

    public T GetValue()
    {
        return data;
    }
    public void SetValue(T d)
    {
        data=d;
    }
}
class Generic2<T> where T: class
{
    public T data;
}
class animal
{
    public string name;
    
}
class programm
{
    public static void Mainn()
    {
        Generic2<animal> gn2 = new Generic2<animal>();
        gn2.data=new animal {name="Dog"};
        Console.WriteLine("Name of animal: "+gn2.data.name);

    }
}
class Calculate
{
    public T add<T>(T a,T b)
    {
        return a;
    }
    public T subs<T>(T a,T b)
    {
        return b;
    }   
    public T PrintData<T>(T x)
    {
        return x;
    } 
}