using System;

class Program
{
    static void Main(string[] args)
    {
        BankSystem bankSystem = new BankSystem();

        while (true)
        {
            bankSystem.PrintTitle();

            Console.WriteLine("Banking System");
            Console.WriteLine("[1] New Account");
            Console.WriteLine("[2] Transactions");
            Console.WriteLine("[3] Exit");
            Console.Write("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    bankSystem.CreateNewAccount();
                    break;
                case 2:
                    bankSystem.PerformTransactions();
                    break;
                case 3:
                    if (Exit())
                    {
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey();
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static bool Exit()
    {
        Console.Clear();
        Console.WriteLine("Exit Program\n");
        Console.Write("Full Name: Jhon Keneth Ryan B. Namias\n");
        Console.Write("Course/Yr./ Sec.: BSCS - 2A\n");
        Console.Write("Subject Code: [ CS 105 ] Programming Languages\n\n");
        Console.Write("Type 'exit' to confirm exit: ");
        string exitConfirmation = Console.ReadLine();

        if (exitConfirmation.ToLower() == "exit")
        {
            Console.WriteLine("\nExiting program...");
            return true;
        }
        else
        {
            Console.WriteLine("\nExit confirmation failed. Returning to main menu.");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return false;
        }
    }
}

class BankAccount
{
    public string AccountType { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public char MI { get; set; }
    public decimal Balance { get; set; }
}

class BankSystem
{
    private BankAccount[] accounts = new BankAccount[100];
    private int accountCount = 0;
    public void PrintTitle()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("  ____              _    _                _____           _                 ");
        Console.WriteLine(" |  _ \\            | |  (_)              / ____|         | |                ");
        Console.WriteLine(" | |_) | __ _ _ __ | | ___ _ __   __ _  | (___  _   _ ___| |_ ___ _ __ ___  ");
        Console.WriteLine(" |  _ < / _` | '_ \\| |/ / | '_ \\ / _` |  \\___ \\| | | / __| __/ _ \\ '_ ` _ \\ ");
        Console.WriteLine(" | |_) | (_| | | | |   <| | | | | (_| |  ____) | |_| \\__ \\ ||  __/ | | | | |");
        Console.WriteLine(" |____/ \\__,_|_| |_|_|\\_\\_|_| |_|\\__, | |_____/ \\__, |___/\\__\\___|_| |_| |_|");
        Console.WriteLine("                                  __/ |          __/ |                      ");
        Console.WriteLine("                                 |___/          |___/                       ");
        Console.ForegroundColor = ConsoleColor.White;
    }

    public void CreateNewAccount()
    {
        PrintTitle();

        if (accountCount >= accounts.Length)
        {
            Console.WriteLine("Maximum number of accounts reached.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        var newAccount = new BankAccount();

        Console.WriteLine("New Account\n" +
                          "[1] Savings\n" +
                          "[2] Current\n" +
                          "[3] Back");
        Console.Write("Enter account type: ");
        if (!int.TryParse(Console.ReadLine(), out int typeChoice) || typeChoice < 1 || typeChoice > 3)
        {
            Console.WriteLine("Invalid input. Please enter a valid choice.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        newAccount.AccountType = typeChoice == 1 ? "Savings" : typeChoice == 2 ? "Current" : "";
        if (typeChoice == 3) return;
        Console.Write("Enter First Name: "); newAccount.FName = Console.ReadLine();
        Console.Write("Enter Last Name: "); newAccount.LName = Console.ReadLine();
        Console.Write("Enter Middle Initial: "); newAccount.MI = char.TryParse(Console.ReadLine(), out char MI) ? MI : ' ';
        Console.Write("Enter Initial Deposit (minimum 500): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal initialDeposit) || initialDeposit < 500)
        {
            Console.WriteLine("Invalid input. Initial deposit must be a number greater than or equal to 500.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        newAccount.Balance = initialDeposit;
        accounts[accountCount++] = newAccount;
        Console.WriteLine("Account created successfully!\nAccount Number: " + accountCount);

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public void PerformTransactions()
    {
        PrintTitle();

        Console.Write("Enter Account Number (1 to " + accountCount + "): ");
        if (!int.TryParse(Console.ReadLine(), out int accountNumber) || accountNumber < 1 || accountNumber > accountCount)
        {
            Console.WriteLine("Invalid account number.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        var selectedAccount = accounts[accountNumber - 1];

        Console.WriteLine("Transactions\n" +
                          "[1] Deposit\n" +
                          "[2] Withdraw\n" +
                          "[3] Balance\n" +
                          "[4] Back");
        Console.Write("Enter your choice: ");
        if (!int.TryParse(Console.ReadLine(), out int transChoice) || transChoice < 1 || transChoice > 4)
        {
            Console.WriteLine("Invalid input. Please enter a valid choice.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        switch (transChoice)
        {
            case 1: Deposit(selectedAccount); break;
            case 2: Withdraw(selectedAccount); break;
            case 3: CheckBalance(selectedAccount); break;
        }
    }

    private void Deposit(BankAccount account)
    {
        PrintTitle();

        Console.Write("Enter amount to deposit: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid amount.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        account.Balance += amount;
        Console.WriteLine("Amount deposited. New balance: " + account.Balance);

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private void Withdraw(BankAccount account)
    {
        PrintTitle();

        Console.Write("Enter amount to withdraw: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid amount.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            return;
        }

        if (account.Balance >= amount)
        {
            account.Balance -= amount;
            Console.WriteLine("Amount withdrawn. New balance: " + account.Balance);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Insufficient balance.");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void CheckBalance(BankAccount account)
    {
        PrintTitle();

        Console.WriteLine("Account type: " + account.AccountType);
        Console.WriteLine("Fullname: " + account.FName + " " + account.LName + ", " + account.MI);
        Console.WriteLine("Balance: " + account.Balance);

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}
