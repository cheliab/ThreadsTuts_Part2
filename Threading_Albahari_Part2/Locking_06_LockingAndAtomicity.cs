using System;

namespace Threading_Albahari_Part2
{
    // Блокировка и атомарность
    //
    // Если группа переменных всегда считывается и записывается в пределах одной блокировки,
    // можно сказать, что переменные считываются и записываются атомарно
    
    public class Locking_06_LockingAndAtomicity
    {
        private static object _locker = new object();
        
        // Если поля x и y всегда читаются и назначаются в lock объекте _locker
        // то можно сказать, что x и y доступны атомарно
        private static int _x = 1, _y = 1; 
        
        private void Divide()
        {
            // (кривой перевод)
            // Заблокированный блок кода не может быть разделен или вытеснен действиями другого потока.
            // таким образом, что она будет меняться x или y и влечет за собой отмену результатов.
            // Вы никогда не получите ошибку деления на ноль, обеспечивая x и y всегда доступны в этом же монопольную блокировку.
            
            lock (_locker)
            {
                if (_x != 0)
                    _y /= _x;
            }
        }
    }

    // !!!
    // Атомарность, обеспечиваемая блокировкой, нарушается, если в lock блоке возникает исключение.
    // !!!
    class BankAccount
    {
        private object _locker = new object();
        private decimal _savingBalance, _checkBalance;

        void Transfer(decimal amount)
        {
            lock (_locker)
            {
                _savingBalance += amount;
                _checkBalance -= amount + GetBankFee();
                
                // Если GetBankFee() возникнет исключение, банк потеряет деньги.
                // В этом случае мы могли бы избежать проблемы, вызваа GetBankFee раньше.
                // Решением для более сложных случаев является реализация логики «отката» в блоке catch or finally.
            }
        }
        
        int GetBankFee()
        {
            throw new Exception();
            return 0;
        }
    }
}