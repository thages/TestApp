using TestApp.Domain.Models.DailyIndicator;
using TestApp.Domain.Repositories.Common;

namespace TestApp.Domain.Repositories
{
    public interface IAverageIndicatorRepository : ICommonRepository<AverageIndicator> 
    {
        List<AverageIndicator> GetByPerformanceIndicatorId(Guid performanceIndicatorId);
    }
}
