namespace LAB3.Order.Operations;
using Objects;
using LAB3.Menu.Core;



public class OrderTotalPriceGetter(List<string> items)
{
    private readonly List<string> _items = items;
    
    public int GetTotalPrice(string orderType, Menu menu)
    {
        int totalPrice = 0;
        foreach (var dishName in _items)
        {
            Dish dish = menu.GetDishByName(dishName);
            totalPrice += dish.Price;
        }

        if (orderType == "Fast") 
            return totalPrice + 100;
        return totalPrice;
    }
}