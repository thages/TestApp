using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Enums;

namespace TestApp.Domain.DataTransferObjects
{
    public class PerformanceIndicatorItem
    {
        public PerformanceIndicatorItem(Guid id, string name, IndicatorType indicatorType)
        {
            Id = id;
            Name = name;
            IndicatorType = indicatorType;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
    }
}
