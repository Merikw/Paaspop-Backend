using System;
using System.Collections.Generic;
using FluentAssertions;
using MongoDB.Bson;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace DatabaseIntegrationTests
{
    public class PlaceDatabaseTests : MockMongoDatabase
    {
        [Fact]
        public async void Update_place()
        {
            var place = new Place()
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            var updatedPlace = new Place()
            {
                Id = place.Id,
                CrowdPercentage = new Percentage(75),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            await PlacesRepositoryMongoDb.InsertPlace(place);
            var insertedPlace = await PlacesRepositoryMongoDb.GetPlaceById(place.Id);

            await PlacesRepositoryMongoDb.UpdatePlaceAsync(updatedPlace);
            var result = await PlacesRepositoryMongoDb.GetPlaceById(place.Id);

            insertedPlace.Should().Be(place);
            result.Should().Be(updatedPlace);
        }

        [Fact]
        public async void Insert_place()
        {
            var place = new Place()
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            await PlacesRepositoryMongoDb.InsertPlace(place);
            var result = await PlacesRepositoryMongoDb.GetPlaceById(place.Id);

            result.Should().Be(place);  
        }

        [Fact]
        public async void Get_place_by_id()
        {
            var place = new Place()
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            await PlacesRepositoryMongoDb.InsertPlace(place);
            var result = await PlacesRepositoryMongoDb.GetPlaceById(place.Id);

            result.Should().Be(place);
        }

        [Fact]
        public async void Get_places_by_type()
        {
            var place = new Place()
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            await PlacesRepositoryMongoDb.InsertPlace(place);
            var result = await PlacesRepositoryMongoDb.GetPlacesByType(place.Type);

            result[0].Should().Be(place);
        }

        [Fact]
        public async void Get_places()
        {
            var place = new Place()
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            await PlacesRepositoryMongoDb.InsertPlace(place);
            var result = await PlacesRepositoryMongoDb.GetPlaces();

            result[0].Should().Be(place);
        }
    }
}
