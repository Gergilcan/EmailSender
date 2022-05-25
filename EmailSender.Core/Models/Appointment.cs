namespace EmailSender.Core.Models
{
  public class Appointment
  {
    public string Name { get; set; }
    public DateTime AppointmentTime { get; set; }
    public Boolean Assisted { get; set; }
  }
}
