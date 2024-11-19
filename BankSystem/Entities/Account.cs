namespace BankSystem.Entities
{
    public class Account
    {
        private int _id;
        private string _name;
        private double _balance;

        public int Id => _id;
        public string Name => _name;
        public double Balance => _balance;

        public Account(int id, string name, double balance)
        {
            _id = id;
            _name = name;
            _balance = balance;
        }

        public void WriteOff(double amount) //todo
        {
            _balance -= amount;
        }

        public void Enrollment(double amount)
        {
            _balance += amount;
        }

        public override string ToString() => _name;
    }
}