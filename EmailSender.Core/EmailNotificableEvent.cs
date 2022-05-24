using EmailSender.Core.Models;

namespace EmailSender.Core;

public class EmailNotificableEvent : Event
{
  public EmailNotificableEvent() { }
  protected EmailNotificableEvent(string? userName) : base(userName) { }
}