class DatabaseApplication
{
    public List<Product> Products { get; } = new List<Product>()
    {
        new Product(25, "Coca Cola"),
        new Product(25, "Fanta"),
        new Product(20, "Loka"),
        new Product(15, "Snickers"),
    };

    public List<string> Commands { get; set; } = new List<string>();

    public DatabaseApplication()
    {
        for (int i = 0; i < Products.Count; i++)
        {
            Commands.Add($"For {Products[i].Name} ({Products[i].Cost} SEK) press: {i + 1},");
        }
    }

    public void DisplayProducts()
    {
        Console.WriteLine();
        for (int i = 0; i < Products.Count; i++)
        {
            Console.WriteLine($"For {Products[i].Name} ({Products[i].Cost} SEK) press: {i + 1}");
        }
    }
   
    public void Run(Customer customer)
    {
            var insertMoney = GetInteger("Please add money into machine: ");
            customer.account = insertMoney;
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("****** MENU ******");
            Console.ResetColor();
            DisplayProducts();

        bool command = true;
          
        do
        {
            var commandInt = GetInteger("Please choose a number: ");
            if (commandInt <= Products.Count)
            {
                for (int i = 0; i < Products.Count; i++)
                {
                    if (commandInt == i + 1)
                    {
                        if (customer.account >= Products[i].Cost)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Your choose: {Products[i].Name}");
                            customer.account -= Products[i].Cost;
                            Console.WriteLine($"Your account: {customer.account} SEK");
                            Console.WriteLine("Do you want to quit Y/N");
                            var answer = Console.ReadLine()!;

                            if (answer == "N" || answer == "n")
                            {
                                DisplayProducts();
                                continue;
                            }

                            if (answer == "Y" || answer == "y")
                            {
                                command = false;
                            }
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("You don't have enough money!");
                            Console.ResetColor();
                            command = false;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("----- Unknown command! -----");
                Console.ResetColor();
                DisplayProducts();
                continue;
            }

        } while (command);
    }

    public int GetInteger(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();

            if (int.TryParse(input, out var number))
                return number;
        }
    }
}