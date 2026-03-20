class arr11
{
    public void eg()
    {
        int[] numbers1;
        int[] numbers2 = new int[9];
        int[] numbers3 = { 10, 20, 30, 40, 50 };

        int[] marks = { 85, 90, 78 };

        for (int i = 0; i < marks.Length; i++)
        {
            Console.WriteLine($" marks of {i + 1} student is {marks[i]}");
        }



        Console.WriteLine("Now using foreach");
        foreach (int i in marks)
        {
            Console.WriteLine($" marks of student is {i}");
        }



        //Calculate total marks and Average








        // Multi Dimension Arrays
        //declaration
        int[,] matric = new int[2, 3];
        // 2-number of rows
        // 3- number of col

        int[,] matrix2 =
        {
            {1,2,3},
            {4,5,6}
        };

        //print number 4
        Console.WriteLine($"Number 4 in the matric is at 1,1 ::>> {matrix2[1, 0]}");



        //print all numbers
        for (int i = 0; i < matrix2.GetLength(0); i++)
        {
            for (int j = 0; j < matrix2.GetLength(1); j++)
            {
                Console.Write(matrix2[i, j] + "  ");
            }
            Console.WriteLine("");
        }


        //jagged arrays
        Console.WriteLine("\nJagged Arrays");
        int[][] jagged = new int[2][];
        jagged[0] = new int[] { 1, 2, 3 };
        jagged[1] = new int[] { 5, 6, 7};

        //print all numbers
        for (int i = 0; i < jagged.Length; i++)
        {
            for (int j = 0; j < jagged[i].Length; j++)
            {
                Console.Write(jagged[i][j] + "  ");
            }
            Console.WriteLine("");
        }
        
        //print all numbers this will not work
        // for (int i = 0; i < jagged.GetLength(0); i++)
        // {
        //     for (int j = 0; j < jagged.GetLength(1); j++)
        //     {
        //         Console.Write(jagged[i][j] + "  ");
        //     }
        //     Console.WriteLine("");
        // }


        //Arrays Methods
        int[] arr = {24,56,32,89,2,6,4,1};
        Console.WriteLine(string.Join(" ",arr));

        arr.Sort();
        Console.WriteLine(string.Join(" ",arr));

        Array.Clear(arr,1,2);
        Console.WriteLine(string.Join(" ",arr));
        Array.Clear(arr,1,arr.Length-1);
        Console.WriteLine(string.Join(" ",arr));


        arr = new int[]{1,2,53,86,43,86,34,77,32};
        int[] arr2 = new int[arr.Length];
        Array.Copy(arr,arr2,arr.Length-1);
        Console.WriteLine(string.Join(" ",arr2));

    }
}