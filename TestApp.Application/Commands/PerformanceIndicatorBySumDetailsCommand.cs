using TestApp.Application.Interfaces;
using TestApp.Domain.DataTransferObjects;

namespace TestApp.Application.Commands
{
    public class PerformanceIndicatorBySumDetailsCommand : IApplicationCommand<PerformanceIndicatorBySumDetails>
    {
        public PerformanceIndicatorBySumDetailsCommand(Guid id)
        {
            PerformanceIndicatorId = id;
        }

        public Guid PerformanceIndicatorId { get; private set; }
    }
}
