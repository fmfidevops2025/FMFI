using MyBlazorApp.DatabaseModel;
using FluentAssertions;
using Xunit;

namespace MyBlazorApp.Tests.BusinessLogic;

/// <summary>
/// Tests for business logic scenarios involving users and their transactions
/// </summary>
public class UserFinancialTests
{
    [Fact]
    public void User_TotalTransactionAmount_ShouldCalculate_Correctly()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "John Doe",
            Salary = 3000m
        };

        user.Transactions.Add(new Transaction { Amount = 500m });
        user.Transactions.Add(new Transaction { Amount = -200m });
        user.Transactions.Add(new Transaction { Amount = 150m });

        // Act
        var totalAmount = user.Transactions.Sum(t => t.Amount);

        // Assert
        totalAmount.Should().Be(450m);
    }

    [Fact]
    public void User_WithNoTransactions_ShouldHave_ZeroTotal()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Name = "Jane Smith",
            Salary = 2500m
        };

        // Act
        var totalAmount = user.Transactions.Sum(t => t.Amount);

        // Assert
        totalAmount.Should().Be(0);
        user.Transactions.Should().BeEmpty();
    }

    [Theory]
    [InlineData(3000, 500, 2500)]
    [InlineData(5000, 1000, 4000)]
    [InlineData(2000, 2000, 0)]
    public void User_AfterExpense_ShouldHave_CorrectBalance(decimal salary, decimal expense, decimal expectedBalance)
    {
        // Arrange
        var user = new User { Salary = salary };
        user.Transactions.Add(new Transaction { Amount = -expense });

        // Act
        var balance = user.Salary + user.Transactions.Sum(t => t.Amount);

        // Assert
        balance.Should().Be(expectedBalance);
    }

    [Fact]
    public void User_TransactionsByMonth_ShouldGroup_Correctly()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Test User", Salary = 3000m };

        user.Transactions.Add(new Transaction
        {
            Amount = 100m,
            IssueDate = new DateTime(2024, 1, 15)
        });
        user.Transactions.Add(new Transaction
        {
            Amount = 200m,
            IssueDate = new DateTime(2024, 1, 20)
        });
        user.Transactions.Add(new Transaction
        {
            Amount = 300m,
            IssueDate = new DateTime(2024, 2, 10)
        });

        // Act
        var groupedByMonth = user.Transactions
            .GroupBy(t => new { t.IssueDate.Year, t.IssueDate.Month })
            .Select(g => new
            {
                Month = g.Key,
                Total = g.Sum(t => t.Amount),
                Count = g.Count()
            })
            .ToList();

        // Assert
        groupedByMonth.Should().HaveCount(2);
        groupedByMonth.First(g => g.Month.Month == 1).Total.Should().Be(300m);
        groupedByMonth.First(g => g.Month.Month == 1).Count.Should().Be(2);
        groupedByMonth.First(g => g.Month.Month == 2).Total.Should().Be(300m);
        groupedByMonth.First(g => g.Month.Month == 2).Count.Should().Be(1);
    }

    [Fact]
    public void User_AverageDailyExpense_ShouldCalculate_ForNegativeTransactions()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Test User", Salary = 3000m };

        user.Transactions.Add(new Transaction { Amount = -50m });   // Expense
        user.Transactions.Add(new Transaction { Amount = -100m });  // Expense
        user.Transactions.Add(new Transaction { Amount = 200m });   // Income (should be excluded)
        user.Transactions.Add(new Transaction { Amount = -30m });   // Expense

        // Act
        var expenses = user.Transactions.Where(t => t.Amount < 0).ToList();
        var averageExpense = expenses.Any() ? Math.Abs(expenses.Average(t => t.Amount)) : 0;

        // Assert
        expenses.Should().HaveCount(3);
        averageExpense.Should().Be(60m); // (50 + 100 + 30) / 3 = 60
    }

    [Fact]
    public void User_MonthlyBudget_ShouldDetect_OverSpending()
    {
        // Arrange
        var user = new User { Id = 1, Name = "Test User", Salary = 2000m };
        var monthlyBudget = 1500m;

        user.Transactions.Add(new Transaction { Amount = -800m });
        user.Transactions.Add(new Transaction { Amount = -600m });
        user.Transactions.Add(new Transaction { Amount = -200m });

        // Act
        var totalExpenses = Math.Abs(user.Transactions.Where(t => t.Amount < 0).Sum(t => t.Amount));
        var isOverBudget = totalExpenses > monthlyBudget;

        // Assert
        totalExpenses.Should().Be(1600m);
        isOverBudget.Should().BeTrue();
    }
}
