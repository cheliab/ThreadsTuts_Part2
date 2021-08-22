using System.Threading;

namespace Threading_Albahari_Part2
{
    /// <summary>
    /// Deadlock (Взаимоблокировка потоков)
    ///
    /// Взаимоблокировка возникает, когда каждый из двух потоков ожидает ресурса, удерживаемого другим, поэтому ни один из них не может продолжить работу.
    /// </summary>
    public class Locking_08_Deadlocks
    {
        // В такой ситуации должно присутствовать более одного объекта блокировки
        private static object _locker1 = new object();
        private static object _locker2 = new object();

        public static void Start()
        {
            // Вспомогательный поток сначала блокирует _locker1 и потом ждет _locker2
            new Thread(() =>
            {
                lock (_locker1)
                {
                    Thread.Sleep(1000);
                    lock (_locker2); // Deadlock
                }
            }).Start();

            // Основной поток наоборот сначала блокирует _locker2 и потом ждет _locker1
            lock (_locker2)
            {
                Thread.Sleep(1000);
                lock (_locker1); // Deadlock
            }
            
            // Теперь оба потока ожидают пока освободится объект блокировки заблокированный в другом потоке
        }
    }
}