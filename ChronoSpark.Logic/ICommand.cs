﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    public interface ICommand
    {
        bool SetEntity(IRavenEntity receivedEntity);
        bool Execute();
    } 
}