using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            AsyncLocal<int> value = new AsyncLocal<int>();
            for (int i = 0; i < 100; i++)
            {
                value.Value = i;
                CustomThreadPool.QueueThreadWorkItem(() =>
                {
                    Console.WriteLine(value.Value);
                    Thread.Sleep(1000);
                });
            }
        }

        /// <summary>
        /// Second test to see if task.run and task.wait functionality has been added properly
        /// </summary>
        public static void Test2()
        {
            AsyncLocal<int> value = new AsyncLocal<int>();
            List<CustomTask> tasks = new List<CustomTask>();

            for (int i = 0; i < 100; i++)
            {
                value.Value = i;
                tasks.Add(CustomTask.Run(() =>
                {
                    Console.WriteLine(value.Value);
                    Thread.Sleep(1000);
                }));
            }

            foreach (var t in tasks) {
                Console.WriteLine("Waiting");
                t.Wait();
            }
        }

        /// <summary>
        /// Third test to test the when all functionality.
        /// </summary>
        public static void Test3()
        {
            AsyncLocal<int> value = new AsyncLocal<int>();
            List<CustomTask> tasks = new List<CustomTask>();

            for (int i = 0; i < 100; i++)
            {
                value.Value = i;
                tasks.Add(CustomTask.Run(() =>
                {
                    Console.WriteLine(value.Value);
                    Thread.Sleep(1000);
                }));
            }

            CustomTask.WhenAll(tasks).Wait();
        }

        /// <summary>
        /// 4th test to test delay functionality.
        /// </summary>
        public static void Test4()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                CustomTask.Delay(1000).Wait();
            }
        }

        /// <summary>
        /// 5th test to add compostability to ContinueWith;
        /// </summary>
        public static void Test5()
        {
            Console.Write("Hello, ");
            CustomTask.Delay(2000).ContinueWith(() =>
            {
            }).ContinueWith(() =>
            {
                Console.Write("World!");
            }).Wait();
        }

        /// <summary>
        /// Testing the functionality of looping Task.Delays by implementing an iterator.
        /// </summary>
        public static void Test6()
        {
            CustomTask.Iterate(PrintAsync()).Wait();
            static IEnumerable<CustomTask> PrintAsync()
            {
                for (int i = 0; ; i++)
                {
                    yield return CustomTask.Delay(1000);
                    Console.WriteLine(i);
                }
            }
        }
    }
}
