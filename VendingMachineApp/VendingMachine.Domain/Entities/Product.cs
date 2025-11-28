namespace VendingMachine.Domain.Entities;

/// <summary>
///     Конкретная реализация <see cref="ProductBase"/>.
/// </summary>
public sealed class Product : ProductBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="Product"/>.
    /// </summary>
    /// <param name="id">Уникальный идентификатор товара.</param>
    /// <param name="name">Название товара.</param>
    /// <param name="price">Цена товара.</param>
    public Product(string id, string name, decimal price)
        : base(id, name, price)
    {
    }

    /// <inheritdoc />
    public override ProductBase WithPrice(decimal price)
    {
        return new Product(Id, Name, price);
    }
}
