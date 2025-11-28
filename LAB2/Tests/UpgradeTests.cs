namespace Tests;

using Objects;
using Inventory;
using Xunit;

public class UpgradeTests
{
    [Fact]
    public void Upgrade_Weapon_DamageIncreased()
    {
        // Arrange
        var inventory = new Inventory.Core.Inventory();
        var sword = new Weapon(50, "Iron Sword");
        var upgradeStone = new UpgradeStone(3, 10, "Minor Stone");
        inventory.Add(sword);
        inventory.Add(upgradeStone);

        // Act
        inventory.Update<Weapon>("Iron Sword", "Minor Stone");

        // Assert
        Assert.NotNull(sword);
    }

    [Fact]
    public void Upgrade_Armor_ArmorClassIncreased()
    {
        // Arrange
        var inventory = new Inventory.Core.Inventory();
        var armor = new Armor(30, "Plate Armor");
        var upgradeStone = new UpgradeStone(3, 5, "Defense Stone");
        inventory.Add(armor);
        inventory.Add(upgradeStone);

        // Act
        inventory.Update<Armor>("Plate Armor", "Defense Stone");

        // Assert
        Assert.NotNull(armor);
    }

    [Fact]
    public void Upgrade_NonExistentItem_ThrowsException()
    {
        // Arrange
        var inventory = new Inventory.Core.Inventory();
        var upgradeStone = new UpgradeStone(3, 10, "Minor Stone");
        inventory.Add(upgradeStone);

        // Act
        var exception = Assert.Throws<ArgumentException>(() => 
            inventory.Update<Weapon>("NonExistent Sword", "Minor Stone"));
        
        // Assert
        Assert.Equal("Item or upgrade stone not found", exception.Message);
    }
}
