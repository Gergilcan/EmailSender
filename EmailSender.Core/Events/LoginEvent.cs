using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Core.Events
{
  public class LoginEvent : Event
  {
    public DateTime LoginTime { get; set; }
    public LoginEvent() {}
    public LoginEvent(string? userName, DateTime loginTime) : base(userName)
    {
      LoginTime = loginTime;
    }
  }
}
