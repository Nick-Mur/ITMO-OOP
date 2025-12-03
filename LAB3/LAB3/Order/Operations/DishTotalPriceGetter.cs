namespace LAB3.Order.Operations;
using Objects;
using LAB3.Menu.Core;

public class OrderTotalPriceGetter(Dictionary<string, int> items)
{
    private readonly Dictionary<string, int> _items = items;
    
    public int GetTotalPrice(string orderType, Menu menu)
    {
        int totalPrice = 0;
        
        foreach (var (dishName, quantity) in _items)
        {
            Dish? dish = menu.GetDishByName(dishName);
            if (dish == null)
                throw new InvalidOperationException($"Блюдо '{dishName}' не найдено в меню.");
            
            totalPrice += dish.Price * quantity;
        }

        if (orderType == "Fast") 
            return totalPrice + 100;
        
        return totalPrice;
    }
}