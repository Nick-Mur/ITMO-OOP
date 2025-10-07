using System;

namespace VendingMachine.Domain.Entities;

/// <summary>
///     Конкретная ячейка инвентаря, управляющая запасами товара.
/// </summary>
public sealed class InventorySlot : InventorySlotBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="InventorySlot"/>.
    /// </summary>
    /// <param name="product">Товар, который хранится в ячейке.</param>
    /// <param name="quantity">Начальное количество товара.</param>
    public InventorySlot(ProductBase product, int quantity)
        : base(product, quantity)
    {
    }

    /// <inheritdoc />
    public override void AddStock(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        Quantity += amount;
    }

    /// <inheritdoc />
    public override bool TryDispense()
    {
        if (Quantity <= 0)
        {
            return false;
        }

        Quantity--;
        return true;
    }
}
