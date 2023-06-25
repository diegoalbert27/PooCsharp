using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.Classes
{
    internal class BankAccountClass
    {
        private static int _accountNumberSeed = 1234567890;

        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance 
        { 
            get
            {
                decimal balance = 0;

                foreach (var transaction in this._allTransactions) 
                {
                    balance += transaction.Amount;
                }

                return balance;
            }
        }

        private readonly decimal _minimumBalance;

        public BankAccountClass(string name, decimal initialBalance) : this(name, initialBalance, 0) { }

        public BankAccountClass(string name, decimal initialBalance, decimal minimumBalance) 
        {
            Owner = name;
            Number = _accountNumberSeed.ToString();

            _accountNumberSeed++;

            _minimumBalance = minimumBalance;

            if (initialBalance > 0)
            {
                MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
            }
        }

        private readonly List<Transaction> _allTransactions = new();

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "El monto para el deposito debe ser positivo");
            }

            var deposit = new Transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "El monto para el retiro debe ser positivo");
            }

            Transaction? overdraftTransaction = CheckWithdrawalLimit((Balance - amount) < _minimumBalance);
            Transaction? withdrawal = new(-amount, date, note);
            _allTransactions.Add(withdrawal);

            if (overdraftTransaction != null)
            {
                _allTransactions.Add(overdraftTransaction);
            }
        }

        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("No hay suficiente fondo para el retiro");
            } 
            else
            {
                return default;
            }
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\t\tAmount\tBalance\tNote");

            foreach(var transaction in this._allTransactions)
            {
                balance += transaction.Amount;
                report.AppendLine($"{transaction.Date}\t{transaction.Amount}\t{balance}\t{transaction.Notes}");
            }

            return report.ToString();
        }

        public virtual void PerformMonthEndTransactions() { }
    }
}
