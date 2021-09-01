using System.Diagnostics;
using System.Threading;

namespace Threading_Albahari_Part1
{
    /// <summary>
    /// Приоритеты потоков
    ///
    /// Приоритет потока поределяет сколько процессорного времени ему будет выделено, относительно других потоков
    /// Чем выше приоритет, тем больше времени процессор будет отдавать потоку
    /// </summary>
    public class Thread_13_ThreadPriority
    {
        public static void Start()
        {
            // у потоков 5 вариантов приоритетов
            // enum ThreadPriority { Lowest, BelowNormal, Normal, AboveNormal, Highest }
            
            Thread mainThread = Thread.CurrentThread;

            mainThread.Priority = ThreadPriority.Highest;

            // У процесса уже 6 варинтов приоритетов
            // enum ProcessPriorityClass { Idle, BelowNormal, Normal, AboveNormal, High, Realtime }
            using (Process p = Process.GetCurrentProcess())
            {
                p.PriorityClass = ProcessPriorityClass.High;
            }
            
            // Realtime означает что поток не отдаст никому время для работы, пока полностью не выполнится
            // Обычно разработчики работают максимум с High

            // Таблица расчета приоритета для потока (VIA CLR)
            
            //               Idle BelowNormal Normal AboveNormal High Realtime
            // Time-Critical 15   15          15     15          15   31
            // Highest       6    8           10     12          15   26
            // Above Normal  5    7           9      11          14   25
            // Normal        4    6           8      10          13   24
            // Below Normal  3    5           7      9           12   23
            // Lowest        2    4           6      8           11   22
            // Idle          1    1           1      1           1    16
        }
    }
}