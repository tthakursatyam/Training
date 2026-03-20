using System;
delegate void PaymentDelegates(decimal amt);
class PaymentService
{
    public void ProcessPayment(decimal amt)
    {
        Console.WriteLine("Payment of "+amt+" processed successfully");
    }
    public void RTGS(decimal amt)
    {
        Console.WriteLine($"Payment of {amt} is completed by RTGS");
    }
}
static class PaymentExtension
{
    public static bool isValidPayment(this decimal amt)
    {
        return amt>0 && amt<=1_000_000;
    }
}