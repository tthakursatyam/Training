using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
class Day14_Directory
{
    public static void func1()
    {

        if(!Directory.Exists("Logs"))
        {
            Directory.CreateDirectory("Logs");
            Console.WriteLine("Directory created succesfull");
        }

        DirectoryInfo dir = new DirectoryInfo("Logs");
        Console.WriteLine("Name: "+dir.Name);
        Console.WriteLine("Created on: "+dir.CreationTime);
        Console.WriteLine("Full path: "+dir.FullName);
    }
    public static void func2()
    {
        Userr user = new Userr
        {
            ID=123,
            content="This is serialization"
        };

        string json = JsonSerializer.Serialize(user);

        File.WriteAllText("user.json",json);
        Console.WriteLine("\t===Created===\t");


        Userr userr = JsonSerializer.Deserialize<Userr>(json);
        Console.WriteLine($"\t===Id: {userr.ID}===\n\t===Content: {userr.content}===\t");

        XmlSerializer serializer = new XmlSerializer(typeof(Userr));
        using(FileStream fs = new FileStream(("user.xml"),FileMode.Create))
        {
            serializer.Serialize(fs,user);
        }
        Console.WriteLine("XML Serialized");
    }


}
public class Userr
{
    public int ID{get; set;}
    public string content{get; set;}
}