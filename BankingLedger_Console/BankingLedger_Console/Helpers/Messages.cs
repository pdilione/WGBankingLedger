using System;

namespace BankingLedger_Console.Helpers
{
   public class Messages
   {
      public static void ShowHeader(string sMessage = "", string sType = "")
      {
         Console.Clear();
         Console.WriteLine("Welcome to WG Bank.\r");
         Console.WriteLine("-------------------\n");

         if (!String.IsNullOrWhiteSpace(sMessage))
         {
            switch (sType)
            {
               case "success":
                  Console.ForegroundColor = ConsoleColor.Green;
                  break;
               case "error":
                  Console.ForegroundColor = ConsoleColor.Red;
                  break;
               default:
                  break;
            }

            Console.WriteLine(sMessage + "\n");
            sMessage = String.Empty;
            Console.ResetColor();
         }
      }
   }
}
