using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GwpApi
{
    public interface IGwpService
    {
        Task<Dictionary<string, decimal>> GetAverageGwp(GwpRequestModel request);
    }

    public class GwpService : IGwpService
    {
        private readonly IDataStorage _dataStorage;

        public GwpService(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        public async Task<Dictionary<string, decimal>> GetAverageGwp(GwpRequestModel request)
        {
            var data = await _dataStorage.GetDataAsync();

            var result = new Dictionary<string, decimal>();

            foreach (var lob in request.Lob)
            {
                var filteredData = data
                    .Where(d => d.CountryCode.ToLower() == request.Country.ToLower() && d.LineOfBusiness.ToLower() == lob.ToLower())
                    .Select(d => d.Gwp)
                    .ToList();

                if (filteredData.Any())
                {
                    result.Add(lob, filteredData.Average());
                }
            }

            return result;
        }
    }
}
