namespace LAB3.Objects;

public class Dish(string name, int price, int cookingTime)
{
    public string Name {get; private set;} = name;
    public int Price {get; set;} = price;
    public int CookingTime { get; set; } = cookingTime;
}