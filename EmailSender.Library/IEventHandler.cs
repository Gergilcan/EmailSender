using EmailSender.Core;

namespace EmailSender.Library;

public interface IEventHandler
{
  void AddObserver(IObserver<Event> observer);
  void CreateUser(string? name, string? mail, DateTime forcedCreationDate);
  void ForceLoginAtConcreteDate(string? userName, DateTime updateTime);
  void GenerateLifeTimeEvent(string? userName);
}