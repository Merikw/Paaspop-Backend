using System;
using FluentAssertions;
using PaaspopService.Common.DistanceBetweenCoordinates;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Enumerations;
using Xunit;

namespace GeneralTests
{
    public class Common
    {
        [Fact]
        public void IsBetween()
        {
            // Act
            var resultTrue = BetweenHandler.IsInBetween(4, 1, 10);
            var resultFalse = BetweenHandler.IsInBetween(0, 1, 10);
            var resultTrueNegative = BetweenHandler.IsInBetween(-4, -10, 1);
            var resultFalseMinBoundary = BetweenHandler.IsInBetween(-11, -10, 1);
            var resultFalseMaxBoundary = BetweenHandler.IsInBetween(2, -10, 1);

            // Assert
            resultTrue.Should().Be(true);
            resultFalse.Should().Be(false);
            resultTrueNegative.Should().Be(true);
            resultFalseMinBoundary.Should().Be(false);
            resultFalseMaxBoundary.Should().Be(false);
        }

        [Fact]
        public void GetDistance()
        {
            var result = Convert.ToInt32(DistanceBetweenCoordinates.GetDistanceInMeters(51.441642, 5.4697225, 51.5077637, 5.3978482));

            result.Should().Be(8879);
        }

        [Fact]
        public void GetEnumDescription()
        {
            var result = PlaceType.Bar.GetDescription();

            result.Should().Be("Bars");
        }
    }
}