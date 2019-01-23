using BankingLedger_Console.Helpers;
using System;
using System.Collections.Generic;

namespace BankingLedger_Console
{
   class Program
   {
      public static void Main(string[] args)
      {
         var bRunning = true;
         var bLoggedIn = false;
         var sInput = "";
         User user = null;
         var users = new List<User>();

         Messages.ShowHeader();

         while (bRunning)
         {
            while (!bLoggedIn)
            {
               Console.WriteLine("Please enter one of the options below and press Enter:\n");
               Console.WriteLine("[1]: Register");
               Console.WriteLine("[2]: Login");
               Console.WriteLine("[0]: Exit");

               sInput = Console.ReadLine().ToLower();
               switch (sInput)
               {
                  case "1":
                     Users.Register(users);
                     break;
                  case "2":
                     user = Users.Login(users);
                     if (user != null)
                     {
                        bLoggedIn = true;
                     }
                     break;
                  case "0":
                     System.Environment.Exit(0);
                     break;
                  default:
                     Messages.ShowHeader("\rInvalid selection.  Please enter a number.", "error");
                     break;
               }
            }
            while (bLoggedIn)
            {
               Console.WriteLine("Please enter one of the options below and press Enter:");
               Console.WriteLine("[1]: Make a Deposit");
               Console.WriteLine("[2]: Make a Withdrawal");
               Console.WriteLine("[3]: Check Balance");
               Console.WriteLine("[4]: Transaction History");
               Console.WriteLine("[5]: Logout");
               Console.WriteLine("[0]: Exit");

               sInput = Console.ReadLine().ToLower();
               switch (sInput)
               {
                  //deposit
                  case "1":
                     Messages.ShowHeader();
                     Transactions.Deposit(user);
                     break;
                  //withdrawal
                  case "2":
                     Messages.ShowHeader();
                     Transactions.Withdrawal(user);
                     break;
                  //balance
                  case "3":
                     Transactions.ShowBalance(user);
                     break;
                  //transaction history
                  case "4":
                     Transactions.TransactionHistory(user);
                     break;
                  //logout
                  case "5":
                     user = null;
                     bLoggedIn = false;
                     Messages.ShowHeader("You have successfully logged out.", "success");
                     break;
                  //exit
                  case "0":
                     System.Environment.Exit(0);
                     break;
                  default:
                     Messages.ShowHeader("\rInvalid selection.  Select a number below.", "error");
                     break;
               }
            }
         }
      }
   }
}
