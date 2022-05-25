using EmailSender.Core.Models;

namespace EmailSender.Core.Events;

public class CloseAppointmentEvent : EmailNotificableEvent
{ 
  public string AppointmentName { get; set; }

  public CloseAppointmentEvent() {}
  public CloseAppointmentEvent(string? userName, string appointmentName): base(userName)
  {
    this.AppointmentName = appointmentName;
  }
}