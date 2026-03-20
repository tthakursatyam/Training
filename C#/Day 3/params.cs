using System;
class Para
{
    
    public void ms(int b ,string n="Satyam Singh",params int[]arr)
    {
        int sum=0;
        for(int i=0;i<arr.Length;i++){
            sum+=arr[i];
        }
        sum=sum;
        Console.WriteLine(n);
        Console.WriteLine($"Sum: {sum}");
    }
    
}