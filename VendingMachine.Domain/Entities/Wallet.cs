using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine.Domain.Entities;

/// <summary>
///     Конкретная реализация кошелька, хранящего монеты в памяти.
/// </summary>
public sealed class Wallet : WalletBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="Wallet"/>.
    /// </summary>
    /// <param name="coins">Коллекция начальных монет.</param>
    public Wallet(IEnumerable<CoinBase> coins)
        : base(coins)
    {
    }

    /// <inheritdoc />
    public override decimal GetBalance()
    {
        return Coins.Sum(coin => coin.Value);
    }

    /// <inheritdoc />
    public override void AddCoin(CoinBase coin)
    {
        Coins.Add(coin);
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<CoinBase> Withdraw(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }

        var orderedCoins = Coins
            .OrderByDescending(coin => coin.Value)
            .ToList();

        var selectedCoins = new List<CoinBase>();
        var remaining = amount;

        foreach (var coin in orderedCoins.ToList())
        {
            if (remaining <= 0)
            {
                break;
            }

            if (coin.Value <= remaining)
            {
                remaining -= coin.Value;
                selectedCoins.Add(coin);
                Coins.Remove(coin);
            }
        }

        if (remaining > 0)
        {
            Coins.AddRange(selectedCoins);
            return Array.Empty<CoinBase>();
        }

        return selectedCoins;
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<CoinBase> Empty()
    {
        var removed = Coins.ToList();
        Coins.Clear();
        return removed;
    }
}
