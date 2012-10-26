using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    public class SparkInvoker
    {
        ICommand TheCommand;

        public SparkInvoker(ICommand newCommand) 
        {
            TheCommand = newCommand;         
        }

        public bool Invoke() 
        {
            TheCommand.Execute();
            return true;
        }

    }
}
