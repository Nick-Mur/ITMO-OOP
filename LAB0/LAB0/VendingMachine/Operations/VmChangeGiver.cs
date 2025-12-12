namespace LAB0.VendingMachine.Operations;

using Objects;


public sealed class VmChangeGiver(List<Coin> coins)
{
    private readonly List<Coin>  _coins = coins;

    public bool TryReturnChange(int amount, out List<Coin> change)
    {
        change = new List<Coin>();

        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));
        if (amount == 0)
            return true;
        
        var availableByNominal = _coins
            .GroupBy(c => c.Nominal)
            .ToDictionary(g => g.Key, g => g.Count());
        
        var nominalsDesc = availableByNominal.Keys.OrderByDescending(x => x).ToList();
        var takePlan = new Dictionary<int, int>(); // nominal -> count to take

        int remaining = amount;

        foreach (var nominal in nominalsDesc)
        {
            if (remaining == 0) break;

            int have = availableByNominal[nominal];
            int need = remaining / nominal;
            int take = Math.Min(have, need);

            if (take <= 0) continue;

            takePlan[nominal] = take;
            remaining -= take * nominal;
        }
        
        if (remaining != 0)
            return false;
        
        foreach (var (nominal, count) in takePlan)
        {
            for (int i = 0; i < count; i++)
            {
                int idx = _coins.FindIndex(c => c.Nominal == nominal);
                var coin = _coins[idx];
                _coins.RemoveAt(idx);
                change.Add(coin);
            }
        }
        return true;
    }
}