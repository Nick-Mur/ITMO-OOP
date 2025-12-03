namespace LAB3.Patterns.Strategy;

using Order.Core;
using Menu.Core;

public class FastDeliveryPriceStrategy : IPriceCalculationStrategy
{
    public int CalculatePrice(Order order, Menu menu)
    {
        int totalPrice = 0;
        foreach (var (dishName, quantity) in order.Items)
        {
            var dish = menu.GetDishByName(dishName);
            if (dish == null)
                throw new InvalidOperationException($"Dish '{dishName}' not found in menu.");
            
            totalPrice += dish.Price * quantity;
        }
        return totalPrice + 100; // Extra charge for fast delivery
    }
}
