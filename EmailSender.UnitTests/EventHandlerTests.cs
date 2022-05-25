using EmailSender.Library;
using System;
using System.Linq;
using EmailSender.Core;
using EmailSender.Core.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventHandler = EmailSender.Library.EventHandler;


namespace EmailSender.UnitTests
{
  [TestClass]
  public class EventHandlerTests
  {
    private EventHandler eventHandler;

    [TestInitialize]
    public void Initialize()
    {
      eventHandler = new EventHandler();
    }

    [TestMethod()]
    public void CleanUpTest()
    {
      eventHandler.Events.Add(new Event());
      eventHandler.CleanUp();
      
      Assert.IsTrue(!eventHandler.Events.Any());
    }

    [TestMethod]
    public void CreateUserWithAllData()
    {
      var time = DateTime.Now;
      var userName = "Test";
      var mail = "TestMail";
      eventHandler.CreateUser(userName, mail, time);

      var eventResult = (CreationEvent) eventHandler.Events.Single();
      Assert.IsTrue(eventResult.UserName == userName);
      Assert.IsTrue(eventResult.Email == mail);
      Assert.IsTrue(eventResult.CreationDate == time);
    }

    [TestMethod]
    public void CreateUserWithoutMail()
    {
      var time = DateTime.Now;
      var userName = "Test";

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.CreateUser(userName, null, time));
    }

    [TestMethod]
    public void CreateUserWithoutName()
    {
      var time = DateTime.Now;
      var mail = "TestMail";

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.CreateUser(null, mail, time));
    }

    [TestMethod]
    public void ForceLoginAtConcreteDateWithAllData()
    {
      var time = DateTime.Now;
      var userName = "Test";
      eventHandler.ForceLoginAtConcreteDate(userName, time);

      var eventResult = (LoginEvent) eventHandler.Events.Single();
      Assert.IsTrue(eventResult.UserName == userName);
      Assert.IsTrue(eventResult.LoginTime == time);
    }

    [TestMethod]
    public void ForceLoginAtConcreteDateWithoutUserName()
    {
      var time = DateTime.Now;

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.ForceLoginAtConcreteDate(null, time));
    }

    [TestMethod]
    public void GenerateEmailSentEventWithAllData()
    {
      var time = DateTime.Now;
      var userName = "Test";
      eventHandler.GenerateEmailSentEvent(userName, time);

      var eventResult = (EmailSentEvent)eventHandler.Events.Single();
      Assert.IsTrue(eventResult.UserName == userName);
      Assert.IsTrue(eventResult.SentTime == time);
    }

    [TestMethod]
    public void GenerateEmailSentEventWithoutUserName()
    {
      var time = DateTime.Now;

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.GenerateEmailSentEvent(null, time));
    }

    [TestMethod]
    public void GenerateLastLoginExcedeedEventWithAllData()
    {
      var time = DateTime.Now;
      var userName = "Test";
      eventHandler.GenerateLastLoginExcedeedEvent(userName);

      var eventResult = (LastLoginExcedeedEvent)eventHandler.Events.Single();
      Assert.IsTrue(eventResult.UserName == userName);
    }

    [TestMethod]
    public void GenerateLastLoginExcedeedEventWithoutUserName()
    {
      var time = DateTime.Now;

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.GenerateLastLoginExcedeedEvent(null));
    }

    [TestMethod]
    public void GenerateCloseAppointmentEventWithAllData()
    {
      var time = DateTime.Now;
      var userName = "Test";
      var appointmentName = "TestAppointment";
      eventHandler.GenerateCloseAppointmentEvent(userName, appointmentName);

      var eventResult = (CloseAppointmentEvent)eventHandler.Events.Single();
      Assert.IsTrue(eventResult.UserName == userName);;
      Assert.IsTrue(eventResult.AppointmentName == appointmentName);;
    }

    [TestMethod]
    public void GenerateCloseAppointmentEventWithoutUserName()
    {
      var time = DateTime.Now;
      var appointmentName = "TestAppointment";

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.GenerateCloseAppointmentEvent(null, appointmentName));
    }

    [TestMethod]
    public void GenerateCloseAppointmentEventWithoutAppointmentName()
    {
      var time = DateTime.Now;
      var userName = "Test";

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.GenerateCloseAppointmentEvent(userName, null));
    }

    [TestMethod]
    public void GenerateMissedAppointmentEventWithAllData()
    {
      var time = DateTime.Now;
      var userName = "Test";
      var appointmentName = "TestAppointment";
      eventHandler.GenerateMissedAppointmentEvent(userName, appointmentName);

      var eventResult = (ExpiredAppointmentEvent)eventHandler.Events.Single();
      Assert.IsTrue(eventResult.UserName == userName); ;
      Assert.IsTrue(eventResult.AppointmentName == appointmentName); ;
    }

    [TestMethod]
    public void GenerateMissedAppointmentEventWithoutUserName()
    {
      var time = DateTime.Now;
      var appointmentName = "TestAppointment";

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.GenerateMissedAppointmentEvent(null, appointmentName));
    }

    [TestMethod]
    public void GenerateMissedAppointmentEventWithoutAppointmentName()
    {
      var time = DateTime.Now;
      var userName = "Test";

      Assert.ThrowsException<ArgumentNullException>(() => eventHandler.GenerateMissedAppointmentEvent(userName, null));
    }
  }
}