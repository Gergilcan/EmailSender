using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using EmailSender.Core;
using EmailSender.Core.Events;
using EmailSender.Core.Models;
using EmailSender.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EventHandler = EmailSender.Library.EventHandler;

namespace EmailSender.IntegrationTests
{
  [TestClass]
  public class IntegrationTests
  {
    private IUserRepository userRepository;
    private IEventHandler handler;
    private Mock<IEmailSenderClient> emailSenderClient;

    [TestInitialize]
    public void Setup()
    {
      userRepository = new UserRepository();
      //Clarification: I have decided to mock the email sender client due that its not the real objective of the exercise to send the mail itself but the manage of it
      emailSenderClient = new Mock<IEmailSenderClient>();
      IObserver<Event> manager = new EventManager(userRepository, emailSenderClient.Object);
      handler = new EventHandler();
      handler.AddObserver(manager);
      userRepository.LoadUsersFromFile("../../../Resources/users.xml");
    }

    [TestMethod]
    public void WhenReceivingACreateUserEvent_AUserIsCreatedCorrectly()
    {
      var userName = "Maria Lamb";
      handler.CreateUser(userName, "marialamb@outlook.com", DateTime.Now);

      Assert.IsNotNull(userRepository.GetUserInformation(userName));
    }


    [TestMethod]
    public void WhenReceivingALoginEvent_AUserLastLoginDateIsUpdatedCorrectly()
    {
      var userName = "Maria Lamb";
      handler.CreateUser(userName, "marialamb@outlook.com", DateTime.Now);

      var currentTime = DateTime.Now;
      handler.ForceLoginAtConcreteDate(userName, currentTime);
      Assert.IsTrue(userRepository.GetUserInformation(userName).LastUpdatedAt == currentTime);
    }

    [TestMethod]
    public void WhenReceivingALifeTimeEvent_AUserNeedsToBeNotifiedByEmail()
    {
      var userName = "Maria Lamb";
      handler.CreateUser(userName, "marialamb@outlook.com", DateTime.Now);

      var currentTime = DateTime.Now;
      handler.GenerateLifeTimeEvent(userName);

      var user = userRepository.GetUserInformation(userName);
      emailSenderClient.Verify(x=> x.SendEmail(user, It.IsAny<LifetimeEvent>()));
    }

    [TestMethod]
    public void WhenReceivingACloseAppointmentEvent_AUserNeedsToBeNotifiedByEmail()
    {
      var userName = "Maria Lamb";
      var currentTime = DateTime.Now;
      handler.ForceLoginAtConcreteDate(userName, DateTime.Now);

      var user = userRepository.GetUserInformation(userName);
      emailSenderClient.Verify(x => x.SendEmail(user, It.IsAny<CloseAppointmentEvent>()));
    }
  }
}