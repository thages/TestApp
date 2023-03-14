using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain.Enums;

namespace TestApp.Domain.DataTransferObjects
{
    public class PerformanceIndicatorBySumDetails
    {
        public PerformanceIndicatorBySumDetails
            (
                string name, 
                IndicatorType indicatorType, 
                List<SumIndicatorDetails>? sumIndicatorList, 
                decimal totalValue
            )
        {
            Name = name;
            IndicatorType = indicatorType;
            SumIndicatorList = sumIndicatorList;
            TotalValue = totalValue;
        }

        public string Name { get; set; }
        public IndicatorType IndicatorType { get; set; }
        public List<SumIndicatorDetails>? SumIndicatorList { get; set; }
        public decimal TotalValue { get; set; }
    }
}
