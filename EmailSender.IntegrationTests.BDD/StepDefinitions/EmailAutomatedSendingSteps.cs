using System.Globalization;
using System.Runtime.CompilerServices;
using EmailSender.Core.Events;
using EmailSender.Core.Models;
using EmailSender.Library;
using Moq;
using EventHandler = EmailSender.Library.EventHandler;

namespace EmailSender.IntegrationTests.BDD.StepDefinitions
{
  [Binding]
  public sealed class EmailAutomatedSendingSteps
  {
    public IUserRepository userRepository;
    private readonly EventManager eventManager;
    private EventHandler eventHandler;
    private Mock<IEmailSenderClient> emailSenderClient;
    private User currentUser;

    public EmailAutomatedSendingSteps(ScenarioContext context)
    {
      userRepository = new UserRepository();
      eventHandler = new EventHandler();
      emailSenderClient = new Mock<IEmailSenderClient>();
      eventManager = new EventManager(userRepository, emailSenderClient.Object);
      eventHandler.AddObserver(eventManager);
    }

    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef
    [BeforeScenario]
    public void SetUpUserForScenario()
    {
      userRepository.LoadUsersFromFile("Resources/users.xml");
      eventManager.LoadEventFile("Resources/events.xml");
      //Setting the user of the file as the default one
      currentUser = userRepository.GetUserInformation("John Smith");
    }

    [Given(@"the last login time was more than one month ago")]
    public void GivenTheLastLoginTimeWasMoreThanOneMonthAgo()
    {
      eventHandler.GenerateLastLoginExcedeedEvent(currentUser.Name);
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

  }
}