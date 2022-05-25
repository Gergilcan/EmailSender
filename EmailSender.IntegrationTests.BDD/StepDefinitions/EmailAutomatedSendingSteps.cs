#region

using System.Globalization;
using EmailSender.Core.Events;
using EmailSender.Core.Models;
using EmailSender.Library;
using Moq;
using EventHandler = EmailSender.Library.EventHandler;

#endregion

namespace EmailSender.IntegrationTests.BDD.StepDefinitions
{
  [Binding]
  public sealed class EmailAutomatedSendingSteps
  {
    private IUserRepository userRepository;
    private EventManager eventManager;
    private EventHandler eventHandler;
    private Mock<IEmailSenderClient> emailSenderClient;
    private User currentUser;
    private Appointment currentAppointment;

    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
    [BeforeScenario]
    public void SetUpUserForScenario()
    {
      userRepository = new UserRepository();
      emailSenderClient = new Mock<IEmailSenderClient>();
      eventManager = new EventManager(userRepository, emailSenderClient.Object);
      eventHandler = new EventHandler(); 
      eventHandler.AddObserver(eventManager);

      userRepository.LoadUsersFromFile("Resources/users.xml");
      eventManager.LoadEventFile("Resources/events.xml");
      //Setting the user of the file as the default one
      currentUser = userRepository.GetUserInformation("John Smith");
    }

    [AfterScenario]
    public void CleanUpForScenario()
    {
      eventHandler.CleanUp();
    }

    [Given(@"the last login time was more than one month ago")]
    public void GivenTheLastLoginTimeWasMoreThanOneMonthAgo()
    {
      eventHandler.GenerateLastLoginExcedeedEvent(currentUser.Name);
    }

    [Given(@"the last login time was after the appointment date")]
    public void GivenTheLastLoginTimeWasAfterTheAppointmentDate()
    {
      //This date is extracted from the users.xml appointments date for the John Smith user first appointment
      eventHandler.ForceLoginAtConcreteDate(currentUser.Name, new DateTime(2022, 5, 25));
    }

    [Given(@"the last login time was 10 minutes before the appointment date")]
    public void GivenTheLastLoginTimeWasBeforeTheAppointmentDate()
    {
      //This date is extracted from the users.xml appointments date for the John Smith user
      eventHandler.ForceLoginAtConcreteDate(currentUser.Name, new DateTime(2022, 5, 31, 7, 16, 46));
    }

    [Given(@"the (.*) appointment was not executed")]
    public void GivenTheAppointmentWasNotExecuted(string appointmentName)
    {
      currentAppointment = currentUser.Appointments?.SingleOrDefault(x => x.Name == appointmentName);
      if (currentAppointment == null)
      {
        throw new ArgumentException($"The specified appointment was not existing for the user {currentUser.Name}");
      }

      currentAppointment.Assisted = false;
    }

    [Given(@"a user was created one year before")]
    public void GivenAUserIsCreatedOneYearBefore()
    {
      var userName = "Maria Lahm";
      userRepository.CreateUser(userName, "marialahm@gmail.com", DateTime.Now.AddYears(-1).Date);
      currentUser = userRepository.GetUserInformation(userName);
    }


    [Given("that a mail was sended on (.*)")]
    public void MailSendedOn(string date)
    {
      eventHandler.GenerateEmailSentEvent(currentUser.Name, DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture));
    }

    [When(@"the appointment missed event is fired")]
    public void WhenTheAppointmentMissedEventIsFired()
    {
      eventHandler.GenerateMissedAppointmentEvent(currentUser.Name, currentAppointment.Name);
    }

    [When(@"the appointment close event is fired")]
    public void WhenTheAppointmentCloseEventIsFired()
    {
      eventHandler.GenerateCloseAppointmentEvent(currentUser.Name, currentAppointment.Name);
    }


    [When("the specific time event is received")]
    public void ReceiveLifetimeEvent()
    {
      eventHandler.GenerateLifeTimeEvent(currentUser.Name);
    }

    [When(@"the last email sent time exceeded event is received")]
    public void WhenTheLastEmailSentTimeExceededEventIsReceived()
    {
      eventHandler.GenerateEmailSentEvent(currentUser.Name, DateTime.Now.AddMonths(-1));
    }

    [When(@"the exceeded login event is received")]
    public void WhenTheExceededLoginEventIsReceived()
    {
      eventHandler.GenerateLastLoginExcedeedEvent(currentUser.Name);
    }

    [Then(@"an email is sent to the user every month")]
    public void ThenAnEmailIsSentToTheUserEveryMonth()
    {
      emailSenderClient.Verify(x => x.SendEmail(currentUser, It.IsAny<EmailSentEvent>()));
    }

    [Then("an email is sent to the user")]
    public void ALifeTimeEventIsSent()
    {
      emailSenderClient.Verify(x => x.SendEmail(currentUser, It.IsAny<LifetimeEvent>()));
    }

    [Then(@"an email is sent to the user warning about the excedeed time")]
    public void ThenAnEmailIsSentToTheUserWarningAboutTheExcedeedTime()
    {
      emailSenderClient.Verify(x => x.SendEmail(currentUser, It.IsAny<LastLoginExcedeedEvent>()));
    }

    [Then(@"an email is sent to the user warning about the missed appointment")]
    public void ThenAnEmailIsSentToTheUserWarningAboutTheMissedAppointment()
    {
      emailSenderClient.Verify(x => x.SendEmail(currentUser, It.IsAny<ExpiredAppointmentEvent>()));
    }

    [Then(@"an email is sent to the user warning about the close appointment")]
    public void ThenAnEmailIsSentToTheUserWarningAboutTheCloseAppointment()
    {
      emailSenderClient.Verify(x => x.SendEmail(currentUser, It.IsAny<CloseAppointmentEvent>()));
    }
  }
}