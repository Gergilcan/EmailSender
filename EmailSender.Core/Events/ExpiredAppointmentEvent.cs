using EmailSender.Core.Models;

namespace EmailSender.Core.Events;

public class ExpiredAppointmentEvent : EmailNotificableEvent
{ 
  public string AppointmentName { get; set; }
  public ExpiredAppointmentEvent() {}

  public ExpiredAppointmentEvent(string? userName, string? appointmentName): base(userName)
  {
    this.AppointmentName = appointmentName;
  }
}