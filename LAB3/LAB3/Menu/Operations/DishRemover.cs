namespace LAB3.Menu.Operations;
using Objects;

public class DishRemover(List<Dish> items)
{
    private readonly List<Dish> _items = items;
    
    public void Remove(Dish item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);   
        }
    }

    public void Remove(params Dish[] items)
    {
        foreach (var item in items)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
        }
    }
    
    public void RemoveRange(IEnumerable<Dish> items)
    {

        foreach (var item in items)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
        }
    }
}