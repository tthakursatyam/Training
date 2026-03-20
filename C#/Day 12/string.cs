using System.Text;
class D12_string
{
    public static void func()
    {
        Console.WriteLine(GC.GetTotalMemory(false));
        StringBuilder sb = new StringBuilder();
        sb.Append("Satyam");
        sb.Append(" ");
        sb.Append("Singh");
        string str = sb.ToString();
        Console.WriteLine(str);

        sb.AppendLine("Hello");
        Console.WriteLine(sb.ToString());

        sb.Insert(0,"start");
        Console.WriteLine(sb.ToString());

        sb.Remove(0,5);
        Console.WriteLine(sb.ToString());

        sb.Replace("Singh","Thakur");
        Console.WriteLine(sb.ToString());

        sb.Clear();
        Console.WriteLine(sb.ToString());
        Console.WriteLine(GC.GetTotalMemory(false));
    }
    public static void func2()
    {
        StringBuilder sb1 = new StringBuilder("Hello");
        StringBuilder sb2 = new StringBuilder("Hello");
        Console.WriteLine(sb1.Equals(sb2));
        StringBuilder sb3 = sb2;
        Console.WriteLine(sb3.Equals(sb2));

        Console.WriteLine(object.ReferenceEquals(sb1,sb2));
        Console.WriteLine(sb1==sb2);
        Console.WriteLine(object.ReferenceEquals(sb3,sb2));
        Console.WriteLine(sb2==sb3);
        Console.WriteLine(object.ReferenceEquals(sb1,sb3));
        Console.WriteLine(sb1==sb3);

        string str1="hello";
        string str2="hello";
        Console.WriteLine(str1==str2);
        Console.WriteLine(str1.Equals(str2));
    }
}