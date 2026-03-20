using System;
sealed class Security
{
    private string pw="admin123";
    public Security(string pw)
    {
        Console.WriteLine("This is Security Module");
        if(this.pw==pw)
        Console.WriteLine("User authentication succesfull");
        else
        Console.WriteLine("User authentication failed");
    }

}

abstract class InsurancePolicy
{
    public int policyno;
    public int Policyno{get;init;}
    public int premium;
    public string? policyholdername;
    public string Policyholdername{set;get;}
    public int Premium
    {
        get {return premium;}
        set
        {
            if(value>0)
            {
                premium=value;
            }
        }
    }
    public InsurancePolicy(int policyno)
    {
        this.policyno=policyno;
    }
    public virtual void insurance_premium()
    {
        Console.WriteLine("Here is your premium amount:"+premium);
    }
    public void display()
    {
        Console.WriteLine("Policy no:"+policyno);
        Console.WriteLine("Policy premium:"+premium);
        Console.WriteLine("Policy holder name:"+policyholdername);
    }
}

class life_inusrance:InsurancePolicy
{
    int life_insurance_charge;
    public life_inusrance(int policyno,int life_insurance_charge) : base(policyno)
    {
        this.life_insurance_charge=life_insurance_charge;
    }
    public override void insurance_premium()
    {
        Console.WriteLine("Here is your updated premium amount:"+(premium+life_insurance_charge));
    }
    public new void display()
    {
        Console.WriteLine("Policy no:"+policyno);
        Console.WriteLine("Policy premium:"+premium);
        Console.WriteLine("Policy holder name:"+policyholdername);
    }
}
class  Health_Insurance:InsurancePolicy
{
    public Health_Insurance(int policyno,int life_insurance_charge) : base(policyno)
    {
        Console.WriteLine("Health_Insurance_Constructor");
    }
    sealed public override void insurance_premium()
    {
        Console.WriteLine("Here is your updated premium amount:"+(premium));
    }
}
// class Policy_Directory
// {
//     private Dictionary<int, string> policies = new Dictionary<int, string>();
//     public string this[int n]
//     {
//         get {return policies.FirstOrDefault(e => e.Value == name).Value;}
//     }
// }


