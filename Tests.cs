using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait_From_Scratch
{
    /// <summary>
    /// Class that contains a number of tests to test custom async functionality.
    /// </summary>
    public static class Tests
    {
        /// <summary>
        /// The first test is to test the queuing of work in the thread pool and
        /// make sure the execution context is kept.
        /// </summary>
        public static void Test1()
        {
            for (int i = 0; i < 10; i++)
            {
                CustomThreadPool.QueueThreadWorkItem(() =>
                {
                    Console.WriteLine(i);
                    Thread.Sleep(1000);
                });
            }
        }

        //SAMPLE CODE THAT NEEDS TO RUN 
        //for (int i = 0; ; i++) #4
        //{
        //    MyTask.Delay(1000)
        //    Console.WriteLine(i)
        //}
        // tip - needs an iterate method that uses an IEnumerable<myTask>

        //Console.Write("Hello, "); #3
        //MyTask.Delay(2000).ContinueWith(delegate
        //{
        //    Console.Write("World!");
        //    return MyTask.Delay(2000);
        //}).ConiuteWith(delegate
        //{
        //    Console.Write(" How are you?")
        //}).Wait();
        // Project should not exit utni lhow are you is printed

        //List<MyTask> tasks = new() #2
        //for (int i = 0; i < 100; i++)
        //{
        //tasks.Add(MyTask.run(delegate
        //{
        //    Console.WriteLine(i);
        //    Thread.Sleep(1000);
        //})
        //}
        // MyTask.WhenAll(tasks).Wait()
        // This code won't work initally 

        //for (int i = 0; i < 1000; i++) #1
        //{ 
        //    //Queue Thread Work (() => {
        //    //console.writeLine(i);
        //    //thead.sleep()
        //    //});
        //}
        //Won't work initially
    }
}
