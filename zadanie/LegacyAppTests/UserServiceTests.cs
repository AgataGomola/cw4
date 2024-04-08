using LegacyApp;
using LegacyApp.Interfaces;

namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]
    public void AddUser_Should_Return_False_When_Email_Without_At_And_Dot()
    {
        //Arrange
        string firstName = "John";
        string lastName = "Doe";
        DateTime birthDate = new DateTime(1969, 4, 20);
        int clientId = 1;
        string email = "doe";
        var service = new UserService();

        //Act

        bool result = service.AddUser(firstName, lastName, email, birthDate, clientId);

        //Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void Add_User_Returns_False_When_First_Name_Is_Empty()
    {

        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            null,
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        Assert.Equal(false, result);
    }

    [Fact]
    public void AddUser_ThrowsArgumentExceptionWhenClientDoesNotExist()
    {

        // Arrange
        var userService = new UserService();

        // Act
        Action action = () => userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            100
        );

        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    [Fact]
    public void AddUser_TReturnsFalseWhenYoungerThen21YearsOld()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            "Marian",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2020-01-01"),
            1
        );

        // Assert
        Assert.Equal(false, result);
    }


    [Fact]
    public void AddUser_ReturnsTrueWhenVeryImportantClient()
    {
        // Arrange
        var userService = new UserService();
        var clientRepository = new ClientRepository();

        // Act
        var user = userService.AddUser(
            "Marian",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );
        var client = clientRepository.GetById(1);
        var result = client.Type.Equals("VeryImportantClient");

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void AddUser_ReturnsTrueWhenImportantClient()
    {
        // Arrange
        var userService = new UserService();
        var clientRepository = new ClientRepository();

        // Act
        var user = userService.AddUser(
            "Marian",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );
        var client = clientRepository.GetById(1);
        var result = client.Type.Equals("ImportantClient");

        // Assert
        Assert.Equal(true, result);
    }

    [Fact]
    public void AddUser_ReturnsTrueWhenNormalClient()
    {
        // Arrange
        var userService = new UserService();
        var clientRepository = new ClientRepository();

        // Act
        var user = userService.AddUser(
            "Marian",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );
        var client = clientRepository.GetById(1);
        var result = client.Type.Equals("NormalClient");

        // Assert
        Assert.Equal(true, result);
    }
    [Fact]
    public void AddUser_ThrowsExceptionWhenUserNoCreditLimitExistsForUser()
    {

        // Arrange
        var userService = new UserService();
        var clientRepository = new ClientRepository();

        // Act
        Action action = () => userService.AddUser(
            "Jan",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            100
        );
        var client = clientRepository.GetById(1);
        
        // Assert
        if (!User.HasLimitCredit(client))
        {
            Assert.Throws<ArgumentException>(action);
        }
    }
    [Fact]
    public void AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit()
    {
        // Arrange
        var userService = new UserService();
        var clientRepository = new ClientRepository();

        // Act
        var user = userService.AddUser(
            "Marian",
            "Kowalski",
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );
        var client = clientRepository.GetById(1);
        var result = client.Type.Equals("NormalClient") && !User.HasLimitCredit(client);

        // Assert
        Assert.Equal(false, result);
    }
}

