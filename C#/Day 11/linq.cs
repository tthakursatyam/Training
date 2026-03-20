using System;
using System.Linq;
using System.Reflection;
class linq
{
    public int Size { get; set; }
    public int salary;
    public static void parameter<T>(int a,bool  b,double c,string d,char[] e,object f,List<T> g)
    {
        a++;
    }
    public linq()
    {
        Console.WriteLine("Constructor is called");
    }

    public static void func1()
    {
        int[] number = { 1, 2, 3, 4, 5, 6, 7, 8 };
        var evenNumbers = number.Where(n => n % 2 == 0);
        var oddNumbers = number.Where(n => n % 2 != 0);
        Console.WriteLine(evenNumbers);
        foreach (var i in evenNumbers)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        foreach (var i in oddNumbers)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        var result = number.Where(n => n > 3).Select(n => n * 3);
        foreach (var i in result)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
    }
    public static void func2()
    {
        List<Student2> student = new List<Student2>()
        {
            new Student2 { Name = "Satyam", Marks = 70 },
            new Student2 { Name = "Sandeep", Marks = 50 }
        };


        var result3 = student.Select(s => new
        {
            s.Name,
            s.Marks,
            Result = s.Marks > 60 ? "Pass" : "Fail"
        });
        var result4 = student.Select(s => new
        {
            s.Name,
            s.Marks,
            Result = s.Marks > 60 ? "Pass" : "Fail"
        }).ToList();


        foreach (var i in result3)
        {
            Console.WriteLine(i + " ");
        }
        Console.WriteLine();
        Console.WriteLine(result3.GetType());
        Console.WriteLine(result4.GetType());
    }
    public static void func3()
    {
        List<int> numbers = new List<int> { 5, 2, 8, 1, 3 };
        var asc = numbers.OrderBy(n => n);
        var desc = numbers.OrderByDescending(n => n);

        foreach (var i in asc)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        foreach (var i in desc)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        List<Student2> ls = new List<Student2>()
        {
            new Student2 {Name="Satyam",Result="Pass",Marks=100},
            new Student2 {Name="Sandeep",Result="Pass",Marks=80},
            new Student2 {Name="Raman",Result="Fail",Marks=60},
        };

        var sortedByMarks = ls.OrderBy(n => n.Marks);

        foreach (var i in sortedByMarks)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
    }
    public static void func4()
    {
        List<Employeee> employee = new List<Employeee>();
        employee.Add(new Employeee { name = "Sandeep", salary = 122 });
        employee.Add(new Employeee { name = "Satyam", salary = 122 });
        employee.Add(new Employeee { name = "aakash", salary = 122 });
        employee.Add(new Employeee { name = "Raman", salary = 170 });

        var sortedBySalary = employee.OrderByDescending(s => s.salary).ThenBy(s => s.name);
        Console.WriteLine(sortedBySalary.GetType());

        foreach (var i in sortedBySalary)
        {
            Console.WriteLine(i.name + " " + i.salary);
        }

        List<int> numbers = new List<int>()
        {
            2,1,2,3,4,5
        };
        Console.WriteLine(numbers.First());
        Console.WriteLine(numbers.First(n => n > 3));
        Console.WriteLine(numbers.Last());
        Console.WriteLine(numbers.Last(n => n > 3));
        //Console.WriteLine(numbers.Single());
        //Console.WriteLine(numbers.Single(n=>n>4));
        Console.WriteLine(numbers.Count());
        Console.WriteLine(numbers.Count(n => n > 4));
    }
    public static void func5()
    {
        //          groupby,tolookup,join,groupjoin
        List<Student2> st = new List<Student2>()
        {
            new Student2 { Id = 1, Name = "A", Marks = 80 },
            new Student2 { Id = 1, Name = "A", Marks = 70 },
            new Student2 { Id = 2, Name = "B", Marks = 60 },
            new Student2 { Id = 2, Name = "B", Marks = 90 },
            new Student2 { Id = 3, Name = "C", Marks = 50 }
        };
        var result = st.GroupBy(s => s.Marks);
        // foreach(var i in result)
        // {
        //     Console.Write("id: "+i.Key+" ");
        //     foreach(var j in i)
        //     {
        //         Console.Write("Marks: "+j.Marks+" ");
        //     }
        // }

    }
    
}
class Employeee
{
    public string name;
    public int salary;
}
class Student2
{
    public string Name;
    public int Id;
    public int Marks;
    public string Result;

}