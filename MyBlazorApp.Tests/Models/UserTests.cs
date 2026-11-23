using MyBlazorApp.DatabaseModel;
using FluentAssertions;
using Xunit;

namespace MyBlazorApp.Tests.Models;

public class UserTests
{
    [Fact]
    public void User_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var user = new User();

        // Assert
        user.Id.Should().Be(0);
        user.Name.Should().BeNull();
        user.Salary.Should().Be(0);
        user.Transactions.Should().NotBeNull();
        user.Transactions.Should().BeEmpty();
    }

    [Fact]
    public void User_ShouldSet_PropertiesCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "John Doe",
            Salary = 50000.00m
        };

        // Assert
        user.Id.Should().Be(1);
        user.Name.Should().Be("John Doe");
        user.Salary.Should().Be(50000.00m);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1000)]
    [InlineData(50000.50)]
    [InlineData(999999.99)]
    public void User_Salary_ShouldAccept_ValidDecimalValues(decimal salary)
    {
        // Arrange
        var user = new User { Salary = salary };

        // Assert
        user.Salary.Should().Be(salary);
    }

    [Fact]
    public void User_Transactions_ShouldBe_Mutable()
    {
        // Arrange
        var user = new User();
        var transaction = new Transaction
        {
            Id = 1,
            UserId = user.Id,
            Amount = 100.00m
        };

        // Act
        user.Transactions.Add(transaction);

        // Assert
        user.Transactions.Should().HaveCount(1);
        user.Transactions.Should().Contain(transaction);
    }

    [Fact]
    public void User_Name_ShouldAllow_SpecialCharacters()
    {
        // Arrange
        var names = new[]
        {
            "O'Connor",
            "José García",
            "李明",
            "Müller-Schmidt"
        };

        foreach (var name in names)
        {
            // Act
            var user = new User { Name = name };

            // Assert
            user.Name.Should().Be(name);
        }
    }
}
