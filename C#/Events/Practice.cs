namespace Practice_Delegates
{
    class Practice
    {
        public static void main()
        {
            var evaluation = new Evaluation();
            var calculation = new Calculation();
            var payment = new Payment();

            GoldSale goldSale = new GoldSale();
            goldSale.Gold += evaluation.Evalutaion;
            goldSale.Gold += calculation.Calculate;

            calculation.AmountPaid+=payment.Pay;

            goldSale.GoldSold(5);

        }
    }
    public class GoldSale
    {
        public event Action<double> Gold;

        public void GoldSold(double weight)
        {
            
            Gold?.Invoke(weight);
        }
    }
    public class Evaluation
    {
        public void Evalutaion(double weight)
        {
            Console.WriteLine($"Your {weight} gram of Gold is Evaluated");
        }
    }
    public class Calculation
    {
        public event Action<double> AmountPaid;
        public void Calculate(double weight)
        {
            double value = weight*15715;
            Console.WriteLine($"Your calculated value {weight} gram of Gold is {value}");
            AmountPaid?.Invoke(value);
        }
    }
    public class Payment
    {
        public void Pay(double value)
        {
            Console.WriteLine($"Your {value} is paid");
        }

    }
}