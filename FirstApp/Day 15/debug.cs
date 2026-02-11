class Debug
{
    public static void func1()
    {
        int total = 0;
        for(int i=1;i<=5;i++)
        {
            total+=i;
        }
        Console.WriteLine(total);
        
        List<Users> users = new List<Users>();
        users.Add(new Users{Age=12});
        users.Add(new Users{Age=8});
        users.Add(new Users{Age=90});
        users.Add(new Users{Age=19});
        foreach(var user in users)
        {
          Console.WriteLine("Age: "+user.Age);
        }
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(45);
        queue.Enqueue(345);
        queue.Enqueue(89);
        queue.Enqueue(0);
        queue.Enqueue(100);
        queue.Enqueue(78);
        queue.Enqueue(90);
        while(queue.Count > 0)
        {
            Console.WriteLine(queue.Dequeue()+" ");
        }
        Stack<int> stck = new Stack<int>();
        stck.Push(45);
        stck.Push(35);
        stck.Push(89);
        stck.Push(0);
        stck.Push(100);
        stck.Push(78);
        stck.Push(90);
        while(stck.Count > 0)
        {
            Console.WriteLine(stck.Peek()+" ");
            stck.Pop();
        }
    }

    public static bool Validate(Users user)
    {
        if(user.Age>60) return true;
        return false;
    }
}
class Users
{
    public int Age{get;set;}
}