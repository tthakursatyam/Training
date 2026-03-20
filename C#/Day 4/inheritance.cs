using System;
class bnkac
{
    int balance=0;
    public bnkac(int b)
    {
        balance=b;
    }
    public void bnk(){
        Console.WriteLine("Balance: "+balance);
    }
}
class FD:bnkac
{
    public int salary=0;
    public FD(int s,int b)  : base(b)
    {
        salary=s;
    }
    public void display()
    {
        Console.WriteLine("Salary: "+salary);
    }
}
class person
{
    public string name;
    public person(string name)
    {
        this.name=name;
    }
}
class student : person
{
    public int rollno;
    public student(int rollno,string name):base (name)
    {
        this.rollno=rollno;
    }
}
class Department : student
{
    public string de;
    public Department(string de,int rollno,string name):base(rollno,name)
    {
        this.de=de;
    }
}
