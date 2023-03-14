using TestApp.Application.Commands;
using TestApp.Application.Interfaces;
using TestApp.Domain.DataTransferObjects;
using TestApp.Domain.Enums;
using TestApp.Domain.Models.PerformanceIndicator;
using TestApp.Domain.Repositories;

namespace TestApp.Application.Handlers
{
    public class PerformanceIndicatorCommandHandler :
        IApplicationCommandHandler<CreatePerformanceIndicatorCommand, PerformanceIndicatorItem>,
        IApplicationCommandHandler<PerformanceIndicatorListCommand, List<PerformanceIndicatorItem>>,
        IApplicationCommandHandler<PerformanceIndicatorByAverageDetailsCommand, PerformanceIndicatorByAverageDetails>,
        IApplicationCommandHandler<PerformanceIndicatorBySumDetailsCommand, PerformanceIndicatorBySumDetails>
    {
        private readonly IPerformanceIndicatorRepository _performanceIndicatorRepository;
        private readonly IAverageIndicatorRepository _averageIndicatorRepository;
        private readonly ISumIndicatorRepository _sumIndicatorRepository;

        public PerformanceIndicatorCommandHandler(IPerformanceIndicatorRepository performanceIndicatorRepository, IAverageIndicatorRepository averageIndicatorRepository, ISumIndicatorRepository sumIndicatorRepository)
        {
            _performanceIndicatorRepository = performanceIndicatorRepository;
            _averageIndicatorRepository = averageIndicatorRepository;
            _sumIndicatorRepository = sumIndicatorRepository;
        }

        public Task<PerformanceIndicatorItem> Handle(CreatePerformanceIndicatorCommand request, CancellationToken cancellationToken)
        {
            var performanceIndicator = new PerformanceIndicator(request.Name, request.IndicatorType);
            _performanceIndicatorRepository.Add(performanceIndicator);
            
            PerformanceIndicatorItem response = new(
                    id: performanceIndicator.Id,
                    name: performanceIndicator.Name,
                    indicatorType: performanceIndicator.IndicatorType
            );
            
            return Task.FromResult(response);
        }

        public async Task<List<PerformanceIndicatorItem>> Handle(PerformanceIndicatorListCommand command, CancellationToken cancellationToken)
        {
            var indicators = await _performanceIndicatorRepository.GetAll();
            List<PerformanceIndicatorItem> response = new();
            
            foreach(var indicator in indicators)
            {
                var item = new PerformanceIndicatorItem(
                    id: indicator.Id,
                    name: indicator.Name,
                    indicatorType: indicator.IndicatorType
                    );

                response.Add(item);
            }

            return response;
        }

        public Task<PerformanceIndicatorByAverageDetails> Handle(PerformanceIndicatorByAverageDetailsCommand command, CancellationToken cancellationToken)
        {
            var performanceIndicator = _performanceIndicatorRepository.Get(command.PerformanceIndicatorId);

            if (performanceIndicator == null)
            {
                throw new Exception("Not found");
            }
            
            if (!IndicatorType.Average.Equals(performanceIndicator.IndicatorType)) 
            {
                throw new ArgumentException("Invalid type");
            }

            var averageIndicators = _averageIndicatorRepository.GetByPerformanceIndicatorId(performanceIndicator.Id);

            List<AverageIndicatorDetails> averageDetails = new() { };

            for (int i = 0; i < averageIndicators.Count; i++)
            {
                averageDetails.Add(
                    new AverageIndicatorDetails(
                        date: averageIndicators[i].Date,
                        value: averageIndicators[i].Value
                    )
                );
            }
            var response = new PerformanceIndicatorByAverageDetails
                (
                    name: performanceIndicator.Name,
                    indicatorType:  performanceIndicator.IndicatorType,
                    averageIndicatorList: averageDetails,
                    totalValue: averageDetails.Count > 0 ? averageDetails.Average(e => e.Value) : 0
                );

            return Task.FromResult(response);
        }

        public Task<PerformanceIndicatorBySumDetails> Handle(PerformanceIndicatorBySumDetailsCommand command, CancellationToken cancellationToken)
        {
            var performanceIndicator = _performanceIndicatorRepository.Get(command.PerformanceIndicatorId);

            if (performanceIndicator == null)
            {
                throw new Exception("Not found");
            }

            if (!IndicatorType.Sum.Equals(performanceIndicator.IndicatorType))
            {
                throw new ArgumentException("Invalid type");
            }

            var sumIndicators = _sumIndicatorRepository.GetByPerformanceIndicatorId(performanceIndicator.Id);

            List<SumIndicatorDetails> sumDetails = new() { };

            for (int i = 0; i < sumIndicators.Count; i++)
            {
                sumDetails.Add(
                    new SumIndicatorDetails(
                        date: sumIndicators[i].Date,
                        value: sumIndicators[i].Value
                    )
                );
            }
            var response = new PerformanceIndicatorBySumDetails
                (
                    name: performanceIndicator.Name,
                    indicatorType: performanceIndicator.IndicatorType,
                    sumIndicatorList: sumDetails,
                    totalValue: sumDetails.Count > 0 ? sumDetails.Sum(e => e.Value) : 0
                );

            return Task.FromResult(response);
        }

        
    }
}
