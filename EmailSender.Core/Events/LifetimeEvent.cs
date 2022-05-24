using EmailSender.Core.Models;

namespace EmailSender.Core.Events;

public class LifetimeEvent : EmailNotificableEvent
{
  public LifetimeEvent() { }
  public LifetimeEvent(string? userName) : base(userName) { }
}