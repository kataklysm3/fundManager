Feature: FundLoadEvents
	I want to make some operations
	with fund service and see the result events

@mytag
Scenario: CreateFundWithStocks
	Given I have created fund items with parameters:
	  |Id        |Name                          |
	  |1		 |My first fund                 |
	  |2		 |My second fund				|
    And I have added stocks to my fund:
	  | StockId |  Name           | FundId  |
	  | 1		| My first stock  | 1		|
	  | 2		| My second stock | 1	    |
	  | 3		| My third stock  | 2	    |
	When I press add
	Then the result should be 120 on the screen
