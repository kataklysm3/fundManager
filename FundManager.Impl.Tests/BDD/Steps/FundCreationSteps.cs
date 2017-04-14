using System.Collections.Generic;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Commands;
using FundManager.Impl.Domain.FundAggregate;
using FundManager.Impl.Storage;
using Moq;
using TechTalk.SpecFlow;

namespace FundManager.Impl.Tests.BDD.Steps
{
    [Binding]
    public class FundCreationSteps
    {
        private FundManagerService _fundManagerService;
        private Mock<IEventStorage> _eventStorageMock;

        [BeforeScenario()]
        public void BeforeScenarioHook()
        {
            _eventStorageMock = new Mock<IEventStorage>();
            _eventStorageMock.Setup(p => p.LoadEventStream(It.IsAny<IIdentity>())).Returns(
                new EventStream() { Events = new List<IEvent>() });
            _fundManagerService = new FundManagerService(_eventStorageMock.Object);
        }

        [Given(@"I have created fund items with parameters:")]
        public void GivenCreateNewFundItemsWithParameters(Table fundValues)
        {
            var fundList = fundValues.ParseTable();
            foreach (var fund in fundList)
            {
                _fundManagerService.Execute(new CreateFundCommand()
                {
                    Id = new FundId(int.Parse(fund["Id"])),
                    Name = fund["Name"]
                });
            }
        }

        [Given(@"I have added stocks to my fund:")]
        public void AndAddedStocksToFund(Table stocksValue)
        {
            var stockList = stocksValue.ParseTable();
            foreach (var stock in stockList)
            {
                _fundManagerService.Execute(new AddStockCommand()
                {
                    FundId = new FundId(int.Parse(stock["FundId"])),
                    StockName = stock["Name"]
                });
            }
        }


        [When(@"I press add")]
        public void WhenIAddStockToFund()
        {
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int p0)
        {
        }
    }
}