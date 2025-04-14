﻿using REPO.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO.Core.Contract
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<Region> Region { get; }
        Task CompleteAsync();
    }
}
