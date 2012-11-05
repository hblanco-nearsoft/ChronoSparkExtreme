using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public interface ICommand
    {
        IRavenEntity ItemToWork { get; set; }   
        bool Execute();
    } 
}