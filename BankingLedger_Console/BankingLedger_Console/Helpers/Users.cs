using System;
using System.Collections.Generic;
using System.Linq;

namespace BankingLedger_Console.Helpers
{
   public class Users
   {
      public static void Register(List<User> users)
      {
         Messages.ShowHeader("Please enter the following information:\r\n");

         var user = new User();
         user.FirstName = FirstNamePrompt();
         user.LastName = LastNamePrompt();
         user.Username = UsernamePrompt(users);
         user.Password = PasswordPrompt();
         user.Transactions = new List<Transaction>();
         user.LoginAttempts = 0;
         user.Locked = false;

         users.Add(user);
         Messages.ShowHeader("You have successfully registered!", "success");
      }

      public static User Login(List<User> users)
      {
         var bLocked = false;
         var bValidLogin = false;
         var sUsername = String.Empty;
         var sPassword = String.Empty;
         var user = new User();

         Messages.ShowHeader();

         while (!bValidLogin && !bLocked)
         {
            Console.WriteLine("Enter your login credentials below:\n");
            sUsername = UsernameLogin();
            sPassword = PasswordLogin();

            var iCount = users.Count(u => u.Username.ToLower() == sUsername.ToLower());
            if (iCount == 0)
            {
               Messages.ShowHeader("Invalid login credentials.", "error");
               return user = null;
            }

            user = users.FirstOrDefault(u => u.Username.ToLower() == sUsername.ToLower());
            if (user.Password != sPassword)
            {
               user.LoginAttempts++;
               if (user.LoginAttempts > 5)
               {
                  bLocked = true;
                  user.Locked = bLocked;
                  Messages.ShowHeader("Too many login failures.  Your account has been locked.  Please contact support.", "error");
                  return user = null;
               }
               Messages.ShowHeader("Invalid login credentials.", "error");
               return user = null;
            }
            bValidLogin = true;
            user.LoginAttempts = 0;
         }
         Messages.ShowHeader(String.Format("Welcome {0}!", user.FirstName), "success");
         return user;
      }

      private static string FirstNamePrompt()
      {
         var sFirstName = String.Empty;
         while (String.IsNullOrWhiteSpace(sFirstName))
         {
            Console.WriteLine("First Name: ");
            sFirstName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(sFirstName))
            {
               Console.WriteLine("\rFirst Name cannot be blank.", "error");
            }
         }
         return sFirstName;
      }

      private static string LastNamePrompt()
      {
         var sLastName = String.Empty;
         while (String.IsNullOrWhiteSpace(sLastName))
         {
            Console.WriteLine("\rLast Name: ");
            sLastName = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(sLastName))
            {
               Console.WriteLine("\rLast Name cannot be blank.", "error");
            }
         }
         return sLastName;
      }

      private static string UsernamePrompt(List<User> users)
      {
         var sUsername = String.Empty;
         while (String.IsNullOrWhiteSpace(sUsername))
         {
            Console.WriteLine("\rUsername: ");
            sUsername = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(sUsername))
            {
               Console.WriteLine("\rUsername cannot be blank.", "error");
            }

            var iCount = users.Count(u => u.Username.ToLower() == sUsername.ToLower());
            if (iCount != 0)
            {
               sUsername = string.Empty;
               Console.WriteLine("\rThis username already exists.  Please choose a different username.", "error");
            }
         }
         return sUsername;
      }

      private static string PasswordPrompt()
      {
         var sPassword = String.Empty;
         while (String.IsNullOrWhiteSpace(sPassword))
         {
            Console.WriteLine("\rPassword: ");
            sPassword = MaskPassword();
            if (String.IsNullOrWhiteSpace(sPassword))
            {
               Console.WriteLine("\rPassword cannot be blank.", "error");
            }
         }
         return sPassword;
      }

      private static string UsernameLogin()
      {
         var sUsername = String.Empty;
         while (String.IsNullOrWhiteSpace(sUsername))
         {
            Console.WriteLine("Username: ");
            sUsername = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(sUsername))
            {
               Console.WriteLine("\rUsername cannot be blank.", "error");
            }
         }
         return sUsername;
      }

      private static string PasswordLogin()
      {
         var sPassword = String.Empty;
         while (String.IsNullOrWhiteSpace(sPassword))
         {
            Console.WriteLine("\rPassword: ");
            sPassword = MaskPassword();
            if (String.IsNullOrWhiteSpace(sPassword))
            {
               Console.WriteLine("\rPassword cannot be blank.", "error");
            }
         }
         return sPassword;
      }

      private static bool IsLoginValid(List<User> users, string sUsername, string sPassword)
      {
         var iCount = users.Count(u => u.Username.ToLower() == sUsername.ToLower());
         if (iCount == 0)
         {
            Console.WriteLine("\rInvalid login credentials.", "error");
            return false;
         }

         var user = users.FirstOrDefault(u => u.Username.ToLower() == sUsername.ToLower());
         if (user.Password != sPassword)
         {
            user.LoginAttempts++;
            Console.WriteLine("\rInvalid login credentials.", "error");
            return false;
         }
         return true;
      }

      private static String MaskPassword()
      {
         var password = String.Empty;
         while (true)
         {
            ConsoleKeyInfo i = Console.ReadKey(true);
            if (i.Key == ConsoleKey.Enter)
            {
               break;
            }
            else if (i.Key == ConsoleKey.Backspace)
            {
               if (password.Length > 0)
               {
                  password.Remove(password.Length - 1);
                  Console.Write("\b \b");
               }
            }
            else
            {
               password += i.KeyChar;
               Console.Write("*");
            }
         }
         return password;
      }
   }
}
