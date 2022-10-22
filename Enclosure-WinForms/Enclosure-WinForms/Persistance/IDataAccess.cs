﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enclosure_WinForms.Persistance
{
    internal interface IDataAccess
    {
        public void SaveAsync(String fileName);
        public void LoadAsync();
    }
}
