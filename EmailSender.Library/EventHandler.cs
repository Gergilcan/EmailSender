#region

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using EmailSender.Core;
using EmailSender.Core.Events;

#endregion

namespace EmailSender.Library
{
  public class EventHandler : IEventHandler
  {
    public ObservableCollection<Event> Events { get; set; }
    private readonly List<IObserver<Event>> observers;

    public EventHandler()
    {
      Events = new ObservableCollection<Event>();
      observers = new List<IObserver<Event>>();
      Events.CollectionChanged += Events_CollectionChanged;
    }

    public void AddObserver(IObserver<Event> observer)
    {
      observers.Add(observer);
    }

    private void Events_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      foreach (var observer in observers)
      {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
          foreach (Event item in e.NewItems!)
          {
            observer.OnNext(item);
          }

        }
      }
    }

    public void CreateUser(string name, string mail, DateTime forcedCreationTime)
    {
      ValidateParameter(nameof(name), name);
      ValidateParameter(nameof(mail), mail);
      Events.Add(new CreationEvent(name, mail, forcedCreationTime));
    }

    public void ForceLoginAtConcreteDate(string userName, DateTime updateTime)
    {
      ValidateParameter(nameof(userName), userName);
      Events.Add(new LoginEvent(userName, updateTime));
    }

    public void GenerateLifeTimeEvent(string userName)
    {
      ValidateParameter(nameof(userName), userName);
      Events.Add(new LifetimeEvent(userName));
    }

    public void GenerateEmailSentEvent(string userName, DateTime sentTime)
    {
      ValidateParameter(nameof(userName), userName);
      Events.Add(new EmailSentEvent(userName, sentTime));
    }

    public void GenerateLastLoginExcedeedEvent(string userName)
    {
      ValidateParameter(nameof(userName), userName);
      Events.Add(new LastLoginExcedeedEvent(userName));
    }

    public void GenerateMissedAppointmentEvent(string currentUserName, string currentAppointmentName)
    {
      ValidateParameter(nameof(currentUserName), currentUserName);
      ValidateParameter(nameof(currentAppointmentName), currentAppointmentName);
      Events.Add(new ExpiredAppointmentEvent(currentUserName, currentAppointmentName));
    }

    public void GenerateCloseAppointmentEvent(string currentUserName, string currentAppointmentName)
    {
      ValidateParameter(nameof(currentUserName), currentUserName);
      ValidateParameter(nameof(currentAppointmentName), currentAppointmentName);
      Events.Add(new CloseAppointmentEvent(currentUserName, currentAppointmentName));
    }

    private static void ValidateParameter(string parameterName, object parameterValue)
    {
      if (parameterValue == null)
      {
        throw new ArgumentNullException($"The {parameterName} is mandatory");
      }
    }

    public void CleanUp()
    {
      observers.Clear();
      Events.Clear();
    }
  }
}