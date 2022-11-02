using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enclosure_WinForms.Persistance
{
    internal interface IDataAccess
    {
        public Task SaveAsync(String fileName, List<int> data);
        public Task<List<int>> LoadAsync(String fileName);
    }
}
