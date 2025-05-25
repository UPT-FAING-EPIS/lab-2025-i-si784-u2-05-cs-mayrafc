using Bank.Domain;
using System;
using Xunit;

namespace Bank.Domain.Tests
{
    public class BankAccountTests
    {
        [Theory]
        [InlineData(11.99, 4.55, 7.44)]
        [InlineData(12.3, 5.2, 7.1)]
        public void MultiDebit_WithValidAmount_UpdatesBalance(double beginningBalance, double debitAmount, double expected)
        {
            var account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            account.Debit(debitAmount);
            double actual = account.Balance;
            Assert.Equal(Math.Round(expected, 2), Math.Round(actual, 2));
        }

        [Fact]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            var account = new BankAccount("Mr. Bryan Walton", 11.99);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(-100.00));
        }

        [Fact]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            var account = new BankAccount("Mr. Bryan Walton", 11.99);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(20.0));
            Assert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, ex.Message);
        }

        [Fact]
        public void Debit_WhenAmountIsZero_ShouldNotChangeBalance()
        {
            var account = new BankAccount("User", 50);
            account.Debit(0);
            Assert.Equal(50, account.Balance);
        }

        [Fact]
        public void Debit_WhenAmountEqualsBalance_ShouldSetBalanceToZero()
        {
            var account = new BankAccount("User", 20);
            account.Debit(20);
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void Debit_WhenBalanceIsZero_ShouldThrowArgumentOutOfRange()
        {
            var account = new BankAccount("User", 0);
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(1));
            Assert.Contains(BankAccount.DebitAmountExceedsBalanceMessage, ex.Message);
        }

        [Fact]
        public void Constructor_WhenOwnerIsNull_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new BankAccount(null, 10));
        }

        [Fact]
        public void Constructor_WhenOwnerIsEmpty_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new BankAccount("", 10));
        }

        [Fact]
        public void Debit_WhenAmountIsPositiveInfinity_ShouldThrowArgumentOutOfRange()
        {
            var account = new BankAccount("User", 1000);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Debit(double.PositiveInfinity));
        }

        // Tests para Credit

        [Fact]
        public void Credit_WithValidAmount_IncreasesBalance()
        {
            var account = new BankAccount("User", 100);
            account.Credit(50);
            Assert.Equal(150, account.Balance);
        }

        [Fact]
        public void Credit_WhenAmountIsNegative_ShouldThrowArgumentOutOfRange()
        {
            var account = new BankAccount("User", 100);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(-10));
        }

        [Fact]
        public void Credit_WhenAmountIsPositiveInfinity_ShouldThrowArgumentOutOfRange()
        {
            var account = new BankAccount("User", 100);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(double.PositiveInfinity));
        }

        [Fact]
        public void Credit_WhenAmountIsNaN_ShouldThrowArgumentOutOfRange()
        {
            var account = new BankAccount("User", 100);
            Assert.Throws<ArgumentOutOfRangeException>(() => account.Credit(double.NaN));
        }
    }
}
