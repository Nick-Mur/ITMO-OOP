namespace Objects.Interfaces;

using Objects;

public interface IUpgradable
{
    UpgradeStone Upgrade(UpgradeStone stone);
}
