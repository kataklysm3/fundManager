using System;
using System.Collections.Generic;
using FundManager.Impl.Domain.Contracts;
using FundManager.Impl.Domain.Contracts.Events;
using FundManager.Impl.Domain.FundAggregate;
using Moq;
using NUnit.Framework;

namespace FundManager.Impl.Tests.Domain
{
    [TestFixture]
    public class FundTest
    {
        [SetUp]
        public void SetUp()
        {
            _eventCollectionMock = new Mock<IEnumerable<IEvent>>();
            //empty list by default
            _eventCollectionMock.Setup(p => p.GetEnumerator()).Returns(new List<IEvent>().GetEnumerator());
            _fundForTesting = new Fund(_eventCollectionMock.Object);
        }

        private Fund _fundForTesting;
        private Mock<IEnumerable<IEvent>> _eventCollectionMock;

        [Test]
        public void AddStockToFundTest()
        {
            var stockName = "stock1";
            var price = new StockPrice {Amount = 10};
            var quantity = 10;
            var dateCreated = new DateTime(2000, 1, 1);
            _fundForTesting.AddStock(stockName, price, quantity, dateCreated);
            Assert.AreEqual(1, _fundForTesting.Changes.Count);
            Assert.IsTrue(_fundForTesting.Changes[0] is StockAdded);
            Assert.AreEqual(stockName, ((StockAdded) _fundForTesting.Changes[0]).StockName);
            Assert.AreEqual(price, ((StockAdded) _fundForTesting.Changes[0]).Price);
            Assert.AreEqual(quantity, ((StockAdded) _fundForTesting.Changes[0]).Quantity);
            Assert.AreEqual(dateCreated, ((StockAdded) _fundForTesting.Changes[0]).TimeAdded);
        }

        [Test]
        public void CreateFundTest()
        {
            var fundName = "test fund";
            var dateCreated = new DateTime(2000, 1, 1);
            _fundForTesting.Create(fundName, dateCreated);
            Assert.AreEqual(1, _fundForTesting.Changes.Count);
            Assert.IsTrue(_fundForTesting.Changes[0] is FundCreated);
            Assert.AreEqual(fundName, ((FundCreated) _fundForTesting.Changes[0]).Name);
            Assert.AreEqual(dateCreated, ((FundCreated) _fundForTesting.Changes[0]).Created);
        }

        [Test]
        public void FundCreatedWithErrorTest()
        {
            _fundForTesting.Create("test name", new DateTime(2000, 1, 1));
            Assert.Throws<InvalidOperationException>(
                () => { _fundForTesting.Create("test name", new DateTime(2001, 1, 1)); }, "Fund was already created");
        }
    }
}