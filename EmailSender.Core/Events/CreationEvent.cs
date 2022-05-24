namespace EmailSender.Core.Events;

public class CreationEvent : Event
{
  public string Email { get; set; }
  public DateTime CreationDate { get; set; }

  public CreationEvent() {}

  public CreationEvent(string name, string email, DateTime creationDate) : base(name)
  {
    CreationDate = creationDate;
    Email = email;
  }
}