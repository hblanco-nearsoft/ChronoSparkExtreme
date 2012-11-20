using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    public class Timers
    {

        DateTime thetime = DateTime.Now;
        
        public void testingthreads()
        {

            for(int x =0; x < 12 ; x++) {
                Console.WriteLine("wait for it...");
                System.Threading.Thread.Sleep(5000);
            }
            Console.WriteLine("IT IS TIME!");
        }
        
    }
}
