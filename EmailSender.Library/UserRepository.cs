#region

using System.Xml.Serialization;
using EmailSender.Core.Models;

#endregion

namespace EmailSender.Library
{
  public class UserRepository : IUserRepository
  {
    public List<User> Users { get; set; }

    public UserRepository()
    {
      Users = new List<User>();
    }

    public Guid CreateUser(string userName, string email, DateTime forcedCreationDate)
    {
      if (userName == null || email == null) throw new ArgumentNullException("The name and email are mandatory");

      var newUser = new User(userName, email) { CreatedAt = forcedCreationDate };
      Users.Add(newUser);
      return newUser.Id;
    }

    public void LoginAtSpecifiedTime(string userName, DateTime loginTime)
    {
      var foundUser = Users.SingleOrDefault(x => x.Name == userName);
      if (foundUser != null)
      {
        foundUser.LastUpdatedAt = loginTime;
      }
    }

    public User GetUserInformation(string userName)
    {
      return Users.SingleOrDefault(x => x.Name == userName)!;
    }

    public void LoadUsersFromFile(string path)
    {
      if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("The path must be defined");

      var serializer = new XmlSerializer(typeof(List<User>));
      var fileStream = File.OpenRead(path);
      Users = (List<User>)serializer.Deserialize(fileStream)!;
      fileStream.Close();
    }
  }
}
