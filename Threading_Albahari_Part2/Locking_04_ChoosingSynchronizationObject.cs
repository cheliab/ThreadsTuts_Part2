using System.Collections.Generic;

namespace Threading_Albahari_Part2
{
    /// <summary>
    /// Варианты объектов синхронизации
    /// </summary>
    public class Locking_04_ChoosingSynchronizationObject
    {
        private List<string> _list = new List<string>();
        
        public void LockVariants()
        {
            // Для объекта синхронизации можно использовать любой ссылочный тип
            lock(_list) {}
            
            // Также можно использовать текущий объект 
            lock(this) {}
            
            // или тип
            lock(typeof(Locking_04_ChoosingSynchronizationObject)) {}
            
            // Проблемы при использовании типа или this
            
            // Недостатком такой блокировки является то,
            // что вы не инкапсулируете логику блокировки,
            // поэтому становится сложнее предотвратить взаимную блокировку и чрезмерную блокировку.
            // Блокировка типа также может проникать через границы домена приложения (в рамках того же процесса).
        }
    }
}