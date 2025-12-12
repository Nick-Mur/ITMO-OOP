namespace LAB0.VendingMachine.Operations;

using Objects;

public class GetterProductBuyName(List<Product> products)
{
    private readonly List<Product> _products = products;
    
    public Product GetProductByName(string productName)
    {
        Product? product = _products.FirstOrDefault(
            p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));

        return product ?? throw new InvalidOperationException($"Товар '{productName}' не найден.");
    }
}
