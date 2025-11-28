namespace Objects;
using ObjectStates;
using Interfaces;

public class Weapon(int damage, string name): Equippable(name), IUpgradable
{
    private int Damage { get; set; } = damage;

    public UpgradeStone Upgrade(UpgradeStone upgradeStone)
    {
        Damage += upgradeStone.Effect;
        
        upgradeStone.Use();
        
        return upgradeStone;
    }
}
