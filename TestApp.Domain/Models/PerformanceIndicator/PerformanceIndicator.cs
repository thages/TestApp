using TestApp.Domain.Enums;
using TestApp.Domain.Models.Common;

namespace TestApp.Domain.Models.PerformanceIndicator
{
    public class PerformanceIndicator : EntityModel
    {
        public PerformanceIndicator(string name, IndicatorType indicatorType)
        {
            Name = name;
            IndicatorType = indicatorType;
        }

        public string Name { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        
    }
}
