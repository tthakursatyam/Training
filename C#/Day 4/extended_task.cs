using System;
class adv_prop
{
    private string name,password;
    private int age,studentID;
    public string Name{ get;set;}
    public int StudentID{ get;set;}

    int marks=93;
    public string Marks
    {
        get
        {
            if(marks>=40)
            return "Pass";
            else
            return "Fail";
        }
    }
    public string Password
    {
        set
        {
            if (value.Length >= 6)
            {
                password=value;
            }
        }
    }
    public int Age
    {
        get {return age;}
        set
        {
            if(value>=0 && value<=100)
            {
                age=value;
            }
        }
    }
}