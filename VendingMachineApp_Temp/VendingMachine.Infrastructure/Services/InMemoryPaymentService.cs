using VendingMachine.Application.Services;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Services;

/// <summary>
///     Реализация <see cref="PaymentServiceBase"/>, работающая в памяти.
/// </summary>
public sealed class InMemoryPaymentService : PaymentServiceBase
{
    private readonly Wallet _machineWallet;
    private readonly List<CoinBase> _currentTransactionCoins = new();

    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="InMemoryPaymentService"/>.
    /// </summary>
    /// <param name="machineWallet">Кошелёк, в котором хранятся средства автомата.</param>
    public InMemoryPaymentService(Wallet machineWallet)
    {
        _machineWallet = machineWallet;
    }

    /// <inheritdoc />
    public override void InsertCoin(CoinBase coin)
    {
        _currentTransactionCoins.Add(coin);
    }

    /// <inheritdoc />
    public override decimal GetInsertedAmount()
    {
        return _currentTransactionCoins.Sum(coin => coin.Value);
    }

    /// <summary>
    ///     Проверяет, может ли автомат сформировать сдачу.
    /// </summary>
    /// <param name="amount">Требуемая сумма сдачи.</param>
    /// <returns>true — если сдача возможна; иначе false.</returns>
    public bool CanMakeChange(decimal amount)
    {
        if (amount <= 0)
        {
            return true;
        }

        // Попытка Выдать сдачу
        var withdrawn = _machineWallet.Withdraw(amount).ToList();
        if (withdrawn.Count == 0)
        {
            return false;
        }

        // Возвращаем монеты обратно
        foreach (var coin in withdrawn)
        {
            _machineWallet.AddCoin(coin);
        }

        return true;
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<CoinBase> MakeChange(decimal amount)
    {
        var changeCoins = _machineWallet.Withdraw(amount).ToList();
        if (changeCoins.Count == 0 && amount > 0)
        {
            return Array.Empty<CoinBase>();
        }

        foreach (var coin in _currentTransactionCoins)
        {
            _machineWallet.AddCoin(coin);
        }

        _currentTransactionCoins.Clear();
        return changeCoins;
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<CoinBase> CancelTransaction()
    {
        var refund = _currentTransactionCoins.ToList();
        _currentTransactionCoins.Clear();
        return refund;
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<CoinBase> CollectFunds()
    {
        return _machineWallet.Empty();
    }

    /// <summary>
    ///     Перемещает внесённые монеты в кошелёк автомата при успешной покупке.
    /// </summary>
    public void CommitTransaction()
    {
        foreach (var coin in _currentTransactionCoins)
        {
            _machineWallet.AddCoin(coin);
        }

        _currentTransactionCoins.Clear();
    }
}
