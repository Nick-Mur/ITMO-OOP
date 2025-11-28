namespace Objects;
using ObjectStates;
using Interfaces;

public class Armor(int armorClass, string name): Equippable(name),  IUpgradable
{
    private int ArmorClass { get; set; } = armorClass;
    
    public UpgradeStone Upgrade(UpgradeStone upgradeStone)
    {
        ArmorClass += 1;
        
        upgradeStone.Use();
        
        return upgradeStone;
    }
}