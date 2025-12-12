namespace LAB0.VendingMachine.Core;

using Objects;

public abstract class VendingMachineStorage
{
    protected readonly List<Coin> _coins = new();
    public IReadOnlyList<Coin> Coins => _coins;
    
    protected readonly List<Product> _products = new();
    public IReadOnlyList<Product> Products => _products;
}