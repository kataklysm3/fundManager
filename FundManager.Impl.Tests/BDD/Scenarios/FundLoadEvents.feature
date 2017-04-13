Feature: FundLoadEvents
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: CreateFundWithStocks
	Given I have created fund with parameters:
	  |Id        |Name                          |
	  |1		 |My first fund                 |
	  |2		 |My second fund				|
    And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
