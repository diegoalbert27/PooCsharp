using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Classes
{
    internal class InterestEarningAccount : BankAccountClass
    {
        public InterestEarningAccount(string name, decimal initialBalance) : base(name, initialBalance)
        {

        }

        public override void PerformMonthEndTransactions()
        {
            if (Balance > 500m)
            {
                decimal interest = Balance * 0.5m;
                MakeDeposit(interest, DateTime.Now, "Interes mensual aplicado");
            }
        }
    }
}
