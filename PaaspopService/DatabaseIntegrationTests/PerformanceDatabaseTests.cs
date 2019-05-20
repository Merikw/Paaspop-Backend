using System;
using System.Collections.Generic;
using FluentAssertions;
using MongoDB.Bson;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace DatabaseIntegrationTests
{
    public class PerformanceDatabaseTests : MockMongoDatabase
    {
        [Fact]
        public async void Update_performance()
        {
            var performance = new Performance
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage(),
                UsersFavoritedPerformance = new HashSet<string>()
            };

            var updatedPerformance = new Performance
            {
                Id = performance.Id,
                PerformanceTime = performance.PerformanceTime,
                Artist = performance.Artist,
                InterestPercentage = new Percentage(75),
                PerformanceId = performance.PerformanceId,
                Stage = performance.Stage,
                UsersFavoritedPerformance = performance.UsersFavoritedPerformance
            };

            await PerformancesRepositoryMongoDb.InsertPerformance(performance);
            var resultInsertedPerformance = await PerformancesRepositoryMongoDb.GetPerformanceById(performance.Id);

            await PerformancesRepositoryMongoDb.UpdatePerformance(updatedPerformance);
            var resultUpdatedPerformance = await PerformancesRepositoryMongoDb.GetPerformanceById(performance.Id);

            resultInsertedPerformance.Should().Be(performance);
            resultUpdatedPerformance.Should().Be(updatedPerformance);
        }

        [Fact]
        public async void Get_performances()
        {
            var performance = new Performance
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage(),
            };

            await PerformancesRepositoryMongoDb.InsertPerformance(performance);

            var result = await PerformancesRepositoryMongoDb.GetPerformances();
            result?[0].Should().Be(performance);
        }

        [Fact]
        public async void Get_performance_by_id()
        {
            var performance = new Performance
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage(),
            };

            await PerformancesRepositoryMongoDb.InsertPerformance(performance);

            var result = await PerformancesRepositoryMongoDb.GetPerformanceById(performance.Id);
            result.Should().Be(performance);
        }

        [Fact]
        public async void Get_performance_by_performance_id()
        {
            var performance = new Performance
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage(),
            };

            await PerformancesRepositoryMongoDb.InsertPerformance(performance);

            var result = await PerformancesRepositoryMongoDb.GetPerformanceByPerformanceId(performance.PerformanceId.ToString());
            result.Should().Be(performance);
        }

        [Fact]
        public async void Insert_performance()
        {
            var performance = new Performance
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                PerformanceTime = new PerformanceTime(5, "20:00", "21:00"),
                Artist = new Artist(),
                InterestPercentage = new Percentage(80),
                PerformanceId = 8083,
                Stage = new Stage(),
            };

            await PerformancesRepositoryMongoDb.InsertPerformance(performance);

            var result = await PerformancesRepositoryMongoDb.GetPerformanceById(performance.Id);
            result.Should().Be(performance);
        }
    }
}
