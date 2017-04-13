using System;
using TechTalk.SpecFlow;

namespace FundManager.Impl.Tests.BDD.Steps
{
    [Binding]
    public class FundCreationSteps
    {
        [Given(@"I have created fund with parameters:")]
        public void GivenCreateNewFundWithParameters(Table fundValues)
        {
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
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
