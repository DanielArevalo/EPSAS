using DataBase.Models;
using EPSA.Objects.Models;

namespace EPSA.Services.Interfaces
{
    public interface IHistoricalService
    {
        Task<IEnumerable<Consumption>> GetHistoricalConsumptionAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<HistoricalConsumption>> GetHistoricalConsumptionByUserAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<Losses>> GetTopLosses(DateTime startDate, DateTime endDate);

    }
}
