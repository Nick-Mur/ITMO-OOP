namespace LAB3.Objects;


using ListDishOperations;


public class Menu: DishStorage
{
    private readonly DishAdder _adder;
    private readonly DishRemover _remover;
    private readonly DishGetter _getter;

    public Menu()
    {
        _adder = new DishAdder(_items);
        _remover = new DishRemover(_items);
        _getter = new DishGetter(_items);
    }
    public void Add(Dish item) =>  _adder.Add(item);
    public void AddRange(IEnumerable<Dish> items) => _adder.AddRange(items);
    public void Remove(Dish item) =>  _remover.Remove(item);
    public void RemoveRange(IEnumerable<Dish> items) => _remover.RemoveRange(items);
    public Dish? GetDishByName(string dishName) => _getter.GetDishByName(dishName);
}