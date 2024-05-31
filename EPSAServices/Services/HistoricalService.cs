using DataBase.Models;
using EPSA.Objects.Models;
using EPSA.Services.Interfaces;
using EPSAS.Repository.Interfaces;

namespace EPSA.Services.Services
{
    public class HistoricalService : IHistoricalService
    {

        public IHistoricalRepository HistoricalRepository { get; }

        public HistoricalService(IHistoricalRepository historicalRepository)
        {
            HistoricalRepository = historicalRepository;
        }

        public Task<IEnumerable<Consumption>> GetHistoricalConsumptionAsync(DateTime startDate, DateTime endDate)
        {
            return HistoricalRepository.GetHistoricalConsumptionAsync(startDate, endDate);
        }

        public Task<IEnumerable<HistoricalConsumption>> GetHistoricalConsumptionByUserAsync(DateTime startDate, DateTime endDate)
        {
            return HistoricalRepository.GetHistoricalConsumptionByUserAsync(startDate, endDate);
        }

        public Task<IEnumerable<Losses>> GetTopLosses(DateTime startDate, DateTime endDate)
        {
            return HistoricalRepository.GetTopLosses(startDate, endDate);
        }

    }
}
