using System;
class Constr
{
    int x=0;
    public Constr(int n)
    {
        x=n;
    }
}
class constr2
{
    private constr2()
    {
        
    }
    public void display()
    {
        Console.WriteLine("Hello");
    }
}
class Product
{
    public string Name;
    public int Price;

    public Product() { }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }
}