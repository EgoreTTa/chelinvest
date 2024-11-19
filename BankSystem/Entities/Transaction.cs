namespace BankSystem.Entities
{
    using System;

    public class Transaction
    {
        private int _idAccountFrom;
        private int _idAccountTo;
        private DateTime _created;
        private double _amount;

        public int IdAccountFrom => _idAccountFrom;
        public int IdAccountTo => _idAccountTo;
        public DateTime Created => _created;
        public double Amount => _amount;

        public Transaction(int idAccountFrom, int idAccountTo, DateTime created, double amount)
        {
            _idAccountFrom = idAccountFrom;
            _idAccountTo = idAccountTo;
            _created = created;
            _amount = amount;
        }
    }
}