# EmailSender.Library

Library that recieve events from a file or from an external source and send mails to the users.


# Basic plan
The basic plan it would be to have a message queue (SQS or Kafka if we want to use it also as a temporal storage) in order to recieve the messages regarding the updates and events that we need to trace in our microservice in order to send the appropiate mail using the information that we receive.

In our microservice we could have a listener for that queue and then we would have an smtp client with the credentials needed in order to be able to send the emails to the users.

Of course, all of this needs to have the appropiate role and permissions to communicate between them.

In this test, due that the time that it takes to create this infrastructure, apart of the cost. I have decided to simulate the queue system with an observer pattern, so we have the EventHandler that is the one in charge of producing the events and the EventManager is the observer of it and depending on the event that we recieve it is able to create users, set their last login time, or send concrete emails depending on the type of the event with the information that we receive (in this case we use the username but we could use the user unique id for example).

In order to test the functionality I have added unit tests for the main classes and also I have added some integration tests using BDD (in this case I have used Specflow that is the framework that uses Gherkin language for it).

I have created scenarios for the cases that were commented on the exercise.

In all the cases the email is not actually sent due of a lack of time to create an smtp client with some appropiate credentials because I think that its not the objective of it. So I just mocked it and I verify if the method is actually called with the appropiate event type and parameters.
