namespace LAB0.VendingMachine.Operations;

using Objects;

public class GetterCoinBuyNominal( List<Coin> coins)
{
    private readonly List<Coin> _coins = coins;
    
    public Coin GetCoinByNominal(int coinNominal)
    {
        Coin? coin = _coins.FirstOrDefault(p => p.Nominal == coinNominal);
        
        return coin ?? throw new InvalidOperationException($"Монета номиналом '{coinNominal}' не найдена.");
    }
}
