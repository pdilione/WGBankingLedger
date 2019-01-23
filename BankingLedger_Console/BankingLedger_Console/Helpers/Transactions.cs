using System;
using System.Globalization;
using System.Linq;

namespace BankingLedger_Console.Helpers
{
   public class Transactions
   {
      public static decimal GetBalance(User user)
      {
         decimal amount = user.Transactions.Sum(t => t.Amount);
         return amount;
      }

      public static void ShowBalance(User user)
      {
         decimal amount = GetBalance(user);
         Messages.ShowHeader("Balance: " + amount.ToString("C", GetCultureInfo()));
         Console.WriteLine("\nPress any key to return home.");
         Console.ReadKey();
         Messages.ShowHeader();
      }

      public static void Deposit(User user)
      {
         var bValidAmount = false;
         var bValidDescription = false;
         decimal dAmount = 0;
         decimal dMinAmount = Properties.Settings.Default.MinAmount;
         var sDescription = String.Empty;
         var transaction = new Transaction();

         while (!bValidAmount)
         {
            Console.WriteLine("Deposit amount: ");
            var sAmount = Console.ReadLine();

            if (Decimal.TryParse(sAmount, out dAmount))
            {
               if (dAmount < dMinAmount)
               {
                  Messages.ShowHeader(String.Format("Amount must be at least ${0}.  Please enter a valid number.", dMinAmount), "error");
               }
               else
               {
                  bValidAmount = true;
               }
            }
            else
            {
               Messages.ShowHeader("Please enter a valid number.", "error");
            }
         }

         while (!bValidDescription)
         {
            Console.WriteLine("Description (optional): ");
            sDescription = Console.ReadLine();

            if (sDescription.Length > 20)
            {
               Messages.ShowHeader(String.Format("Description length cannot be longer than 20 characters.\n\nDeposit amount: {0}", dAmount.ToString("C", GetCultureInfo())), "error");
            }
            else
            {
               bValidDescription = true;
            }
         }

         transaction.Amount = dAmount;
         transaction.Description = sDescription;
         transaction.DateCreated = DateTime.Now;
         transaction.CreatedBy = user.Username;
         user.Transactions.Add(transaction);

         Messages.ShowHeader("Successful deposit: " + transaction.Amount.ToString("C", Transactions.GetCultureInfo()), "success");
         Console.WriteLine("\nPress any key to return home.");
         Console.ReadKey();
         Messages.ShowHeader();
      }

      public static void Withdrawal(User user)
      {
         var bValidAmount = false;
         var bValidDescription = false;
         decimal dAmount = 0;
         decimal dMinAmount = Properties.Settings.Default.MinAmount;
         var sDescription = String.Empty;
         var transaction = new Transaction();

         while (!bValidAmount)
         {
            Console.WriteLine("Withdrawal amount: ");
            var dBalance = GetBalance(user);
            var sAmount = Console.ReadLine();

            if (Decimal.TryParse(sAmount, out dAmount))
            {
               if (dAmount < dMinAmount)
               {
                  Messages.ShowHeader(String.Format("Amount must be at least ${0}.  Please enter a valid number: ", dMinAmount), "error");
               }
               else if (dAmount > dBalance)
               {
                  Messages.ShowHeader(String.Format("Amount cannot be greater than current balance of ${0}.  Please enter a valid number: ", dBalance), "error");
               }
               else
               {
                  bValidAmount = true;
               }
            }
            else
            {
               Messages.ShowHeader("Please enter a valid number.", "error");
            }
         }

         while (!bValidDescription)
         {
            Console.WriteLine("Description (optional): ");
            sDescription = Console.ReadLine();

            if (sDescription.Length > 20)
            {
               Messages.ShowHeader(String.Format("Description length cannot be longer than 20 characters.\n\nDeposit amount: {0}", dAmount.ToString("C"), GetCultureInfo()), "error");
            }
            else
            {
               bValidDescription = true;
            }
         }

         transaction.Amount = dAmount * (-1);
         transaction.Description = sDescription;
         transaction.DateCreated = DateTime.Now;
         transaction.CreatedBy = user.Username;
         user.Transactions.Add(transaction);

         Messages.ShowHeader("Successful withdrawal: " + transaction.Amount.ToString("C", Transactions.GetCultureInfo()), "success");
         Console.WriteLine("\nPress any key to return home.");
         Console.ReadKey();
         Messages.ShowHeader();
      }

      public static void TransactionHistory(User user)
      {
         Messages.ShowHeader("Transaction History" + "\n-------------------");
         Console.WriteLine("{0,-20} {1,25} {2,20}", "Date", "Description", "Amount");
         Console.WriteLine("-------------------------------------------------------------------");

         if (user.Transactions.Count == 0)
         {
            Console.WriteLine("No entries to display.");
         }
         else
         {
            foreach (var transaction in user.Transactions)
            {
               Console.WriteLine("{0,-20:MM/dd/yyyy hh:mm tt} {1,25} {2,20:C}", transaction.DateCreated, transaction.Description, transaction.Amount.ToString("C", GetCultureInfo()));
            }
         }

         Console.WriteLine("\nPress any key to return home.");
         Console.ReadKey();
         Messages.ShowHeader();
      }

      public static CultureInfo GetCultureInfo()
      {
         var culture = CultureInfo.CreateSpecificCulture("en-US");
         culture.NumberFormat.CurrencyNegativePattern = 1;
         return culture;
      }
   }
}
