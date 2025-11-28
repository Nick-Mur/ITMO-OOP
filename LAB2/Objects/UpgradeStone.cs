namespace Objects;

public class UpgradeStone(int usages, int effect, string name): Item(name)
{
    public int Effect { get; } = effect;
    private int  Usages { get; set; } = usages;
    
    public void Use()
    {
        if (Usages <= 0)
            throw new InvalidOperationException("UpgradeStone has no usages left.");

        Usages--;
    }
}