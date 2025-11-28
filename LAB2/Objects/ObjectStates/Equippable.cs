namespace Objects.ObjectStates;

using Objects;

public abstract class Equippable(string name) : Item(name)
{
    public bool Active {get; set;} =  false;
}