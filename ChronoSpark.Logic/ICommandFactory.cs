using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    interface ICommandFactory
    {
        String CommandName { get; set; }
        String CommandDescription { get; set; }

        //ICommand MakeCommand(String[] Arguments);
    }
}
