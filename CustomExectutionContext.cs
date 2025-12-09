using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait_From_Scratch
{
    public class CustomExectutionContext
    {
        public static ThreadLocal<Dictionary<string, object>> ThreadLocal = new ThreadLocal<Dictionary<string, object>>();
    }
}
