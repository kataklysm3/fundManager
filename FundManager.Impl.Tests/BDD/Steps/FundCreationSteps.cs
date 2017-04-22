using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private Dictionary<IIdentity, ICollection<IEvent>> _eventsDictionary = new Dictionary<IIdentity, ICollection<IEvent>>();

        [BeforeScenario()]
        public void BeforeScenarioHook()
        {
            _eventStorageMock = new Mock<IEventStorage>();
            _eventStorageMock.Setup(
                    p => p.AppendToStream(It.IsAny<IIdentity>(), It.IsAny<long>(), It.IsAny<ICollection<IEvent>>()))
                .Callback<IIdentity, long, ICollection<IEvent>>((i, l, c) =>
                {
                    if (_eventsDictionary.ContainsKey(i)
                        && _eventsDictionary[i] != null)
                    {
                        var newList = new List<IEvent>(c);
                        newList.AddRange(_eventsDictionary[i]);
                        _eventsDictionary[i] = newList;
                    }
                    else
                    {
                        _eventsDictionary[i] = c;
                    }
                });
            _eventStorageMock.Setup(p => p.LoadEventStream(It.IsAny<IIdentity>())).Returns(
                new EventStream() { Events = new List<IEvent>() });
        }

        [Given(@"I have setuped fund service")]
        public void GivenSetupFundService()
        {
            _fundManagerService = new FundManagerService(_eventStorageMock.Object);
        }

        [When(@"I have created fund items with parameters:")]
        public void WhenCreateNewFundItemsWithParameters(Table fundValues)
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

        [When(@"I have added stocks to my fund:")]
        public void WhenAddedStocksToFund(Table stocksValue)
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

        [Then(@"I can see the fund events:")]
        public void ThenFundEventsWillBe(Table events)
        {

        }
    }
}