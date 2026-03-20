using System;

class Events
{
    public static void main()
    {
        var orderService = new OrderService();
        orderService.PlaceOrder("Pizza");
    }
}

// Order Service
public class OrderService
{
    private EmailService _emailService;
    private FoodPreparationService _foodService;

    public OrderService()
    {
        _emailService = new EmailService();
        _foodService = new FoodPreparationService();
    }

    public void PlaceOrder(string orderId)
    {
        Console.WriteLine($"Order {orderId} placed");

        // Manually calling "subscribers"
        _emailService.SendEmail(orderId);
        _foodService.PrepareFood(orderId);
    }
}

// Email Service
public class EmailService
{
    public void SendEmail(string orderId)
    {
        Console.WriteLine($"Email sent for order {orderId}");
    }
}

// Food Preparation Service
public class FoodPreparationService
{
    private DeliveryService _deliveryService;

    public FoodPreparationService()
    {
        _deliveryService = new DeliveryService();
    }

    public void PrepareFood(string orderId)
    {
        Console.WriteLine($"Food prepared for order {orderId}");

        // Manually triggering next step (like next event)
        _deliveryService.Deliver(orderId);
    }
}

// Delivery Service
public class DeliveryService
{
    public void Deliver(string orderId)
    {
        Console.WriteLine($"Order {orderId} delivered");
    }
}