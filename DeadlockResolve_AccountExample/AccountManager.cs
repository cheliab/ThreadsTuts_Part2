using System.Threading;

namespace DeadlockResolve_AccountExample
{
    public class AccountManager
    {
        private Account _fromAccount;
        private Account _toAccount;
        private double _amountToTransfer;

        public AccountManager(Account fromAccount, Account toAccount, double amountToTransfer)
        {
            _fromAccount = fromAccount;
            _toAccount = toAccount;
            _amountToTransfer = amountToTransfer;
        }

        /// <summary>
        /// Перевод денег с одного счета на другой
        ///
        /// тут возможен дедлок
        /// </summary>
        public void Transfer()
        {
            object lock1, lock2;

            // Тут мы смотрим на идентификаторы аккаунтов
            // и блокируем их по порядку их идентификаторов
            // это заставит второй поток дождаться освобождения аккаунта с меньшим идентификатором
            // перед тем как выполнить свою операцию
            if (_fromAccount.ID < _toAccount.ID)
            {
                lock1 = _fromAccount;
                lock2 = _toAccount;
            }
            else
            {
                lock1 = _toAccount;
                lock2 = _fromAccount;
            }
            
            lock (_fromAccount)
            {
                Thread.Sleep(1000);
                lock (_toAccount) // тут дедлок
                {
                    _fromAccount.Withdraw(_amountToTransfer);
                    _toAccount.Deposit(_amountToTransfer);
                }
            }
        }
    }
}