namespace Inventory.Operations;
using Objects;
using Objects.Interfaces;


public class InventoryItemUpdater(List<Item> items)
{
    private readonly List<Item> _items = items;
    
    public void Update<T>(string itemName, string upgradeStoneName)
        where T : Item, IUpgradable
    {
        T? item = _items
            .OfType<T>()
            .FirstOrDefault(x => x.Name == itemName);

        UpgradeStone? upgradeStone = _items
            .OfType<UpgradeStone>()
            .FirstOrDefault(x => x.Name == upgradeStoneName);

        if (item == null || upgradeStone == null)
            throw new ArgumentException("Item or upgrade stone not found");

        item.Upgrade(upgradeStone);
    }
}