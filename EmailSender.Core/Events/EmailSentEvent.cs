namespace EmailSender.Core.Events
{
  public class EmailSentEvent : EmailNotificableEvent
  {
    public DateTime SentTime { get; set; }
    public EmailSentEvent() {}
    public EmailSentEvent(string userName, DateTime sentTime) : base(userName)
    {
      SentTime = sentTime;
    }
  }
}
