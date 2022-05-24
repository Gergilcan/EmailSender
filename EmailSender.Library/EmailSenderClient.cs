using EmailSender.Core;
using EmailSender.Core.Events;
using EmailSender.Core.Models;

namespace EmailSender.Library;

//This is currently not used due that it is mocked on the tests, this class should contain the smtp client to actually send the emails.
public class EmailSenderClient : IEmailSenderClient
{
  public void SendEmail(User user, EmailNotificableEvent @event)
  {
    switch (@event)
    {
      case CloseAppointmentEvent e:
        Console.WriteLine($"Sending mail to the user {user.Name} to the email {user.Email} to warn him about an incoming appointment {e.Appointment.Name}");
        break;
      case LifetimeEvent e:
        Console.WriteLine($"Sending mail to the user {user.Name} to the email {user.Email} to celebrate the lifetime event");
        break;
      case ExpiredAppointmentEvent e:
        Console.WriteLine($"Sending mail to the user {user.Name} to the email {user.Email} to tell him about a lost appointment {e.AppointmentName}");
        break;
      case EmailSentEvent e:
        Console.WriteLine($"Sending mail to the user {user.Name} to the email {user.Email} to tell him that this is the monthly mail");
        break;
      case LastLoginExcedeedEvent e:
        Console.WriteLine($"Sending mail to the user {user.Name} to the email {user.Email} to tell him that their last login was more than 1 month ago");
        break;
    }
  }
}