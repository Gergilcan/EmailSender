using System.Collections.ObjectModel;
using System.Collections.Specialized;
using EmailSender.Core;
using EmailSender.Core.Events;

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

    private void Events_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
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

    public void CreateUser(string? name, string? mail, DateTime forcedCreationTime)
    {
      Events.Add(new CreationEvent(name, mail, forcedCreationTime));
    }

    public void ForceLoginAtConcreteDate(string? userName, DateTime updateTime)
    {
      Events.Add(new LoginEvent(userName, updateTime));
    }

    public void GenerateLifeTimeEvent(string? userName)
    {
      Events.Add(new LifetimeEvent(userName));
    }

    public void GenerateEmailSentEvent(string? userName, DateTime sentTime)
    {
      Events.Add(new EmailSentEvent(userName, sentTime));
    }

    public void GenerateLastLoginExcedeedEvent(string? userName)
    {
      Events.Add(new LastLoginExcedeedEvent(userName));
    }
  }
}