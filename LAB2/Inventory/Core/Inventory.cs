namespace Inventory.Core;

using Objects;
using Operations;
using Objects.ObjectStates;
using Objects.Interfaces;


public class Inventory: InventoryStorage
{
    private readonly InventoryAdder _adder;
    private readonly InventoryRemover _remover;
    private readonly InventoryEquipper  _equipper;
    private readonly InventoryItemUpdater  _itemUpdater;
    private readonly InventoryViewer _viewer;
    
    public Inventory()
    {
        _adder = new InventoryAdder(_items);
        _remover = new InventoryRemover(_items);
        _equipper = new InventoryEquipper(_items);
        _itemUpdater = new InventoryItemUpdater(_items);
        _viewer = new InventoryViewer(_items);
    }
    public void Add(Item item) =>  _adder.Add(item);
    public void AddRange(IEnumerable<Item> items) => _adder.AddRange(items);
    public void Remove(Item item) =>  _remover.Remove(item);
    public void RemoveRange(IEnumerable<Item> items) => _remover.RemoveRange(items);
    public void Equip(Equippable item) =>  _equipper.Equip(item);
    public void Unequip(Equippable item) =>  _equipper.Unequip(item);
    public void Update<T>(string itemName, string upgradeStoneName)
        where T : Item, IUpgradable =>
        _itemUpdater.Update<T>(itemName, upgradeStoneName);
    public List<string> ViewItems() => _viewer.ViewItems();
}