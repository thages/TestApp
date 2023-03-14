using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models.DailyIndicator;
using TestApp.Domain.Repositories.Common;

namespace TestApp.Domain.Repositories
{
    public interface ISumIndicatorRepository :  ICommonRepository<SumIndicator> 
    {
        List<SumIndicator> GetByPerformanceIndicatorId(Guid performanceIndicatorId);
    }
}
