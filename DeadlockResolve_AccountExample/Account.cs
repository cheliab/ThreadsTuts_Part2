namespace DeadlockResolve_AccountExample
{
    public class Account
    {
        private int _id;
        private double _balance;

        public Account(int id, double balance)
        {
            _id = id;
            _balance = balance;
        }

        public int ID
        {
            get { return _id; }
        }

        public void Withdraw(double amount)
        {
            _balance -= amount;
        }

        public void Deposit(double amount)
        {
            _balance += amount;
        }
    }
}