class DatabaseApplication
{
    public List<string> Database { get; set; } = new List<string>();
    public List<string> Commands { get; set; } = new List<string>()
    {
        "For Coca Cola (25 SEK) press: 1",
        "For Fanta (25 SEK) press: 2",
        "For Loka (20 SEK) press: 3",
        "For Snickers (15 SEK) press: 4",
        "Cancel: C"
    };

    public DatabaseApplication() => Load();

    public void Load()
    {
        var filename = "/tmp/database.txt";

        if (File.Exists(filename))
            Database = File.ReadAllLines(filename).ToList();
    }

    public void Save()
    {
        var filename = "/tmp/database.txt";

        File.WriteAllLines(filename, Database);
    }

    public void Run(Customer customer)
    {
        var insertMoney = GetInteger("Please add money into machine: ");
            customer.account = insertMoney;
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*** MENU ***");
            Console.WriteLine();
            Console.ResetColor();

            string command;

            do
            {
                command = GetCommand();

                if (command == "1")
                {
                    if (customer.account >= 25)
                    {
                        customer.account -= 25;
                        Console.WriteLine($"Your account: {customer.account}");
                        Console.WriteLine("Do you want to qouit Y/N");
                        var answer = Console.ReadLine()!;

                        if (answer == "Y" || answer == "y")
                        {
                            command = "C";
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough money!");
                        command = "C";
                    }
                }

            } while (command != "C" && command != "c");
    }

    public void List()
    {
        foreach (var company in Database)
        {
            Console.WriteLine(company);
        }

        Console.WriteLine("--");
        Console.WriteLine($"Companies in database: {Database.Count}");
        Console.WriteLine();
    }

    public void Help()
    {

        foreach (var command in Commands)
        {
            if (command != "help")
            {
                Console.WriteLine(command);
            }
        }

        Console.WriteLine();
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

    public string GetCommand()
    {
        while (true)
        {
            Help();

            var input = Console.ReadLine()!;
            return input;
        }
    }
}