namespace CLI;

using LAB0.VendingMachine.Core;
using LAB0.Objects;

internal static class Program
{
    private static void Main()
    {
        var vm = new VendingMachine(adminCode: "1234");
        var insertedCoins = new List<Coin>();

        Seed(vm);

        while (true)
        {
            Console.WriteLine("\n=== ВЕНДИНГ ===");
            Console.WriteLine("1) Пользователь");
            Console.WriteLine("2) Администратор");
            Console.WriteLine("0) Выход");
            Console.Write("Выбор: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    UserCli.Run(vm, insertedCoins);
                    break;

                case "2":
                    AdminCli.Run(vm);
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    private static void Seed(VendingMachine vm)
    {
        const string admin = "1234";
        
        vm.AddProduct(new Product("Water", 25), admin);
        vm.AddProduct(new Product("Water", 25), admin);
        vm.AddProduct(new Product("Chips", 40), admin);
        vm.AddProduct(new Product("Soda", 60), admin);

        vm.AddCoin(new Coin(1), admin);
        vm.AddCoin(new Coin(2), admin);
        vm.AddCoin(new Coin(5), admin);
        vm.AddCoin(new Coin(10), admin);
    }
}