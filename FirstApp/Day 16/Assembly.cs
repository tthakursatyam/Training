using System.Reflection;
class Assemble
{
    public static void func1()
    {
        //Console.WriteLine(Assembly.Load("FirstApp.dll"));

        Assembly assembly = Assembly.LoadFrom("D:\\Study\\Semester 8\\C# Prac\\bin\\Debug\\net10.0\\C# Prac.dll");
        Console.WriteLine(Assembly.GetExecutingAssembly());
        Console.WriteLine(assembly);

        foreach(Type t in assembly.GetTypes())
        {
            Console.Write(t+"   ");
        }
        Console.WriteLine();
        Type type = assembly.GetType("LINQ");
        Console.WriteLine(type);

        object obj = Activator.CreateInstance(type);
        Console.WriteLine(obj);
        
        foreach(MethodInfo m in type.GetMethods())
        {
            Console.Write(m+" ");
        }
        Console.WriteLine();

        MethodInfo methodInfo = type.GetMethod("func11");
        Console.WriteLine(methodInfo);
        methodInfo.Invoke(obj,null);


        
        Type type2 = typeof(linq);
        linq emp = new linq();


        MethodInfo info = type2.GetMethod("func4");
        Console.WriteLine(info);


        //info.Invoke(emp,null);


        PropertyInfo prop = type2.GetProperty("Size");
        prop.SetValue(emp, 7);
        Console.WriteLine(emp.Size);
        Console.WriteLine(prop);

        FieldInfo field = type2.GetField("salary");
        Console.WriteLine(field);
        field.SetValue(emp, 50000);
        Console.WriteLine(field.GetValue(emp));

        ConstructorInfo constructorInfo = type2.GetConstructor(Type.EmptyTypes);
        Console.WriteLine(constructorInfo);  
        object obj1 = constructorInfo.Invoke(null);
        Console.WriteLine("name:"+obj1);
        Console.WriteLine("name:"+obj1.GetType);
        Console.WriteLine("name:"+emp.GetType);


        MethodInfo method = type2.GetMethod("parameter");
        ParameterInfo[] parameters = method.GetParameters();

        Console.WriteLine(method);
        Console.Write("(");
        foreach(var i in parameters)
        {
            Console.Write(i+",   ");
        }
        Console.Write(")");




    }
}