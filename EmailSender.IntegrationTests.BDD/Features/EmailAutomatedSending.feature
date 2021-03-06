Feature: Email automated sending

Scenario: A certain amount of time passing after a user lifetime event ("joining")
	Given a user was created one year before
	When the specific time event is received 
	Then an email is sent to the user


Scenario: A certain amount of time passing after a previous email in the series is sent
	Given that a mail was sended on 24/04/2021
	When the last email sent time exceeded event is received 
	Then an email is sent to the user every month

Scenario: A certain amount of time passing without using the platform.
	Given the last login time was more than one month ago
	When the exceeded login event is received 
	Then an email is sent to the user warning about the excedeed time

Scenario: A missed appointment.
	Given the last login time was after the appointment date
	And the HR Interview appointment was not executed
	When the appointment missed event is fired
	Then an email is sent to the user warning about the missed appointment
	
Scenario: An upcoming appointment.
	Given the last login time was 10 minutes before the appointment date
	And the Interview appointment was not executed
	When the appointment close event is fired
	Then an email is sent to the user warning about the close appointment