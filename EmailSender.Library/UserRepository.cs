using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EmailSender.Core.Models;

namespace EmailSender.Library
{
  public class UserRepository : IUserRepository
  {
    public List<User> Users { get; set; }

    public UserRepository()
    {
      Users = new List<User>();
    }

    public Guid CreateUser(string? userName, string? email, DateTime forcedCreationDate)
    {
      var newUser = new User(userName, email) { CreatedAt = forcedCreationDate };
      Users.Add(newUser);
      return newUser.Id;
    }

    public void LoginAtSpecifiedTime(string? userName, DateTime loginTime)
    {
      var foundUser = Users.SingleOrDefault(x => x.Name == userName);
      if (foundUser != null)
      {
        foundUser.LastUpdatedAt = loginTime;
      }
    }

    public User GetUserInformation(string? userName)
    {
      return Users.SingleOrDefault(x => x.Name == userName)!;
    }

    public void LoadUsersFromFile(string path)
    {
      var serializer = new XmlSerializer(typeof(List<User>));
      var fileStream = File.OpenRead(path);
      Users = (List<User>)serializer.Deserialize(fileStream)!;
      fileStream.Close();
    }
  }
}
