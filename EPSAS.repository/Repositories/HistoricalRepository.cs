using DataBase;
using DataBase.Models;
using EPSA.Objects.Models;
using EPSAS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EPSAS.Repository.Repositories
{
    public class HistoricalRepository : IHistoricalRepository
    {
        private EPSAContext ePSContext;

        public HistoricalRepository(EPSAContext context)
        {
            ePSContext = context;
        }

        public async Task<IEnumerable<Consumption>> GetHistoricalConsumptionAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = ePSContext)
            {
                return await context.Consumptions.Where(x => x.Date >= startDate && x.Date <= endDate).OrderBy(x => x.Line).ToListAsync();
            }
        }

        public async Task<IEnumerable<HistoricalConsumption>> GetHistoricalConsumptionByUserAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = ePSContext)
            {
                var query = from c in context.Consumptions
                            join co in context.Costs on new { c.Line, c.Date } equals new { co.Line, co.Date } into coGroup
                            from co in coGroup.DefaultIfEmpty()
                            join l in context.Losses on new { c.Line, c.Date } equals new { l.Line, l.Date } into lGroup
                            from l in lGroup.DefaultIfEmpty()
                            where c.Date >= startDate && c.Date <= endDate
                            select new HistoricalConsumption
                            {
                                Line = c.Line,
                                Date = c.Date,
                                Residential_Consumption = c.Residential_Wh,
                                Commercial_Consumption = c.Commercial_Wh,
                                Industrial_Consumption = c.Industrial_Wh,
                                Residential_Losses = l.Residential_Percentage,
                                Commercial_Losses = l.Commercial_Percentage,
                                Industrial_Losses = l.Industrial_Percentage,
                                Residential_CostToConsumption = co.Residential_Wh * c.Residential_Wh,
                                Commercial_CostToConsumption = co.Commercial_Wh * c.Commercial_Wh,
                                Industrial_CostToConsumption = co.Industrial_Wh * c.Industrial_Wh
                            };

                return await query.ToListAsync();
            }
        }
        public async Task<IEnumerable<Losses>> GetTopLosses(DateTime startDate, DateTime endDate)
        {
            using (var context = ePSContext)
            {
                var topLosses = await context.Losses
                .Where(loss => loss.Date >= startDate && loss.Date <= endDate)
                .OrderBy(loss => loss.Residential_Percentage)
                .ThenBy(loss => loss.Commercial_Percentage)
                .ThenByDescending(loss => loss.Industrial_Percentage)
                .Take(20)
                .ToListAsync();

                return topLosses;
            }
        }
    }
}
