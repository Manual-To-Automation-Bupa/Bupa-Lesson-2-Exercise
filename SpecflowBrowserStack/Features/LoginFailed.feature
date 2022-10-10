Feature: Login Tests Failure

A short summary of the feature

Scenario Outline: Failed Login
	Given I navigate to the "https://practicetestautomation.com/practice-test-login/" page
	When I login with incorrect details
	Then The login is failed
