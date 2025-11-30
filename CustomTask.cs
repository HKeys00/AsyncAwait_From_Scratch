using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait_From_Scratch
{

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


    internal class CustomTask
    {
    }
}
