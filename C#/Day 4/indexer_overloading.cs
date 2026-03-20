using System;
class EmployeeDirectory
{
    private Dictionary<int, string> employees = new Dictionary<int, string>();

    public string this[int id]
    {
        get { return employees[id]; }
        set { employees[id] = value; }
    }

    public string this[string name]
    {
        get
        {
            return employees.FirstOrDefault(e => e.Value == name).Value;
        }
    }
}