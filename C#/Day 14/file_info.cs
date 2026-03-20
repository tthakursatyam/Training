using System;
class Day14_File_info
{
    public static void func1()
    {
        FileInfo file = new FileInfo("sample.txt");
        
        if(!file.Exists)
        {
            using(StreamWriter writer = file.CreateText())
            {
                writer.WriteLine("Hello FileInfo Class");
            }
        }
        Console.WriteLine("File name: "+file.Name);
        Console.WriteLine("File name: "+file.Length+" bytes");
        Console.WriteLine("File name: "+file.CreationTime);

    }
}