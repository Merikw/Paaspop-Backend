using System.Collections.Generic;
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
        public void CalculateInterestPercentage_correct_plus()
        {
            var performance = new Performance()
            {
                InterestPercentage = new Percentage(12, 100)
            };

            var result = performance.CalculateInterestPercentage(100, 1, Operator.Plus);
            result.AbsolutePercentage.Should().Be(13);
        }

        [Fact]
        public void CalculateInterestPercentage_wrong_exception_plus()
        {
            var performance = new Performance()
            {
                InterestPercentage = new Percentage(12, 100)
            };

            Assert.Throws<PercentageInvalidException>(() => performance.CalculateInterestPercentage(1, 1, Operator.Plus));
        }

        [Fact]
        public void CalculateInterestPercentage_wrong_null_pointer()
        {
            var performance = new Performance()
            {
                InterestPercentage = new Percentage(12, 100)
            };

            Assert.Throws<PercentageInvalidException>(() => performance.CalculateInterestPercentage(0, 1, Operator.Plus));
        }

        [Fact]
        public void CalculateInterestPercentage_correct_minus()
        {
            var performance = new Performance()
            {
                InterestPercentage = new Percentage(12, 100)
            };

            var result = performance.CalculateInterestPercentage(100, 1, Operator.Minus);
            result.AbsolutePercentage.Should().Be(11);
        }
    }
}
