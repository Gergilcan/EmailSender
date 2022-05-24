using EmailSender.Core;
using EmailSender.Core.Models;

namespace EmailSender.Library;

public interface IEmailSenderClient
{
  public void SendEmail(User user, EmailNotificableEvent @event);
}