using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace GwpApi
{
    public interface IDataStorage
    {
        Task<List<GwpDataModel>> GetDataAsync();
    }

    public class InMemoryDataStorage : IDataStorage
    {
        private readonly List<GwpDataModel> _data;

        public InMemoryDataStorage()
        {
            _data = LoadData();
        }

        public async Task<List<GwpDataModel>> GetDataAsync()
        {
            // Simulating an asynchronous operation
            await Task.Delay(0);
            return _data;
        }

        private List<GwpDataModel> LoadData()
        {
            // Load CSV data into memory
            using (var reader = new StreamReader("Data/gwpByCountry.csv"))
            using (var csv = new CsvReader(reader))
            {
                return csv.GetRecords<GwpDataModel>().ToList();
            }
        }
    }
}
