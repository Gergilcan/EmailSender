using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.Core.Models
{
  public class User
  {
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public Guid Id { get; set; }

    public List<Appointment>? Appointments { get; set; }

    public User(){}
    public User(string? name, string? email)
    {
      this.Name = name;
      this.Email = email;
      var currentDate = DateTime.Now;
      this.CreatedAt = currentDate;
      this.LastUpdatedAt = currentDate;
      Id = Guid.NewGuid();
      Appointments = new List<Appointment>();
    }
  }
}
