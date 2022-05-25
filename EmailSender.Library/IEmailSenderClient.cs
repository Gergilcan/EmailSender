#region

using EmailSender.Core;
using EmailSender.Core.Models;

#endregion

namespace EmailSender.Library;

public interface IEmailSenderClient
{
  public void SendEmail(User user, EmailNotificableEvent @event);
}