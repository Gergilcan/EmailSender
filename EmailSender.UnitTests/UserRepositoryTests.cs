using System;
using System.IO;
using System.Linq;
using EmailSender.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmailSender.UnitTests
{
  [TestClass()]
  public class UserRepositoryTests
  {
    private UserRepository userRepository;

    [TestInitialize]
    public void Initialize()
    {
      userRepository = new UserRepository();
    }

    [TestMethod()]
    public void CreateUserTest()
    {
      var userName = "Test";
      var email = "TestEmail";
      var time = DateTime.Now;

      userRepository.CreateUser(userName, email, time);

      var generatedUser = userRepository.Users.Single();
      Assert.IsTrue(generatedUser.Name == userName);
      Assert.IsTrue(generatedUser.Email == email);
      Assert.IsTrue(generatedUser.CreatedAt == time);
    }

    [TestMethod()]
    public void CreateUserTestWithoutUserName()
    {
      var email = "TestEmail";
      var time = DateTime.Now;

      Assert.ThrowsException<ArgumentNullException>(() => userRepository.CreateUser(null, email, time));
    }

    [TestMethod()]
    public void CreateUserTestWithoutEmail()
    {
      var userName = "Test";
      var time = DateTime.Now;

      Assert.ThrowsException<ArgumentNullException>(() => userRepository.CreateUser(userName, null, time));
    }

    [TestMethod()]
    public void LoginAtSpecifiedTimeTest()
    {
      var userName = "Test";
      var email = "TestEmail";
      var time = DateTime.Now;
      userRepository.CreateUser(userName, email, time);

      var forcedTime = time.AddMonths(1);
      userRepository.LoginAtSpecifiedTime(userName, forcedTime);

      var generatedUser = userRepository.Users.Single();
      Assert.IsTrue(generatedUser.LastUpdatedAt == forcedTime);
    }

    [TestMethod()]
    public void GetUserInformationTest()
    {
      //Arrange
      var userName = "Test";
      var email = "TestEmail";
      var time = DateTime.Now;
      userRepository.CreateUser(userName, email, time);

      //Act
      var user = userRepository.GetUserInformation(userName);

      //Assert
      var generatedUser = userRepository.Users.Single();
      Assert.IsTrue(generatedUser.LastUpdatedAt == user.LastUpdatedAt);
      Assert.IsTrue(generatedUser.Name == user.Name);
      Assert.IsTrue(generatedUser.CreatedAt == user.CreatedAt);
      Assert.IsTrue(generatedUser.Email == user.Email);
    }

    [TestMethod()]
    public void GetUserInformationTestForNonExistingUser()
    {
      //Arrange
      var userName = "Test";
      var email = "TestEmail";
      var time = DateTime.Now;
      userRepository.CreateUser(userName, email, time);

      //Act
      var user = userRepository.GetUserInformation("NonExistingUser");

      //Assert
      Assert.IsNull(user);
    }

    [TestMethod()]
    public void LoadUsersFromFileTest()
    {
      userRepository.LoadUsersFromFile("Resources/users.xml");

      Assert.IsTrue(userRepository.Users.Count == 1);
    }

    [TestMethod()]
    public void LoadUsersFromFileTestFromNonExistingFile()
    {
      Assert.ThrowsException<FileNotFoundException>(() => userRepository.LoadUsersFromFile("Resources/users2.xml"));
    }
  }
}