// Task 1: Implement Patient class with proper encapsulation
public class Patient
{
    // TODO: Add properties with get/set accessors
    public int Id {get;set;}
    public string Name  {get;set;}
    public int Age {get;set;}
    public string Condition {get;set;}
    // TODO: Add constructor
    public Patient(int id,string name,int age,string condition)
    {
        Id=id;
        Name = name;
        Age=age;
        Condition=condition;
    }
}

// Task 2: Implement HospitalManager class
public class HospitalManager
{
    private Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();
    private Queue<Patient> _appointmentQueue = new Queue<Patient>();
    
    // Add a new patient to the system
    public void RegisterPatient(int id, string name, int age, string condition)
    {
        // TODO: Create patient and add to dictionary
        Patient patient = new Patient(id,name,age,condition);
        _patients.Add(id,patient);
    }
    
    // Add patient to appointment queue
    public void ScheduleAppointment(int patientId)
    {
        // TODO: Find patient and add to queue
        _appointmentQueue.Enqueue(_patients[patientId]);
    }
    
    // Process next appointment (remove from queue)
    public Patient ProcessNextAppointment()
    {
        // TODO: Return and remove next patient from queue
        return _appointmentQueue.Dequeue();

    }
    
    // Find patients with specific condition using LINQ
    public List<Patient> FindPatientsByCondition(string condition)
    {
        // TODO: Use LINQ to filter patients
        var SpecificCondition = _patients.Where(x=>x.Value.Condition==condition).Select(x=>x.Value).ToList();
        return SpecificCondition;
    }
}
