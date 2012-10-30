using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data; 

namespace ChronoSpark.Logic
{
    class CommandDeterminator
    {
        public ICommand GetCommand(String receivedCommand, IRepository receivedRepository) 
        {
            switch (receivedCommand)
            { 
                case "add":
                    return new AddItemCmd(receivedRepository);
                case "delete":
                    return new DeleteItemCmd(receivedRepository);
                case "update":
                    return new UpdateItemCmd(receivedRepository);
                case "getbyid":
                    return new GetByIdCmd(receivedRepository);
                default:
                    Console.WriteLine(receivedCommand + " is not an identified command");
                    return null;
            }
        }
    }
}
