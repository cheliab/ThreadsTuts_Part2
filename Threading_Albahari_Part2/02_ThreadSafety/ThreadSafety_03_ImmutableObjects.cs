using System;

namespace Threading_Albahari_Part2
{
    // Immutable Objects (Неизменяемые объекты)
    
    // Неизменяемый объект - это объект, состояние которого нельзя изменить ни внешне, ни внутренне.
    // Поля неизменяемого объекта обычно объявляются доступными только для чтения и полностью инициализируются во время построения.

    // Неизменность является отличительной чертой функционального программирования - где вместо мутироваания(изменения) объекта,
    // вы создаете новый объект с другими значениями свойств.
    // LINQ следует этой парадигме.
    // Неизменяемость также ценна в многопоточности тем,
    // что позволяет избежать проблемы разделяемого записываемого состояния -
    // за счет исключения (или минимизации) записываемого.

    // Один из шаблонов - использовать неизменяемые объекты для инкапсуляции группы связанных полей,
    // чтобы минимизировать длительность блокировки.
    // В качестве очень простого примера предположим, что у нас есть два следующих поля:
    
    // int _percentComplete; 
    // string _statusMessage;
    
    // и мы хотели читать / записывать их атомарно.
    //
    // Вместо того, чтобы блокировать эти поля, мы могли бы определить следующий неизменяемый класс:
    
    public class ProgressStatus
    {
        // Представляет прогресс некоторой активности
        public readonly int PercentComplete;
        public readonly string StatusMessage;
        
        // Этот класс может иметь гораздо больше полей ..

        public ProgressStatus(int percentComplete, string statusMessage)
        {
            PercentComplete = percentComplete;
            StatusMessage = statusMessage;
        }
    }

    public class ThreadSafety_03_ImmutableObjects
    {
        // Определяе поле этого типа вместе с объектом блокировки:
        private static readonly object _statusLocker = new object();
        private static ProgressStatus _status;

        public static void Start()
        {
            // Теперь мы можем читать / записывать значения этого типа, не удерживая блокировку более чем для одного присваивания:
            var status = new ProgressStatus(50, "Все оки!");

            lock (_statusLocker)
                _status = status; // быстрая блокировака (обновляем выполненный прогресс)

            // ...
            
            // Чтобы прочитать объект, мы сначала получаем копию объекта (внутри блокировки).
            // Тогда мы можем читать его значения без необходимости удерживать блокировку:
            
            ProgressStatus statusCopy;
            lock (_statusLocker)
                statusCopy = _status; // еще одна быстрая блокировка

            // получаем данные свойств
            int pc = statusCopy.PercentComplete;
            string msg = statusCopy.StatusMessage;
            
            Console.WriteLine($"Persent complete {pc}"); // Persent complete 50
            Console.WriteLine($"Message {msg}"); // Message Все оки!
        }
    }
}