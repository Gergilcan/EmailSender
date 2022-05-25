#region

using System;
using System.IO;
using EmailSender.Core;
using EmailSender.Core.Events;
using EmailSender.Core.Models;
using EmailSender.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

#endregion

namespace EmailSender.UnitTests
{
  [TestClass]
  public class EventManagerTests
  {
    private EventManager eventManager;
    private Mock<IUserRepository> userRepository;
    private Mock<IEmailSenderClient> emailClientSender;

    [TestInitialize]
    public void Initialize()
    {
      userRepository = new Mock<IUserRepository>();
      emailClientSender = new Mock<IEmailSenderClient>();
      eventManager = new EventManager(userRepository.Object, emailClientSender.Object);
    }

    [TestMethod]
    public void OnNextMustSendEmailWhenEventIsANotificableEvent()
    {
      //Arrange
      var mockUser = new User("TestUserName", "TestMail");
      userRepository.Setup(x => x.GetUserInformation(It.IsAny<string>())).Returns(mockUser);
      var eventValue = new EmailNotificableEvent();

      //Act
      eventManager.OnNext(eventValue);

      //Assert
      emailClientSender.Verify(x=> x.SendEmail(mockUser, It.IsAny<EmailNotificableEvent>()));
    }

    [TestMethod]
    public void OnNextMustCreateUserWhenCreationEventIsReceived()
    {
      //Arrange
      var userName = "TestUserName";
      var userEmail = "TestMail";
      var time = DateTime.Now;

      var creationEvent = new CreationEvent(userName, userEmail, time);

      //Act
      eventManager.OnNext(creationEvent);

      //Assert
      userRepository.Verify(x => x.CreateUser(userName, userEmail, time));
    }

    [TestMethod]
    public void OnNextMustUpdateLoginInfoOfUserWhenLoginEventIsReceived()
    {
      //Arrange
      var userName = "TestUserName";
      var time = DateTime.Now;

      var creationEvent = new LoginEvent(userName, time);

      //Act
      eventManager.OnNext(creationEvent);

      //Assert
      userRepository.Verify(x => x.LoginAtSpecifiedTime(userName, time));
    }

    [TestMethod]
    public void LoadEventFileTestWhenFileDoesNotExists()
    {
      Assert.ThrowsException<FileNotFoundException>(() => eventManager.LoadEventFile("Resources/events2.xml"));
    }

    [TestMethod]
    public void OnErrorTest()
    {
      Assert.ThrowsException<NullReferenceException>(() => eventManager.OnError(It.IsAny<NullReferenceException>()));
    }
  }
}