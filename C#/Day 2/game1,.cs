using System;
class Game
{
    public static void Example1()
    {
        for(int i=1;i<=10;i++)
        {
            if(i!=4)
            {
                Console.WriteLine($"Player killed enemy {i}");
            }
            else
            {
                Console.WriteLine($"enemy 4 is invisible Skipping");
            }
        }
    }
}