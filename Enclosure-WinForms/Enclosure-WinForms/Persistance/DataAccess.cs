using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enclosure_WinForms.Persistance
{
    internal class DataAccess : IDataAccess
    {
        public async Task SaveAsync(String fileName, List<int> data)
        {
            using (StreamWriter streamWriter = new(fileName))
            {
                foreach (int value in data)
                {
                    await streamWriter.WriteLineAsync(value.ToString());
                }
            }
        }
        public async Task<List<int>> LoadAsync(String fileName) {
            using (StreamReader streamReader = new(fileName))
            {
                List<int> data = new();
                while (!streamReader.EndOfStream)
                {
                    data.Add(int.Parse(await streamReader.ReadLineAsync()));
                }
                return data;
            }
        }
    }
}
