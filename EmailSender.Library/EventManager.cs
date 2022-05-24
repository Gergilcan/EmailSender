using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EmailSender.Core;
using EmailSender.Core.Events;

namespace EmailSender.Library
{
  public class EventManager : IObserver<Event>
  {
    private readonly IUserRepository userRepository;
    private readonly IEmailSenderClient emailSenderClient;
    
    public EventManager(IUserRepository userRepository, IEmailSenderClient emailSenderClient)
    {
      this.userRepository = userRepository;
      this.emailSenderClient = emailSenderClient;
    }

    public void LoadEventFile(string filePath)
    {
      XmlSerializer serializer = new XmlSerializer(typeof(List<Event>));
      var eventFileStream = File.OpenRead("Resources/events.xml");
      var events = (List<Event>) serializer.Deserialize(eventFileStream);
      ProcessEventList(events);
    }

    private void ProcessEventList(List<Event> events)
    {
      foreach (var @event in events)
      {
        OnNext(@event);
      }
    }

    public void OnCompleted()
    {
      //Nothing to do here when the event finish sending things, we will keep listening
    }

    public void OnError(Exception error)
    {
      throw error;
    }

    public void OnNext(Event value)
    {
      switch (value)
      {
        case LoginEvent e: 
          userRepository.LoginAtSpecifiedTime(e.UserName, e.LoginTime);
          break;

        case CreationEvent e:
          userRepository.CreateUser(e.UserName, e.Email, e.CreationDate);
          break;

        case EmailNotificableEvent e:
          var user = userRepository.GetUserInformation(e.UserName);
          emailSenderClient.SendEmail(user, e);
          break;
      }
    }
  }
}
