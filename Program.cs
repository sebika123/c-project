//q1 SebikaNepal
//rollno =>23409/076 (39)
// using System;

// public delegate void TransactionHandler(string accountHolderName, decimal transactionAmount);

// public class Account
// {
//     private string accountNumber;
//     private double balance;

//     public Account(string accountNumber)
//     {
//         this.accountNumber = accountNumber;
//         this.balance = 0;
//     }

//     public string AccountNumber
//     {
//         get { return accountNumber; }
//     }

//     public event TransactionHandler TransactionMade;

//     public void MakeTransaction(decimal transactionAmount)
//     {
//         if (transactionAmount <= 0)
//         {
//             Console.WriteLine("Transaction amount must be greater than zero.");
//             return;
//         }

//         balance += (double)transactionAmount;

//         TransactionMade?.Invoke(accountNumber, transactionAmount);
//     }
// }

// public class NotificationService
// {
//     public void SendNotification(string accountHolderName, decimal transactionAmount)
//     {
//         Console.WriteLine("Transaction made for account holder {0} for amount {1}.", accountHolderName, transactionAmount);
//     }
// }
// public class EventTester
// {
//     static void Main(string[] args)
//     {
//         // Create Account and NotificationService objects
//         Account account = new Account("0016150000012");
//         NotificationService notificationService = new NotificationService();

//         // Subscribe SendNotification method to TransactionMade event
//         account.TransactionMade += notificationService.SendNotification;

//         // Make some transactions
//         account.MakeTransaction(35000);
//         account.MakeTransaction(-100); // Should produce an error
//         account.MakeTransaction(6700);

//         Console.ReadKey();
//     }
// }


//q2
//SebikaNepal
//rollno =>23409/076 (39)
using System;
using System.Collections.Generic;
using System.Threading;

public delegate void AnswerHandler(int playerId, string selectedAnswer);

public class QuizGame
{
    public List<string> questions;
    public List<string> correctAnswers;
    public event AnswerHandler AnswerSelected;

    public QuizGame()
    {
        questions = new List<string>();
        correctAnswers = new List<string>();
    }

    public void AddQuestion(string question, string correctAnswer)
    {
        questions.Add(question);
        correctAnswers.Add(correctAnswer);
    }

    public void StartGame(int timeLimitSeconds)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            Console.WriteLine("Question {0}: {1}", i + 1, questions[i]);
            Console.WriteLine("Select your answer:");

            using (var cts = new CancellationTokenSource(timeLimitSeconds * 1000))
            {
                string selectedAnswer = null;

                Timer timer = new Timer(state =>
                {
                    cts.Cancel();
                }, null, timeLimitSeconds * 1000, Timeout.Infinite);

                try
                {
                    selectedAnswer = Console.ReadLine();
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Time's up!");
                }
                finally
                {
                    timer.Dispose();
                    Console.WriteLine();
                }

                AnswerSelected?.Invoke(i + 1, selectedAnswer);
            }
        }
    }
}

public class Player
{
    public int PlayerId { get; }

    public Player(int playerId)
    {
        PlayerId = playerId;
    }

    public void SelectAnswer(int questionId, string selectedAnswer)
    {
        Console.WriteLine("Player {0} selected answer '{1}' for Question {2}", PlayerId, selectedAnswer, questionId);
    }
}

public class Program
{
    public static void Main()
    {
        QuizGame quizGame = new QuizGame();

        quizGame.AddQuestion("Which country  is highly populated?", "China");
        quizGame.AddQuestion("What is the national animal of Nepal?", "Cow");
        quizGame.AddQuestion("Who is the first programmer?", "Lady Ada Lovelace");

        Player player1 = new Player(1);

        quizGame.AnswerSelected += player1.SelectAnswer;

        quizGame.StartGame(10);

        Console.WriteLine("\nQuiz Game Results:");
        for (int i = 0; i < quizGame.questions.Count; i++)
        {
            Console.WriteLine("Question {0}: {1}", i + 1, quizGame.questions[i]);
            Console.WriteLine("Correct answer: {0}", quizGame.correctAnswers[i]);
            Console.WriteLine();
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}