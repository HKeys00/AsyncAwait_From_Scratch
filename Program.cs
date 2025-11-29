// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//MyThreadPool - Look at performance, implement non fixed number of threads, thread management - decreasing and increasing the number of threads


//Class MyThreadPool 
// - Queue work items
// - Create and start threads
// - Implement Execution context with AsyncLocal see if you can't implement your own ExecutionContext instead of using the base class.

// - Implement class MyTask instead of using Task<T>
// - MyTask needs to know if it's completed and set that it's completed
// - It needs to be able to set any exceptions that were thrown
// - It needs to synchronously block and wait for a task to complete
// - It needs to schedule a continuation to run after the task is complete (bonus if you can chain tasks together)
// - Needs to implement Task.Run() and Task.WhenAll
// - Bonus make it generic
// - Task needs to be thread safe (why is lock(this) bad?)
// - ManualResetEventSlim
// - Can you await custom MyTask class.


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