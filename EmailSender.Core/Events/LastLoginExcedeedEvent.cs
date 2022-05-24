using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Core.Events
{
  public class LastLoginExcedeedEvent : EmailNotificableEvent
  {
    public LastLoginExcedeedEvent() { }
    public LastLoginExcedeedEvent(string? userName) : base(userName)
    {
    }
  }
}
