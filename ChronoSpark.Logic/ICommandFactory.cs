using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    interface ICommandFactory
    {
        String CommandName { get; }
        String CommandDescription { get; }

        //ICommand MakeCommand(String[] Arguments);
    }
}
