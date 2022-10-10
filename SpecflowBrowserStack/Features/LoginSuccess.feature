Feature: Login Tests Success
A short summary of the feature

@Success
Scenario Outline: Successful Login
	Given I am on "https://practicetestautomation.com/practice-test-login/"
	When I login with the correct username "student" using the correct password "Password123"
	Then I should be successfully logged in