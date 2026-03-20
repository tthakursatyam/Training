using System;
using System.Reflection;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
class LINQ
{
    public static void func1()
    {
        Console.WriteLine();
        int[] numbers = { 1, 2, 3, 4, 5, 6 };

        var evennumbers1 = from n in numbers where n % 2 == 0 select n;     //Query Syntax

        //n jdsfhjsd
        Console.WriteLine(evennumbers1.GetType());

        foreach (var i in evennumbers1)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        var evennumbers2 = numbers.Where(n => n % 2 == 0);

        foreach (var i in evennumbers2)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();

        List<int> num1 = new List<int> { 10, 25, 30, 45, 60 };

        var result1 = num1.Where(n => n > 30).ToList();

        //n jdsfhjsd
        Console.WriteLine("ffdgf");
        Console.WriteLine(result1.GetType());

        foreach (var i in result1)
        {
            Console.Write(i + " ");
        }
    }
    public static void func2()
    {
        Type t = typeof(string);

        foreach (MethodInfo m in t.GetMethods())
        {
            Console.WriteLine(m.GetBaseDefinition());
        }
    }
    public static void func3()
    {
        List<int> numbers = new List<int> { 10, 20, 30 };

        var deferredQuerry = numbers.Where(n => n > 15);       //this will not execute until deferredQuery called
        numbers.Add(40); //40 is added after deferredQuerry is defined but it will still show in output as deferredQuerry is not executed yet
        foreach (var i in deferredQuerry)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        var immediateQuerry = numbers.Where(n => n > 15)
                                            .ToList();  //Forces to execute immediately
        numbers.Add(50);   //50 is added after immediateQuerry is defined but it will not show in output as immediateQuerry is not executed yet

        foreach (var i in immediateQuerry)
        {
            Console.Write(i + " ");
        }
    }
    public static void func4()
    {
        List<int> number = new List<int> { 10, 20, 30, 45, 12, 23, 78, 56, 89, 88 };

        int size = number.Count;

        var res1 = number.Take(size / 2);
        var res2 = number.Skip(size / 2);

        foreach (var i in res1)
        {
            Console.Write(i + " ");
        }
        foreach (var i in res2)
        {
            Console.Write(i + " ");
        }
    }
    public static void func5()
    {
        // Creating a List
        List<Student8> students = new List<Student8>();


        // Adding students
        students.Add(new Student8 { Id = 1, Name = "Ravi", Age = 20 });
        students.Add(new Student8 { Id = 2, Name = "Kumar", Age = 22 });
        students.Add(new Student8 { Id = 3, Name = "Priya", Age = 19 });

        Console.WriteLine("Total Students: " + students.Count);
        Console.WriteLine();

        // Accessing by index
        Console.WriteLine("First Student: " + students[0].Name);
        Console.WriteLine();

        // Updating student
        students[1].Age = 23;

        // Removing student
        students.RemoveAt(2);

        Console.WriteLine("After Update and Remove:");
        Console.WriteLine("Total Students: " + students.Count);
        Console.WriteLine();

        // Sorting list by Age
        students.Sort((s1, s2) => s1.Age.CompareTo(s2.Age));

        Console.WriteLine("Sorted by Age:");
        foreach (var student in students)
        {
            Console.WriteLine(student.Id + " - " + student.Name + " - " + student.Age);
        }
    }
    public static void func6()
    {
        ArrayList array = new ArrayList { 1, "Mari", "MCA", 2000 };

        var result1 = array.OfType<int>();

        foreach (var i in result1)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        string[] str = new string[] { "Satyam", "Aakash", "Ramanv", "Aksh", "Shubhamv", "Aryan", "Ankit" };

        var result2 = str.Where(str => str[0] == 'A');

        foreach (var i in result2)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine("");
        var result3 = str.Where(str => str.EndsWith("v")).Select(str => "Mr " + str);

        foreach (var i in result3)
        {
            Console.Write(i + " ");
        }
    }
    public static void func7()
    {
        int[] num = new int[] { 1, 4, 53, 70, 89, 76, 9, 456, 80 };

        IEnumerable<int> res1 = num.SkipWhile(n => n < 50);
        IEnumerable<int> res2 = num.TakeWhile(n => n < 50);

        IEnumerable res3 = res1;

        foreach (var i in res3)
        {
            Console.Write(i + " ");
        }
        Console.WriteLine();
        foreach (var i in res2)
        {
            Console.Write(i + " ");
        }
    }
    public static void func8()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 4 };

        IEnumerable<int> result = numbers.Where(x => x > 2);

        numbers.Add(10);

        foreach (var n in result)
        {
            Console.Write(n+" ");
        }
    }
}
class Student8
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}