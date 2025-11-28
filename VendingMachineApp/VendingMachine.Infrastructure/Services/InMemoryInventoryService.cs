using VendingMachine.Application.Services;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Services;

/// <summary>
///     Реализация <see cref="InventoryServiceBase"/>, работающая в памяти.
/// </summary>
public sealed class InMemoryInventoryService : InventoryServiceBase
{
    private readonly IDictionary<string, InventorySlot> _slots;

    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="InMemoryInventoryService"/>.
    /// </summary>
    /// <param name="slots">Начальные ячейки инвентаря.</param>
    public InMemoryInventoryService(IEnumerable<InventorySlot> slots)
    {
        _slots = slots.ToDictionary(slot => slot.Product.Id, slot => slot);
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<InventorySlotBase> GetInventory()
    {
        return _slots.Values.Cast<InventorySlotBase>().ToArray();
    }

    /// <inheritdoc />
    public override InventorySlotBase? TryDispense(string productId)
    {
        if (!_slots.TryGetValue(productId, out var slot))
        {
            return null;
        }

        return slot.TryDispense() ? slot : null;
    }

    /// <inheritdoc />
    public override void Refill(string productId, int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        if (!_slots.TryGetValue(productId, out var slot))
        {
            throw new KeyNotFoundException($"Товар {productId} не найден.");
        }

        slot.AddStock(amount);
    }

    /// <inheritdoc />
    public override void AddProduct(ProductBase product, int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity));
        }

        if (_slots.ContainsKey(product.Id))
        {
            _slots[product.Id].AddStock(quantity);
            return;
        }

        _slots[product.Id] = new InventorySlot(product, quantity);
    }
}
