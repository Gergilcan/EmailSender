using EmailSender.Core.Models;

namespace EmailSender.Core.Events;

public class CloseAppointmentEvent : EmailNotificableEvent
{ 
  public Appointment Appointment { get; set; }

  public CloseAppointmentEvent() {}
  public CloseAppointmentEvent(string? userName, Appointment? appointment): base(userName)
  {
    this.Appointment = appointment;
  }
}