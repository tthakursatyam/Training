using System;
class Ou
{
    public void divide(int a,int b,out int q,out int r)
    {
        q=a/b;
        r=a%b;
    }

    public void show(in int number)
    {
        Console.WriteLine(number);
        // number = number + 10;   // Not allowed
    }
    
}