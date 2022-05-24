using EmailSender.Core.Models;

namespace EmailSender.Library;

public interface IUserRepository
{
  Guid CreateUser(string? userName, string? email, DateTime forcedCreationDate);
  void LoginAtSpecifiedTime(string? userName, DateTime loginTime);
  User GetUserInformation(string? userName);
  void LoadUsersFromFile(string path);
}