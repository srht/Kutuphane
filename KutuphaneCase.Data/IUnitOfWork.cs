﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutuphaneCase.Data
{
    public interface IUnitOfWork:IDisposable
    {
        public Task Save();
    }
}
