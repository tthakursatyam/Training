using System;
using System.IO;
class Day14_file
{
    public static void func1()
    {
        string path = "data.txt";
        File.WriteAllText(path,"Example of File class");
        File.WriteAllText(path,"2nd Example of File class");

        string read = File.ReadAllText("data2.txt");

        Console.WriteLine(read);
    }
    public static void func2()
    {
        using(StreamReader reader = new StreamReader("log.txt"))
        {
            string line;
            while( (line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
        User user = new User
        {
            ID=123,
            content="This file is created by StreamWriter"
        };
        using(StreamWriter writer = new StreamWriter("user.txt"))
        {
            writer.WriteLine(user.ID);
            writer.WriteLine(user.content);
            user.ID=321;
            user.content="This is an attempt to rewrite the file data";
            writer.WriteLine(user.ID);
            writer.WriteLine(user.content);
        }
    }
    public static void func3()
    {
        User user = new User();
        using(StreamReader reader = new StreamReader("data2.txt"))
        {
            user.ID=int.Parse(reader.ReadLine());
            user.content=reader.ReadLine();

            Console.WriteLine(user.ID);
            Console.WriteLine(user.content);

            user.ID=int.Parse(reader.ReadLine());
            user.content=reader.ReadLine();

            Console.WriteLine(user.ID);
            Console.WriteLine(user.content);

        }
        
    }
    public static void func4()
    {
        User user = new User
        {
            ID=2,content="Binary"
        };
        using(BinaryWriter writer = new BinaryWriter(File.Open("user.bin",FileMode.Create)))
        {
            writer.Write(user.ID);
            writer.Write(user.content);
        }
        using(BinaryReader reader = new BinaryReader(File.Open("user.bin",FileMode.Open)))
        {
            Console.WriteLine(reader.ReadInt32());
            Console.WriteLine(reader.ReadString());
        }
    }
}
class User
{
    public int ID;
    public string content;
}