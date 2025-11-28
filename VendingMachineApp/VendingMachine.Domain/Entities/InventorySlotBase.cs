namespace VendingMachine.Domain.Entities;

/// <summary>
///     Предоставляет базовое поведение для хранения товаров в торговом автомате.
/// </summary>
public abstract class InventorySlotBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="InventorySlotBase"/>.
    /// </summary>
    /// <param name="product">Товар, хранящийся в ячейке.</param>
    /// <param name="quantity">Начальное количество товара.</param>
    protected InventorySlotBase(ProductBase product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    /// <summary>
    ///     Возвращает товар, хранящийся в ячейке.
    /// </summary>
    public ProductBase Product { get; }

    /// <summary>
    ///     Возвращает текущее количество товара.
    /// </summary>
    public int Quantity { get; protected set; }

    /// <summary>
    ///     Добавляет товар в ячейку.
    /// </summary>
    /// <param name="amount">Количество добавляемых единиц.</param>
    public abstract void AddStock(int amount);

    /// <summary>
    ///     Пытается выдать товар из ячейки.
    /// </summary>
    /// <returns>Значение <c>true</c>, если товар выдан; иначе <c>false</c>.</returns>
    public abstract bool TryDispense();
}
