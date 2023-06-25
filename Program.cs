using BankAccount.Classes;

namespace BankAccount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var giftCard = new GiftCardAccount("Gift Card", 100, 50);
            giftCard.MakeWithdrawal(20, DateTime.Now, "Compre un cafe");
            giftCard.MakeWithdrawal(50, DateTime.Now, "Compre un perro donde el gordo");
            giftCard.PerformMonthEndTransactions();
            giftCard.MakeDeposit(20.50m, DateTime.Now, "Deposito a mi cuenta");

            Console.WriteLine("Gift Card");
            Console.WriteLine(giftCard.GetAccountHistory());

            var savings = new InterestEarningAccount("Guardando Cuenta", 10000);
            savings.MakeDeposit(750, DateTime.Now, "Pago de nomina");
            savings.MakeDeposit(1250, DateTime.Now, "Guardando ahorros");
            savings.MakeWithdrawal(250, DateTime.Now, "Gastando algo de dinero");
            savings.PerformMonthEndTransactions();

            Console.WriteLine("Cuenta que acomula intereses");
            Console.WriteLine(savings.GetAccountHistory());

            var lineOfCreditAccount = new LineOfCreditAccount("Tarjeta De Credito", 0, 2000);
            lineOfCreditAccount.MakeWithdrawal(1000m, DateTime.Now, "Uso de mi tarjeta");
            lineOfCreditAccount.MakeDeposit(50m, DateTime.Now, "Primer pago");
            lineOfCreditAccount.MakeWithdrawal(5000m, DateTime.Now, "Compra de comida");
            lineOfCreditAccount.MakeDeposit(150m, DateTime.Now, "Segundo pago");
            lineOfCreditAccount.PerformMonthEndTransactions();

            Console.WriteLine("Cuenta de Credito");
            Console.WriteLine(lineOfCreditAccount.GetAccountHistory());
        }

        public static void sessionOne()
        {
            BankAccountClass accountBank = new BankAccountClass("Diego Hinagas", 200.5m);
            Console.WriteLine($" Su cuenta {accountBank.Number} ha sido creada para {accountBank.Owner} con balance inicial de {accountBank.Balance}");

            accountBank.MakeDeposit(100, DateTime.Now, "Recarga");
            Console.WriteLine(accountBank.Balance);

            accountBank.MakeWithdrawal(150.1m, DateTime.Now, "Retiro");
            Console.WriteLine(accountBank.Balance);

            Console.WriteLine(accountBank.GetAccountHistory());

            BankAccountClass invalidAccount;
            try
            {
                invalidAccount = new BankAccountClass("Invalid", -222);
            }
            catch (ArgumentOutOfRangeException error)
            {
                Console.WriteLine("Ha ocurrido un error al crear una nueva cuenta");
                Console.WriteLine(error.ToString());
            }

            try
            {
                accountBank.MakeWithdrawal(1000, DateTime.Now, "Retiro de mucho dinero que no tengo");
            }
            catch (InvalidOperationException error)
            {
                Console.WriteLine("Hubo un error al retirar el dinero");
                Console.WriteLine(error.ToString());
            }
        }
    }
}