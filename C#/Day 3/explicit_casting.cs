using System;
class ex
{
    public static void eg(double a){
        
        Console.WriteLine($"Double:{a}");

        int b=Convert.ToInt32(a);
        Console.WriteLine($"Integer:{b}");
        
        double c = (double) b;
        c+=0.1;
        Console.WriteLine($"double:{c}");

        Console.WriteLine($"double:{b*0.1}");
        
    }
}