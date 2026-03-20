using System;
class LMS
{
    private Dictionary<int, string> book = new Dictionary<int, string>();

    public string this[int id]
    {
        get {return book[id];}
        set { book[id]=value;}
    }
    public string this[string name]
    {
        get { return book.FirstOrDefault(e => e.Value == name).Value;}
    }
}