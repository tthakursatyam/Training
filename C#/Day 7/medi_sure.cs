class PatientBill
{
    public string BillId;
    public string PatientName;
    public bool HasInsurance;
    public decimal ConsultationFee;
    public decimal LabCharges;
    public decimal MedicineCharges;
    public decimal GrossAmount;
    public decimal DiscountAmount;
    public decimal FinalPayable;
    public PatientBill()
    {
        BillId = "BILL1991";
        PatientName = "Satyam Singh";
        HasInsurance = true;

        ConsultationFee = 500.00m;
        LabCharges = 1200.00m;
        MedicineCharges = 800.00m;
        GrossAmount = 2500.00m;
        DiscountAmount = 500.00m;
        FinalPayable = 2000.00m;
    }


    public void calcGrossAmount()
    {
        GrossAmount = ConsultationFee + LabCharges + MedicineCharges;
    }

    public void calcDiscountAmount()
    {
        if (HasInsurance)
        {
            DiscountAmount = GrossAmount * 0.10m;
        }
        else
        {
            DiscountAmount = GrossAmount;
        }
    }

    public void calcFinalPayable()
    {
        FinalPayable = GrossAmount - DiscountAmount;
    }
}

class Programm
{
    static PatientBill data = new PatientBill();
    static bool HasLastBill = true;

    public static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("MediSure Clinic Billing");
        Console.WriteLine("1. Create New Bill");
        Console.WriteLine("2. View Last Bill");
        Console.WriteLine("3. Clear Last Bill");
        Console.WriteLine("4. Exit");
        Console.Write("Enter your option: ");
    }

    public static void CreateNewBill()
    {
        Console.Write("Enter Bill Id: ");
        data.BillId = Console.ReadLine();

        Console.Write("Enter Patient Name: ");
        data.PatientName = Console.ReadLine();

        Console.Write("Is the patient insured? (Y/N): ");
        string str = Console.ReadLine();
        data.HasInsurance = (str == "Y" || str == "y");

        Console.Write("Enter Consultation Fee: ");
        data.ConsultationFee = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Enter Lab Charges: ");
        data.LabCharges = Convert.ToDecimal(Console.ReadLine());

        Console.Write("Enter Medicine Charges: ");
        data.MedicineCharges = Convert.ToDecimal(Console.ReadLine());

        data.calcGrossAmount();
        data.calcDiscountAmount();
        data.calcFinalPayable();

        Console.WriteLine("\nBill created successfully!");
        Console.WriteLine($"Gross Amount   : {data.GrossAmount}");
        Console.WriteLine($"Discount Amount: {data.DiscountAmount}");
        Console.WriteLine($"Final Payable  : {data.FinalPayable}");
    }

    public static void ViewLastBill()
    {
        if (HasLastBill)
        {
            Console.WriteLine("\n--- Last Bill ---");
            Console.WriteLine($"Bill Id           : {data.BillId}");
            Console.WriteLine($"Patient Name      : {data.PatientName}");
            Console.WriteLine($"Insured           : {data.HasInsurance}");
            Console.WriteLine($"Consultation Fee  : {data.ConsultationFee}");
            Console.WriteLine($"Lab Charges       : {data.LabCharges}");
            Console.WriteLine($"Medicine Charges  : {data.MedicineCharges}");
            Console.WriteLine($"Gross Amount      : {data.GrossAmount}");
            Console.WriteLine($"Discount Amount   : {data.DiscountAmount}");
            Console.WriteLine($"Final Payable     : {data.FinalPayable}");
        }
        else
        {
            Console.WriteLine("No bill data available.");
        }
    }

    public static void ClearLastBill()
    {
        HasLastBill = false;
        Console.WriteLine("Last bill removed successfully.");
    }

    public static void ClearScreen()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    public void Main(string[] args)
    {
        int ch = 0;

        while (ch != 4)
        {
            ShowMenu();
            ch = Convert.ToInt32(Console.ReadLine());

            switch (ch)
            {
                case 1:
                    CreateNewBill();
                    ClearScreen();
                    break;

                case 2:
                    ViewLastBill();
                    ClearScreen();
                    break;

                case 3:
                    ClearLastBill();
                    ClearScreen();
                    break;

                case 4:
                    Console.WriteLine("Thank you. Application closed.");
                    break;

                default:
                    Console.WriteLine("Please enter a choice between 1 and 4.");
                    ClearScreen();
                    break;
            }
        }
    }
}
