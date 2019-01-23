using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingLedger_Console
{
   public class Transaction
   {
      public decimal Amount { get; set; }
      public DateTime DateCreated { get; set; }
      public string CreatedBy { get; set; }
      public string Description { get; set; }
   }
}
