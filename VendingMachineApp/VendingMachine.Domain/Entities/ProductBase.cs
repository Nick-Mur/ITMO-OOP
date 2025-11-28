namespace VendingMachine.Domain.Entities;

/// <summary>
///     Описывает общий контракт для товаров торгового автомата.
/// </summary>
public abstract class ProductBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="ProductBase"/>.
    /// </summary>
    /// <param name="id">Уникальный идентификатор товара.</param>
    /// <param name="name">Название товара.</param>
    /// <param name="price">Цена товара в базовой валюте.</param>
    protected ProductBase(string id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    /// <summary>
    ///     Возвращает идентификатор товара.
    /// </summary>
    public string Id { get; }

    /// <summary>
    ///     Возвращает название товара.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Возвращает цену товара.
    /// </summary>
    public decimal Price { get; }

    /// <summary>
    ///     Создаёт копию товара с другой ценой.
    /// </summary>
    /// <param name="price">Новая цена.</param>
    /// <returns>Скопированный товар.</returns>
    public abstract ProductBase WithPrice(decimal price);
}
