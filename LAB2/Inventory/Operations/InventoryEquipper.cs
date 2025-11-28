using Objects.ObjectStates;

namespace Inventory.Operations;
using Objects;

public class InventoryEquipper(List<Item> items)
{
    private readonly List<Item> _items = items;
    
    public void Equip(Equippable item)
    {
        Equippable? result = _items.OfType<Equippable>().FirstOrDefault(x =>  x.GetType() == item.GetType() && x.Active);
        
        if (result != null)
        {
            throw new InvalidOperationException("The item in this category is already equipped");
        }

        item.Active = true;
    }
    
    public void Unequip(Equippable item)
    {
        item.Active = false;
    }
   
}