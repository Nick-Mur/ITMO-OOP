namespace Inventory;
using Objects;

public class InventoryRemover(List<Item> items)
{
    private readonly List<Item> _items = items;
    
    public void Remove(Item item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);   
        }
    }

    public void Remove(params Item[] items)
    {
        foreach (var item in items)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
            }
        }
    }
    
    public void RemoveRange(IEnumerable<Item> items)
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