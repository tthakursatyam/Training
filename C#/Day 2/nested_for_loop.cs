using System;
class Multiplication
{
    public static void Example2()
    {
        int start=20;
        int end = 30;

        for(int i=start;i<=end;i++){
            for(int j=1;j<=10;j++){
                Console.WriteLine($"{i}*{j}={i*j}");
            }
            Console.WriteLine();
        }
    }
}