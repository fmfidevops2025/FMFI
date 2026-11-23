using MyBlazorApp.DatabaseModel;
using FluentAssertions;
using Xunit;

namespace MyBlazorApp.Tests.Models;

public class TransactionTests
{
    [Fact]
    public void Transaction_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var transaction = new Transaction();

        // Assert
        transaction.Id.Should().Be(0);
        transaction.TransactionTypeId.Should().Be(0);
        transaction.UserId.Should().Be(0);
        transaction.AccountNumber.Should().BeNull();
        transaction.BankCode.Should().BeNull();
        transaction.IssueDate.Should().Be(default(DateTime));
        transaction.Amount.Should().Be(0);
    }

    [Fact]
    public void Transaction_ShouldSet_AllPropertiesCorrectly()
    {
        // Arrange
        var issueDate = new DateTime(2024, 12, 1);
        var transaction = new Transaction
        {
            Id = 1,
            TransactionTypeId = 2,
            UserId = 3,
            AccountNumber = "1234567890",
            BankCode = "0900",
            IssueDate = issueDate,
            Amount = 1500.50m
        };

        // Assert
        transaction.Id.Should().Be(1);
        transaction.TransactionTypeId.Should().Be(2);
        transaction.UserId.Should().Be(3);
        transaction.AccountNumber.Should().Be("1234567890");
        transaction.BankCode.Should().Be("0900");
        transaction.IssueDate.Should().Be(issueDate);
        transaction.Amount.Should().Be(1500.50m);
    }

    [Theory]
    [InlineData("1234567890", "0900")]
    [InlineData("SK1234567890", "0200")]
    [InlineData("987654321", "1100")]
    public void Transaction_ShouldAccept_ValidAccountNumberAndBankCode(string accountNumber, string bankCode)
    {
        // Arrange & Act
        var transaction = new Transaction
        {
            AccountNumber = accountNumber,
            BankCode = bankCode
        };

        // Assert
        transaction.AccountNumber.Should().Be(accountNumber);
        transaction.BankCode.Should().Be(bankCode);
    }

    [Theory]
    [InlineData(0.01)]
    [InlineData(100.00)]
    [InlineData(99999.99)]
    [InlineData(-50.00)]  // Negative amounts for withdrawals
    public void Transaction_Amount_ShouldAccept_VariousDecimalValues(decimal amount)
    {
        // Arrange & Act
        var transaction = new Transaction { Amount = amount };

        // Assert
        transaction.Amount.Should().Be(amount);
    }

    [Fact]
    public void Transaction_IssueDate_ShouldBe_InPast()
    {
        // Arrange
        var pastDate = DateTime.Now.AddDays(-10);
        var transaction = new Transaction { IssueDate = pastDate };

        // Assert
        transaction.IssueDate.Should().BeBefore(DateTime.Now);
    }

    [Fact]
    public void Transaction_ShouldAllow_NavigationProperties()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Test User", Salary = 3000m };
        var transactionType = new TransactionType { Id = 1, Name = "Deposit" };

        // Act
        var transaction = new Transaction
        {
            UserId = user.Id,
            User = user,
            TransactionTypeId = transactionType.Id,
            TransactionType = transactionType
        };

        // Assert
        transaction.User.Should().Be(user);
        transaction.User.Name.Should().Be("Test User");
        transaction.TransactionType.Should().Be(transactionType);
        transaction.TransactionType.Name.Should().Be("Deposit");
    }

    [Fact]
    public void Transaction_WithFutureDate_ShouldBeValid()
    {
        // Arrange
        var futureDate = DateTime.Now.AddDays(30);

        // Act
        var transaction = new Transaction { IssueDate = futureDate };

        // Assert
        transaction.IssueDate.Should().BeAfter(DateTime.Now);
    }
}
