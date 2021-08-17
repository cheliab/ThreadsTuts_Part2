using System;
using Threading_Albahari_Part1.ThreadPoolExamples;

namespace Threading_Albahari_Part1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Thread_01_XY_Example.Start();
            // Thread_02_MemoryStack.Start();
            // Thread_03_ShareData.Start();
            // Thread_04_ShareStaticField.Start();
            // Thread_05_BrokeShareData.Start();
            // Thread_06_LockShareStaticData.Start();
            // Thread_07_JoinSleep.Start();
            // Thread_08_Delegate_ThreadStart.Start();
            // Thread_10_LambdaExp_CapturedVar.Start_forLoopExample();
            // Thread_10_LambdaExp_CapturedVar.Start_StringExample();
            // Thread_10_LambdaExp_CapturedVar.Start_TempVarSolution();
            // Thread_11_NamingThreads.Start();
            // Thread_12_ForegroundBackgroundThreads.Start();
            // Thread_14_ExceptionHandling.Start();
            
            // ThreadPool_01_EnteringTheThreadPool.Start();
            // ThreadPool_02_GenericTask.Start();
            ThreadPool_03_WithoutTPL_QueueUserWorkItem.Start();
            
            Console.ReadLine();
        }
    }
}