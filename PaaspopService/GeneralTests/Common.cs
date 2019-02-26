using System;
using FluentAssertions;
using PaaspopService.Common.Handlers;
using Xunit;

namespace GeneralTests
{
    public class Common
    {
        [Fact]
        public void IsBetween()
        {
            // Act
            bool resultTrue = BetweenHandler.IsInBetween(4, 1, 10);
            bool resultFalse = BetweenHandler.IsInBetween(0, 1, 10);
            bool resultTrueNegative = BetweenHandler.IsInBetween(-4, -10, 1);
            bool resultFalseNegative = BetweenHandler.IsInBetween(-11, -10, 1);
            bool resultFalseMinBoundary = BetweenHandler.IsInBetween(-10, -10, 1);
            bool resultFalseMaxBoundary = BetweenHandler.IsInBetween(1, -10, 1);

            // Assert
            resultTrue.Should().Be(true);
            resultFalse.Should().Be(false);
            resultTrueNegative.Should().Be(true);
            resultFalseNegative.Should().Be(false);
            resultFalseMinBoundary.Should().Be(false);
            resultFalseMaxBoundary.Should().Be(false);
        }
    }
}
