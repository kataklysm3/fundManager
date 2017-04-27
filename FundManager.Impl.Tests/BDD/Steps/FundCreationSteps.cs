using System.Collections.Generic;
using System.Linq;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Commands;
using FundManager.Impl.Domain.FundAggregate;
using FundManager.Impl.Storage;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FundManager.Impl.Tests.BDD.Steps
{
    [Binding]
    public class FundCreationSteps
    {
        private readonly Dictionary<IIdentity, ICollection<IEvent>> _eventsDictionary =
            new Dictionary<IIdentity, ICollection<IEvent>>();

        private Mock<IEventStorage> _eventStorageMock;
        private FundManagerService _fundManagerService;

        [BeforeScenario]
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
                        var newList = new List<IEvent>(_eventsDictionary[i]);
                        newList.AddRange(c);
                        _eventsDictionary[i] = newList;
                    }
                    else
                    {
                        _eventsDictionary[i] = c;
                    }
                });
            _eventStorageMock.Setup(p => p.LoadEventStream(It.IsAny<IIdentity>())).Returns(
                new EventStream { Events = new List<IEvent>() });
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
                _fundManagerService.Execute(new CreateFundCommand
                {
                    Id = new FundId(int.Parse(fund["Id"])),
                    Name = fund["Name"]
                });
        }

        [When(@"I have added stocks to my fund:")]
        public void WhenAddedStocksToFund(Table stocksValue)
        {
            var stockList = stocksValue.ParseTable();
            foreach (var stock in stockList)
                _fundManagerService.Execute(new AddStockCommand
                {
                    FundId = new FundId(int.Parse(stock["FundId"])),
                    StockId = new StockId(int.Parse(stock["StockId"])),
                    StockName = stock["Name"],
                    Price = new StockPrice(decimal.Parse(stock["Price"]))
                });
        }

        [Then(@"I can see the fund events:")]
        public void ThenFundEventsWillBe(Table events)
        {
            var actualCollection = _eventsDictionary.SelectMany(p => p.Value,
                (parent, child) => new { FundId = ((FundId)parent.Key).Id.ToString(), Name = child.ToString() }).ToList();

            var expectedCollection = events.Rows.Select(p => new
            {
                FundId = p.Values.ElementAt(0),
                Name = p.Values.ElementAt(1)
            }).ToList();

            CollectionAssert.AreEqual(
                expectedCollection,
                actualCollection,
                "Events are not the same");
        }
    }
}