using DataBase.Models;
using EPSA.Objects.Models;
using EPSA.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PruebaTecnica.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HistoricalController : Controller
    {
        private readonly ILogger<HistoricalController> _logger;
        private readonly IHistoricalService _historicalService;

        public HistoricalController(ILogger<HistoricalController> logger, IHistoricalService historicalService)
        {
            _logger = logger;
            _historicalService = historicalService;
        }

        [HttpGet("HistoricalConsumption/{startDate}/{endDate}")]
        public Task<IEnumerable<Consumption>> GetHistoricalConsumptionByDate(string startDate, string endDate)
        {
            var startDateData = Convert.ToDateTime(startDate);
            var endDateData = Convert.ToDateTime(endDate);
            return _historicalService.GetHistoricalConsumptionAsync(startDateData, endDateData);
        }

        [HttpGet("HistoricalConsumptionByUser/{startDate}/{endDate}")]
        public Task<IEnumerable<HistoricalConsumption>> GetHistoricalConsumptionByUser(string startDate, string endDate)
        {
            var startDateData = Convert.ToDateTime(startDate);
            var endDateData = Convert.ToDateTime(endDate);
            return _historicalService.GetHistoricalConsumptionByUserAsync(startDateData, endDateData);
        }


        [HttpGet("GetTopLosses/{startDate}/{endDate}")]
        public Task<IEnumerable<Losses>> GetTopLosses(string startDate, string endDate)
        {
            var startDateData = Convert.ToDateTime(startDate);
            var endDateData = Convert.ToDateTime(endDate);
            return _historicalService.GetTopLosses(startDateData, endDateData);
        }

    }
}
