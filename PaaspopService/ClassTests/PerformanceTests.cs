using FluentAssertions;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace ClassTests
{
    public class PerformanceTests
    {
        [Fact]
        public void CalculateInterestPercentage_correct_minus()
        {
            var performance = new Performance
            {
                InterestPercentage = new Percentage(12, 100)
            };

            for (var i = 0; i < 12; i++) performance.UsersFavoritedPerformance.Add(i.ToString());

            var result = performance.CalculateInterestPercentage(100, 1, Operator.Minus);
            result.AbsolutePercentage.Should().Be(11);
        }

        [Fact]
        public void CalculateInterestPercentage_correct_plus()
        {
            var performance = new Performance
            {
                InterestPercentage = new Percentage(12, 100)
            };

            for (var i = 0; i < 12; i++) performance.UsersFavoritedPerformance.Add(i.ToString());

            var result = performance.CalculateInterestPercentage(100, 1, Operator.Plus);
            result.AbsolutePercentage.Should().Be(13);
        }

        [Fact]
        public void CalculateInterestPercentage_wrong_exception_plus()
        {
            var performance = new Performance
            {
                InterestPercentage = new Percentage(12, 100)
            };

            for (var i = 0; i < 12; i++) performance.UsersFavoritedPerformance.Add(i.ToString());

            Assert.Throws<PercentageInvalidException>(
                () => performance.CalculateInterestPercentage(1, 1, Operator.Plus));
        }
    }
}