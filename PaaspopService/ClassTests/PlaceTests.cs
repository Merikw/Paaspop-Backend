using FluentAssertions;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace ClassTests
{
    public class PlaceTests
    {
        [Fact]
        public void CalculateCrowdPercentage_correct_plus()
        {
            var place = new Place
            {
                CrowdPercentage = new Percentage(12, 100)
            };

            for (var i = 0; i < 12; i++) place.UsersOnPlace.Add(i.ToString());

            var result = place.CalculateCrowdPercentage(100, 1, Operator.Plus);
            result.AbsolutePercentage.Should().Be(13);
        }

        [Fact]
        public void CalculateCrowdPercentage_wrong_exception_plus()
        {
            var place = new Place
            {
                CrowdPercentage = new Percentage(12, 100)
            };

            for (var i = 0; i < 12; i++) place.UsersOnPlace.Add(i.ToString());

            Assert.Throws<PercentageInvalidException>(() => place.CalculateCrowdPercentage(1, 1, Operator.Plus));
        }

        [Fact]
        public void CalculateCrowdPercentage_wrong_null_pointer()
        {
            var place = new Place
            {
                CrowdPercentage = new Percentage(12, 100)
            };

            for (var i = 0; i < 12; i++) place.UsersOnPlace.Add(i.ToString());

            Assert.Throws<PercentageInvalidException>(() => place.CalculateCrowdPercentage(0, 1, Operator.Plus));
        }
        [Fact]
        public void CalculateCrowdPercentage_correct_minus()
        {
            var place = new Place
            {
                CrowdPercentage = new Percentage(12, 100)
            };

            for (var i = 0; i < 12; i++) place.UsersOnPlace.Add(i.ToString());

            var result = place.CalculateCrowdPercentage(100, 1, Operator.Minus);
            result.AbsolutePercentage.Should().Be(11);
        }

        [Fact]
        public void CalculateCrowdPercentage_wrong_exception_minus()
        {
            var place = new Place
            {
                CrowdPercentage = new Percentage(12, 100)
            };

            for (var i = 0; i < 12; i++) place.UsersOnPlace.Add(i.ToString());

            Assert.Throws<PercentageInvalidException>(() => place.CalculateCrowdPercentage(1, 1, Operator.Minus));
        }
    }
}
