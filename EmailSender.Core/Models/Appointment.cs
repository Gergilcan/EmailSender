using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Core.Models
{
  public class Appointment
  {
    public string Name { get; set; }
    public DateTime AppointmentTime { get; set; }
    public Boolean Assisted { get; set; }
  }
}
