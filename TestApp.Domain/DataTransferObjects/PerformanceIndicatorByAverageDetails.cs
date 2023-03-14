using TestApp.Domain.Enums;

namespace TestApp.Domain.DataTransferObjects
{
    public class PerformanceIndicatorByAverageDetails
    {
        public PerformanceIndicatorByAverageDetails
            (
                string name, 
                IndicatorType indicatorType, 
                List<AverageIndicatorDetails>? averageIndicatorList, 
                decimal totalValue
            )
        {
            Name = name;
            IndicatorType = indicatorType;
            AverageIndicatorList = averageIndicatorList;
            TotalValue = totalValue;
        }

        public string Name { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public List<AverageIndicatorDetails>? AverageIndicatorList { get; set; }
        public decimal TotalValue { get; set; }
    }
}
