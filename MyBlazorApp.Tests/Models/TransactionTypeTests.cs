using MyBlazorApp.DatabaseModel;
using FluentAssertions;
using Xunit;

namespace MyBlazorApp.Tests.Models;

public class TransactionTypeTests
{
    [Fact]
    public void TransactionType_ShouldInitialize_WithDefaultValues()
    {
        // Arrange & Act
        var transactionType = new TransactionType();

        // Assert
        transactionType.Id.Should().Be(0);
        transactionType.Name.Should().BeNull();
        transactionType.Transactions.Should().NotBeNull();
        transactionType.Transactions.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Deposit")]
    [InlineData("Withdrawal")]
    [InlineData("Transfer")]
    [InlineData("Payment")]
    public void TransactionType_ShouldSet_ValidNames(string name)
    {
        // Arrange & Act
        var transactionType = new TransactionType
        {
            Id = 1,
            Name = name
        };

        // Assert
        transactionType.Name.Should().Be(name);
    }

    [Fact]
    public void TransactionType_Transactions_ShouldBe_Mutable()
    {
        // Arrange
        var transactionType = new TransactionType { Id = 1, Name = "Deposit" };
        var transaction1 = new Transaction { Id = 1, Amount = 100m };
        var transaction2 = new Transaction { Id = 2, Amount = 200m };

        // Act
        transactionType.Transactions.Add(transaction1);
        transactionType.Transactions.Add(transaction2);

        // Assert
        transactionType.Transactions.Should().HaveCount(2);
        transactionType.Transactions.Should().Contain(transaction1);
        transactionType.Transactions.Should().Contain(transaction2);
    }

    [Fact]
    public void TransactionType_ShouldAllow_MultipleTransactions()
    {
        // Arrange
        var transactionType = new TransactionType { Id = 1, Name = "Withdrawal" };

        // Act
        for (int i = 1; i <= 5; i++)
        {
            transactionType.Transactions.Add(new Transaction
            {
                Id = i,
                Amount = i * 10m,
                TransactionTypeId = transactionType.Id
            });
        }

        // Assert
        transactionType.Transactions.Should().HaveCount(5);
        transactionType.Transactions.Sum(t => t.Amount).Should().Be(150m);
    }
}
