using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Models.Common;

namespace TestApp.Domain.Models.DailyIndicator
{
    public class AverageIndicator : EntityModel
    {
        public AverageIndicator(Guid performanceIndicatorId, DateTimeOffset date, decimal value)
        {
            PerformanceIndicatorId = performanceIndicatorId;
            Date = date;
            Value = value;
        }

        public Guid PerformanceIndicatorId { get; private set; }
        public DateTimeOffset Date { get; private set; }
        public decimal Value { get; private set; }
    }
}
