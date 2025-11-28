namespace VendingMachine.Domain.Entities;

/// <summary>
///     Предоставляет базовое поведение для монет, принимаемых торговым автоматом.
/// </summary>
public abstract class CoinBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="CoinBase"/>.
    /// </summary>
    /// <param name="value">Номинал монеты.</param>
    protected CoinBase(decimal value)
    {
        Value = value;
    }

    /// <summary>
    ///     Возвращает номинал монеты.
    /// </summary>
    public decimal Value { get; }

    /// <summary>
    ///     Создаёт копию текущей монеты.
    /// </summary>
    /// <returns>Клонированная монета.</returns>
    public abstract CoinBase Clone();
}
