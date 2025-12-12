namespace LAB0.Objects;

public class Product(string name, int price)
{
    public string Name {get; private set;} = name;
    public int Price {get; set;} = price;
}