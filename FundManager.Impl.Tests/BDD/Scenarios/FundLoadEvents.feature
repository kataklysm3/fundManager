Feature: FundLoadEvents
	I want to make some operations
	with fund service and see the result events

@mytag
Scenario: CreateFundWithStocks
	Given I have setuped fund service
    When I have created fund items with parameters:
	  |Id        |Name                          |
	  |1		 |My first fund                 |
	  |2		 |My second fund				|
    When I have added stocks to my fund:
	  | StockId |  Name           | FundId  | Price  |
	  | 1		| My first stock  | 1		| 12	 |
	  | 2		| My second stock | 1	    | 24	 |
	  | 3		| My third stock  | 2	    | 56	 |
	Then I can see the fund events:
	  | Id		  |  EventName										|
	  | 1		  | Fund 'My first fund' was created				|
	  | 1		  | Added 'My first stock' with Id '1' Price = 12   |
	  | 1		  | Added 'My second stock' with Id '2' Price = 24  |
	  | 2		  | Fund 'My second fund' was created				|
	  | 2		  | Added 'My third stock' with Id '3' Price = 56	|