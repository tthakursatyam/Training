using System;
class merge
{
    public void func()
    {
        int[] arr1 = {1, 3, 5};
        int[] arr2 = {2, 4, 6};

        List<int> ls = new List<int>();
        for(int i=0;i<arr1.Length;i++)
        {
            ls.Add(arr1[i]);
        }
        for(int i=0;i<arr2.Length;i++)
        {
            ls.Add(arr2[i]);
        }
        ls.Sort();
        for(int i=0;i<ls.Count();i++)
        {
            Console.WriteLine(ls[i]);
        
        }
    }
}