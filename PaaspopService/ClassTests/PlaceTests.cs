using System;
using FluentAssertions;
using MongoDB.Bson;
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
        public void CalculateCrowdPercentage_correct()
        {
            var place = new Place
            {
                Name = "Wc naast Apollo",
                CrowdPercentage = new Percentage(12, 100),
                Id = ObjectId.GenerateNewId().ToString(),
                Location = new LocationCoordinate(5.5, 51.5),
                Type = PlaceType.Toilet
            };

            var result = place.CalculateCrowdPercentage(100, 1);
            result.AbsolutePercentage.Should().Be(13);
        }

        [Fact]
        public void CalculateCrowdPercentage_wrong_exception()
        {
            var place = new Place
            {
                Name = "Wc naast Apollo",
                CrowdPercentage = new Percentage(12, 100),
                Id = ObjectId.GenerateNewId().ToString(),
                Location = new LocationCoordinate(5.5, 51.5),
                Type = PlaceType.Toilet
            };

            Assert.Throws<PercentageInvalidException>(() => place.CalculateCrowdPercentage(1, 1));
        }

        [Fact]
        public void CalculateCrowdPercentage_wrong_null_pointer()
        {
            var place = new Place
            {
                Name = "Wc naast Apollo",
                CrowdPercentage = new Percentage(12, 100),
                Id = ObjectId.GenerateNewId().ToString(),
                Location = new LocationCoordinate(5.5, 51.5),
                Type = PlaceType.Toilet
            };

            Assert.Throws<PercentageInvalidException>(() => place.CalculateCrowdPercentage(0, 1));
        }
    }
}
