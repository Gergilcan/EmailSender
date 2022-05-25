namespace EmailSender.Core.Events
{
  public class LastLoginExcedeedEvent : EmailNotificableEvent
  {
    public LastLoginExcedeedEvent() { }
    public LastLoginExcedeedEvent(string userName) : base(userName)
    {
    }
  }
}
