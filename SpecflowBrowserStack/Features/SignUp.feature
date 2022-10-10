Feature: Sign Up

A short summary of the feature

@tag1
Scenario: Sign Up For Account
	Given I go to "https://automationexercise.com"
	When I click on Sign Up / Login
	When I enter a name and email address into the sign up section
	When I fill in my personal details
	Then My new account was successfully created
