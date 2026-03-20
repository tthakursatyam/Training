using System;
class StudentPrf
{
    private string name;
    private int age,marks;

    public int Age
    {
        get {return age;}
        set
        {
            if(value>=0)
            {
                age=value;
            }
        }
    }
    public int Marks
    {
        get {return marks;}
        set
        {
            if(value>=0 && value<=100)
            {
                marks=value;
            }
        }
    }

    public string Name
    {
        get {return name;}
        set
        {
            if (value != null)
            {
                name = value;
            }
        }
    }
}