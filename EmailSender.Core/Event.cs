using EmailSender.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmailSender.Core
{
  [XmlInclude(typeof(CreationEvent))]
  [XmlInclude(typeof(LoginEvent))]
  [XmlInclude(typeof(ExpiredAppointmentEvent))]
  [XmlInclude(typeof(CloseAppointmentEvent))]
  [XmlInclude(typeof(LifetimeEvent))]
  [XmlInclude(typeof(EmailSentEvent))]
  [XmlInclude(typeof(LastLoginExcedeedEvent))]
  public class Event
  {
    public Event() { }
    public Event(string? userName)
    {
      UserName = userName;
    }

    public string? UserName { get; set; }
  }
}
