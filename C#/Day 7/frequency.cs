using System;
class freq
{
    public void func()
    {
        int[] arr = {1, 2, 3, 2, 1, 4, 2};
        Dictionary<int,int> dic = new Dictionary<int,int>();

        for(int i=0;i<arr.Length;i++)
        {
            if (dic.ContainsKey(arr[i]))
            dic[arr[i]]++;  
            else
            dic[arr[i]]=1;
        }
        foreach(var i in dic)
        {
            Console.WriteLine($"Frequency of {i.Key}:{i.Value}");
        }
    }
}