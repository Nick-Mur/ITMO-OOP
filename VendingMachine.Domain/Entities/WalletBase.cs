namespace VendingMachine.Domain.Entities;

/// <summary>
///     Предоставляет контракт для хранения монет внутри торгового автомата.
/// </summary>
public abstract class WalletBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="WalletBase"/>.
    /// </summary>
    /// <param name="coins">Коллекция начальных монет.</param>
    protected WalletBase(IEnumerable<CoinBase> coins)
    {
        Coins = new List<CoinBase>(coins);
    }

    /// <summary>
    ///     Возвращает монеты, которые сейчас хранятся в кошельке.
    /// </summary>
    protected List<CoinBase> Coins { get; }

    /// <summary>
    ///     Рассчитывает текущий баланс кошелька.
    /// </summary>
    /// <returns>Общая сумма номиналов монет.</returns>
    public abstract decimal GetBalance();

    /// <summary>
    ///     Добавляет монету в кошелёк.
    /// </summary>
    /// <param name="coin">Монета для добавления.</param>
    public abstract void AddCoin(CoinBase coin);

    /// <summary>
    ///     Пытается выдать сдачу из кошелька.
    /// </summary>
    /// <param name="amount">Требуемая сумма.</param>
    /// <returns>Коллекция монет, представляющая сдачу.</returns>
    public abstract IReadOnlyCollection<CoinBase> Withdraw(decimal amount);

    /// <summary>
    ///     Очищает кошелёк от монет.
    /// </summary>
    /// <returns>Все монеты, которые были извлечены.</returns>
    public abstract IReadOnlyCollection<CoinBase> Empty();
}
