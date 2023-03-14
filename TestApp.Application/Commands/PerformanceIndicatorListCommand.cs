using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Application.Interfaces;
using TestApp.Domain.DataTransferObjects;

namespace TestApp.Application.Commands
{
    public class PerformanceIndicatorListCommand : IApplicationCommand<List<PerformanceIndicatorItem>> { }
}
