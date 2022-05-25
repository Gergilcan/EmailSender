namespace EmailSender.Core.Events
{
  public class LoginEvent : Event
  {
    public DateTime LoginTime { get; set; }
    public LoginEvent() {}
    public LoginEvent(string userName, DateTime loginTime) : base(userName)
    {
      LoginTime = loginTime;
    }
  }
}
