using System.Globalization;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Services;

var slots = new List<InventorySlot>
{
    new(new Product("A1", "Газированная вода", 1.50m), 5),
    new(new Product("B2", "Шоколад", 2.00m), 4),
    new(new Product("C3", "Чипсы", 1.80m), 6)
};

var machineWallet = new Wallet(new List<CoinBase>
{
    new Coin(0.50m),
    new Coin(0.50m),
    new Coin(1.00m),
    new Coin(1.00m),
    new Coin(2.00m)
});

var inventoryService = new InMemoryInventoryService(slots);
var paymentService = new InMemoryPaymentService(machineWallet);
var vendingMachine = new VendingMachineService(inventoryService, paymentService);

var isRunning = true;

while (isRunning)
{
    Console.WriteLine();
    Console.WriteLine("=== Торговый автомат ===");
    Console.WriteLine("1. Показать товары");
    Console.WriteLine("2. Внести монету");
    Console.WriteLine("3. Показать внесённую сумму");
    Console.WriteLine("4. Купить товар");
    Console.WriteLine("5. Отменить операцию");
    Console.WriteLine("6. Админ: Пополнить товар");
    Console.WriteLine("7. Админ: Забрать средства");
    Console.WriteLine("8. Выход");
    Console.Write("Выберите пункт меню: ");

    var choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            DisplayProducts();
            break;
        case "2":
            InsertCoin();
            break;
        case "3":
            ShowInsertedAmount();
            break;
        case "4":
            PurchaseProduct();
            break;
        case "5":
            CancelTransaction();
            break;
        case "6":
            RestockProduct();
            break;
        case "7":
            CollectFunds();
            break;
        case "8":
            isRunning = false;
            break;
        default:
            Console.WriteLine("Неизвестный пункт меню.");
            break;
    }
}

void DisplayProducts()
{
    var products = vendingMachine.GetAvailableProducts();
    Console.WriteLine("Товары:");
    foreach (var slot in products)
    {
        Console.WriteLine($"{slot.Product.Id}: {slot.Product.Name} - {slot.Product.Price.ToString("C", CultureInfo.CurrentCulture)} (Кол-во: {slot.Quantity})");
    }
}

void InsertCoin()
{
    Console.Write("Введите номинал монеты: ");
    if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) && value > 0)
    {
        vendingMachine.InsertCoin(new Coin(value));
        Console.WriteLine($"Внесено {value.ToString("C", CultureInfo.CurrentCulture)}");
        Console.WriteLine($"Текущая сумма: {vendingMachine.GetInsertedAmount().ToString("C", CultureInfo.CurrentCulture)}");
    }
    else
    {
        Console.WriteLine("Некорректный номинал монеты.");
    }
}

void ShowInsertedAmount()
{
    var amount = vendingMachine.GetInsertedAmount();
    Console.WriteLine(amount == 0
        ? "Вы ещё не внесли монеты."
        : $"Внесённая сумма: {amount.ToString("C", CultureInfo.CurrentCulture)}");
}

void PurchaseProduct()
{
    Console.Write("Введите идентификатор товара: ");
    var productId = Console.ReadLine() ?? string.Empty;
    var result = vendingMachine.Purchase(productId);
    Console.WriteLine(result.Message);

    if (result.IsSuccessful)
    {
        if (result.DispensedProduct is not null)
        {
            Console.WriteLine($"Выдано: {result.DispensedProduct.Name}");
        }

        if (result.Change.Count > 0)
        {
            Console.WriteLine("Возвращена сдача:");
            foreach (var coin in result.Change)
            {
                Console.WriteLine(coin.Value.ToString("C", CultureInfo.CurrentCulture));
            }
        }
    }
    else
    {
        if (result.Change.Count > 0)
        {
            Console.WriteLine("Возвращены монеты:");
            foreach (var coin in result.Change)
            {
                Console.WriteLine(coin.Value.ToString("C", CultureInfo.CurrentCulture));
            }
        }
    }
}

void CancelTransaction()
{
    var refund = vendingMachine.Cancel();
    if (refund.Count == 0)
    {
        Console.WriteLine("Нет монет для возврата.");
        return;
    }

    Console.WriteLine("Операция отменена. Возвращённые монеты:");
    foreach (var coin in refund)
    {
        Console.WriteLine(coin.Value.ToString("C", CultureInfo.CurrentCulture));
    }
}

void RestockProduct()
{
    Console.Write("Введите идентификатор товара: ");
    var id = Console.ReadLine() ?? string.Empty;
    Console.Write("Введите название товара: ");
    var name = Console.ReadLine() ?? string.Empty;
    Console.Write("Введите цену: ");
    if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price) || price <= 0)
    {
        Console.WriteLine("Некорректная цена.");
        return;
    }

    Console.Write("Введите количество: ");
    if (!int.TryParse(Console.ReadLine(), out var qty) || qty < 0)
    {
        Console.WriteLine("Некорректное количество.");
        return;
    }

    vendingMachine.Restock(new Product(id, name, price), qty);
    Console.WriteLine("Товар добавлен / пополнен.");
}

void CollectFunds()
{
    var funds = vendingMachine.CollectFunds();
    if (funds.Count == 0)
    {
        Console.WriteLine("Средств нет.");
        return;
    }

    Console.WriteLine("Изъяты монеты:");
    foreach (var coin in funds)
    {
        Console.WriteLine(coin.Value.ToString("C", CultureInfo.CurrentCulture));
    }
}