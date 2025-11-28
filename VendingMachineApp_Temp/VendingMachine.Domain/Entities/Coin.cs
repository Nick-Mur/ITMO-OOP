namespace VendingMachine.Domain.Entities;

/// <summary>
///     Конкретное представление монеты определённого номинала.
/// </summary>
public sealed class Coin : CoinBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="Coin"/>.
    /// </summary>
    /// <param name="value">Номинал монеты.</param>
    public Coin(decimal value)
        : base(value)
    {
    }

    /// <inheritdoc />
    public override CoinBase Clone()
    {
        return new Coin(Value);
    }
}
