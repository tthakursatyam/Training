using System;
class multid_array
{
    public void func()
    {
        int[,] matrix1 = new int [2,3];
        int[,] matrix2 = { {1,2,3},
                            {2,3,4},
                            {3,4,5}};
        for(int i=0;i<matrix2.GetLength(0);i++)
        {
            for(int j=0;j<matrix2.GetLength(1);j++)
            {
                Console.Write(matrix2[i,j]+" ");
            }
            Console.WriteLine();
        }
    }
}