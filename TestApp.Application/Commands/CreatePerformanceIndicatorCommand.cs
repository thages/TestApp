using TestApp.Application.Interfaces;
using TestApp.Domain.Enums;
using TestApp.Domain.DataTransferObjects;

namespace TestApp.Application.Commands
{
    public class CreatePerformanceIndicatorCommand : IApplicationCommand<PerformanceIndicatorItem>
    {
        public CreatePerformanceIndicatorCommand(string name, IndicatorType indicatorType, DateTimeOffset date, decimal value)
        {
            Name = name;
            IndicatorType = indicatorType;
            Date = date;
            Value = value;
        }

        public string Name { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public DateTimeOffset Date { get; private set; }
        public decimal Value { get; private set; }
    }
}
