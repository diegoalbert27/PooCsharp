using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Classes
{
    internal class LineOfCreditAccount : BankAccountClass
    {
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit) 
        { 

        }

        public override void PerformMonthEndTransactions()
        {
            if (Balance < 0)
            {
                decimal interest = -Balance * 0.7m;
                MakeWithdrawal(interest, DateTime.Now, "Carga de interes mensual");
            }
        }

        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            return isOverdrawn ? new Transaction(-20, DateTime.Now, "Aplica un cargo adicional por sobrepasar el credito") : default;
        }
    }
}
