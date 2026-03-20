using System;
class tuple
{
    
    static (int sum, int avg) Calculate(int a, int b)
    {
        return (a + b, (a + b) / 2);
    }
    static (bool isValid,string msg) ValidateUser(string username)
    {
        if(string.IsNullOrEmpty(username))
        {
            return (false,"Username is required");
        }
        return (true,"Valid User");
    }
    static (int min,int max) small_large(int[] number)
    {
        return (number.Min(),number.Max());
    }
    
    public static void func1()
    {
        
        (int,string) student1 = (100,"Satyam");
        var student2 = (id:101,Name:"Amit");
        var student3 = new
        {
                id=102,Name="Sandeep"
        };
        Console.WriteLine(student1.GetType());
        Console.WriteLine(student2.GetType());
        Console.WriteLine(student3.GetType());

        var result1 = Calculate(10,20);
        var result2 = ValidateUser("Satyam");

        Console.Write(result1.avg+" ");
        Console.WriteLine(result1.sum);
        Console.Write(result2.isValid+" ");
        Console.WriteLine(result2.msg);

    }
    
    public static void func2()
    {
                //Deconstruction
        var person = (Id: 1 , Name:"Satyam");
        Console.WriteLine(person);

        var (_,username) = person;

        var(id,name) = person;
        Console.WriteLine(id);
        Console.WriteLine(id.GetType());
        Console.WriteLine(person.GetType());

        Console.WriteLine(username);
        Console.WriteLine(username.GetType());


    }
    public static void func3()
    {
        var s = new tuple2 { Id = 1, Name = "Amit" };
        
        var (sid, sname) = s;

        Console.WriteLine(sid);
        Console.WriteLine(sname);
        Console.WriteLine(s.GetType());
    }
}
class tuple2
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Deconstruct(out int id, out string name)
    {
        id = Id;
        name = Name;
    }
}
class Student
{
    
}

