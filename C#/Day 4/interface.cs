interface IPrintable
{
    void Print();
}

interface IScannable
{
    void Scan();
}

class Machine : IPrintable, IScannable
{
    public void Print()
    {
        Console.WriteLine("Printing");
    }

    public void Scan()
    {
        Console.WriteLine("Scanning");
    }
}