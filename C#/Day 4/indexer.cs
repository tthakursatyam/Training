using System;
class Stucoll
{
    private string[] std= new string[3];
    public string this[int index]
    {
        get {return std[index];}
        set {std[index]=value;}
    }
}