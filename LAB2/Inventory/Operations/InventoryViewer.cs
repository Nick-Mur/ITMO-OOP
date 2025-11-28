namespace Inventory.Operations;
using Objects;

public class InventoryViewer(List<Item> items)
{
    private readonly List<Item> _items = items;
    
    public List<string> ViewItems()
    {
        return _items.Select(x => x.Name).ToList();
    }
}