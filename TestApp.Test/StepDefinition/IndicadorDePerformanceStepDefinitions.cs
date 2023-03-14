using MediatR;
using System;
using TechTalk.SpecFlow;
using TestApp.Application.Commands;
using TestApp.Domain.Enums;
using TestApp.Domain.Models.PerformanceIndicator;
using TestApp.Domain.Repositories;
using System.Linq;
using System.Collections.Generic;
using TestApp.Domain.Models.DailyIndicator;
using TestApp.Domain.DataTransferObjects;

namespace TestApp.Test
{
    [Binding]
    public class IndicadorDePerformanceStepDefinitions
    {
        private readonly IPerformanceIndicatorRepository _performanceIndicatorRepository;
        private readonly IAverageIndicatorRepository _averageIndicatorRepository;
        private readonly ISumIndicatorRepository _sumIndicatorRepository;
        private readonly ScenarioContext _scenarioContext;
        private readonly Mediator _mediator;

        private PerformanceIndicatorItem? averageIndicatorItem;
        private PerformanceIndicatorItem? sumIndicatorItem;
        public IndicadorDePerformanceStepDefinitions(IPerformanceIndicatorRepository performanceIndicatorRepository, ScenarioContext scenarioContext, ServiceFactory serviceFactory, IAverageIndicatorRepository averageIndicatorRepository, ISumIndicatorRepository sumIndicatorRepository)
        {
            _performanceIndicatorRepository = performanceIndicatorRepository;
            _scenarioContext = scenarioContext;
            _mediator = new Mediator(serviceFactory);
            _averageIndicatorRepository = averageIndicatorRepository;
            _sumIndicatorRepository = sumIndicatorRepository;
        }

        [Given(@"a limpeza e cadastro de exemplos")]
        public void GivenALimpezaECadastroDeExemplos()
        {
            _performanceIndicatorRepository.GetAll().Result.ToList()
                .ForEach(e => _performanceIndicatorRepository.Delete(e));


            var averageIndicator = new PerformanceIndicator(
                    name: "Average Indicator",
                    indicatorType: IndicatorType.Average
                );

            var sumIndicator = new PerformanceIndicator(
                    name: "Sum Indicator",
                    indicatorType: IndicatorType.Sum
                );

            var indicators = new List<PerformanceIndicator>()
            {
                averageIndicator, sumIndicator
            };

            _performanceIndicatorRepository.Save(indicators);

            averageIndicatorItem = new(
                averageIndicator.Id,
                averageIndicator.Name,
                averageIndicator.IndicatorType
            );

            sumIndicatorItem = new(
                sumIndicator.Id,
                sumIndicator.Name,
                sumIndicator.IndicatorType
            );

            var avarageIndicators = new List<AverageIndicator>()
            {
                new AverageIndicator(
                    performanceIndicatorId: averageIndicatorItem.Id,
                    date: DateTimeOffset.Parse("01/03/2023"),
                    value: 12
                ),
                new AverageIndicator(
                    performanceIndicatorId: averageIndicatorItem.Id,
                    date: DateTimeOffset.Parse("02/03/2023"),
                    value: 8
                ),
                new AverageIndicator(
                    performanceIndicatorId: averageIndicatorItem.Id,
                    date: DateTimeOffset.Parse("03/03/2023"),
                    value: 16
                ),
            };

            _averageIndicatorRepository.Save(avarageIndicators);


            var sumIndicators = new List<SumIndicator>()
            {
                new SumIndicator(
                    performanceIndicatorId: sumIndicatorItem.Id,
                    date: DateTimeOffset.Parse("01/03/2023"),
                    value: 37_000
                ),
                new SumIndicator(
                    performanceIndicatorId: sumIndicatorItem.Id,
                    date: DateTimeOffset.Parse("02/03/2023"),
                    value: 43_500
                ),
                new SumIndicator(
                    performanceIndicatorId: sumIndicatorItem.Id,
                    date: DateTimeOffset.Parse("03/03/2023"),
                    value: 39_780
                ),
            };

            _sumIndicatorRepository.Save(sumIndicators);

        }


        [Given(@"preenchimento e envio de um commando")]
        public void GivenPreenchimentoEEnvioDeUmCommando()
        {
            var command = new CreatePerformanceIndicatorCommand(
                    name: "Teste 1",
                    indicatorType: IndicatorType.Average,
                    date: DateTimeOffset.Now,
                    value: 10
              );

            _scenarioContext["data"] = command;
            _scenarioContext["response"] = _mediator.Send(_scenarioContext["data"]).Result;
        }

        [Then(@"uma resposta do tipo Indicador de Performance deve ser retornada")]
        public void ThenUmaRespostaDoTipoIndicadorDePerformanceDeveSerRetornada()
        {
            var response = _scenarioContext["response"] as PerformanceIndicatorItem;

            Assert.IsNotNull(response);
            Assert.AreEqual("Teste 1", response.Name);
            Assert.AreNotEqual(Guid.Empty, response.Id);
            Assert.AreNotEqual(null, response.Id);
        }

        [Given(@"a requisicao de uma lista")]
        public void GivenARequisicaoDeUmaLista()
        {
            var command = new PerformanceIndicatorListCommand();
            _scenarioContext["data"] = command;
            _scenarioContext["response"] = _mediator.Send(_scenarioContext["data"]).Result;
        }

        [Then(@"uma lista deve ser retornada")]
        public void ThenUmaListaDeveSerRetornada()
        {
            var response = _scenarioContext["response"] as List<PerformanceIndicatorItem>;
            
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Count);
            Assert.AreNotEqual(Guid.Empty, response[0].Id);
            Assert.AreNotEqual(Guid.Empty, response[1].Id);
        }

        [Given(@"a requisicao de uma performance do tipo media")]
        public void GivenARequisicaoDeUmaPerformanceDoTipoMedia()
        {
            var indicatorId = averageIndicatorItem == null ? Guid.NewGuid() : averageIndicatorItem.Id; 
            var command = new PerformanceIndicatorByAverageDetailsCommand(indicatorId);
            _scenarioContext["data"] = command;
            _scenarioContext["response"] = _mediator.Send(_scenarioContext["data"]).Result;
        }

        [Then(@"um detalhe de indicador por media deve ser retornado")]
        public void ThenUmDetalheDeIndicadorPorMediaDeveSerRetornado()
        {
            var response = _scenarioContext["response"] as PerformanceIndicatorByAverageDetails;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.AverageIndicatorList);
            Assert.AreEqual("Average Indicator", response.Name);
            Assert.AreEqual(3, response.AverageIndicatorList.Count);
            Assert.AreEqual(12, response.TotalValue);
        }

        [Given(@"a requisicao de um indicador do tipo soma")]
        public void GivenARequisicaoDeUmIndicadorDoTipoSoma()
        {
            var indicatorId = sumIndicatorItem == null ? Guid.NewGuid() : sumIndicatorItem.Id;
            var command = new PerformanceIndicatorBySumDetailsCommand(indicatorId);
            _scenarioContext["data"] = command;
            _scenarioContext["response"] = _mediator.Send(_scenarioContext["data"]).Result;
        }

        [Then(@"uma resposta do tipo indicador por soma deve ser retornada")]
        public void ThenUmaRespostaDoTipoIndicadorPorSomaDeveSerRetornada()
        {
            var response = _scenarioContext["response"] as PerformanceIndicatorBySumDetails;

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.SumIndicatorList);
            Assert.AreEqual("Sum Indicator", response.Name);
            Assert.AreEqual(3, response.SumIndicatorList.Count);
            Assert.AreEqual(120_280, response.TotalValue);
        }
    }
}
