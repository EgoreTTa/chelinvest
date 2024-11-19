using System.Linq;

namespace BankSystem.Entities
{
    using System;
    using System.Collections.ObjectModel;

    public class Bank
    {
        private int _id;
        private string _name;
        private Account[] _accounts = Array.Empty<Account>();
        private Transaction[] _transactions = Array.Empty<Transaction>();

        public int Id => _id;
        public string Name => _name;
        public ReadOnlyCollection<Account> Accounts => Array.AsReadOnly(_accounts);
        public ReadOnlyCollection<Transaction> Transactions => Array.AsReadOnly(_transactions);

        public Bank(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public void CreateAccount(string name, double balance)
        {
            var nextId = Accounts.Select(x => x.Id)
                                 .Distinct()
                                 .OrderBy(x => x)
                                 .LastOrDefault() + 1;

            _accounts = _accounts.Append(new Account(nextId, name, balance))
                                 .ToArray();
        }

        public void NewTransactions(Account from, Account to, double amount)
        {
            from.WriteOff(amount);//todo
            to.Enrollment(amount);

            _transactions = _transactions.Append(new Transaction(from.Id, to.Id, DateTime.Now, amount))
                                         .ToArray();
        }
    }
}