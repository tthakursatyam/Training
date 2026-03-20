namespace Events_With_Delegates
{
    class Events
    {
        public static void main()
        {
            var orderService = new OrderService();
            var emailService = new EmailService();
            var foodService = new FoodPreparationService();
            var deliveryService = new DeliveryService();

            // Step 1: OrderPlaced subscriptions
            orderService.OrderPlaced += emailService.SendEmail;
            orderService.OrderPlaced += foodService.PrepareFood;

            // Step 2: FoodPrepared subscriptions
            foodService.FoodPrepared += deliveryService.Deliver;

            // Start flow
            orderService.PlaceOrder("Pizza");
        }
    }

    public class DeliveryService
    {
        public void Deliver(string orderId)
        {
            Console.WriteLine($"Order {orderId} delivered");
        }
    }

    public class FoodPreparationService
    {
        // New event
        public event Action<string> FoodPrepared;

        public void PrepareFood(string orderId)
        {
            Console.WriteLine($"Food prepared for order {orderId}");

            // Raise next event
            FoodPrepared?.Invoke(orderId);
        }
    }

    public class EmailService
    {
        public void SendEmail(string orderId)
        {
            Console.WriteLine($"Email sent for order {orderId}");
        }
    }

    public class OrderService
    {
        public event Action<string> OrderPlaced;

        public void PlaceOrder(string orderId)
            {
                Console.WriteLine($"Order {orderId} placed");

            OrderPlaced?.Invoke(orderId);
        }
    }
}