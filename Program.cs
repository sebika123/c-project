//SebikaNepal
//rollno =>23409/076 (39)
using System;

public delegate void TransactionHandler(string accountHolderName, decimal transactionAmount);

public class Account
{
    private string accountNumber;
    private double balance;

    public Account(string accountNumber)
    {
        this.accountNumber = accountNumber;
        this.balance = 0;
    }

    public string AccountNumber
    {
        get { return accountNumber; }
    }

    public event TransactionHandler TransactionMade;

    public void MakeTransaction(decimal transactionAmount)
    {
        if (transactionAmount <= 0)
        {
            Console.WriteLine("Transaction amount must be greater than zero.");
            return;
        }

        balance += (double)transactionAmount;

        TransactionMade?.Invoke(accountNumber, transactionAmount);
    }
}

public class NotificationService
{
    public void SendNotification(string accountHolderName, decimal transactionAmount)
    {
        Console.WriteLine("Transaction made for account holder {0} for amount {1}.", accountHolderName, transactionAmount);
    }
}
public class EventTester
{
    static void Main(string[] args)
    {
        // Create Account and NotificationService objects
        Account account = new Account("0016150000012");
        NotificationService notificationService = new NotificationService();

        // Subscribe SendNotification method to TransactionMade event
        account.TransactionMade += notificationService.SendNotification;

        // Make some transactions
        account.MakeTransaction(35000);
        account.MakeTransaction(-100); // Should produce an error
        account.MakeTransaction(6700);

        Console.ReadKey();
    }
}
