using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MongoDB.Bson;
using Moq;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Application.Places.Commands.UpdatePlace;
using PaaspopService.Application.Places.Queries.GetBestPlacesQuery;
using PaaspopService.Application.Places.Queries.GetMeetingPointQuery;
using PaaspopService.Application.Places.Queries.GetPlacesQuery;
using PaaspopService.Common.Handlers;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.Enumerations;
using PaaspopService.Domain.ValueObjects;
using Xunit;

namespace BusinessLogicTests
{
    public class PlacesLogictests
    {
        [Fact]
        public async void Get_best_places()
        {
            var place = new Place
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            var mapper = new Mock<IMapper>();
            mapper.Setup(m => m.Map<BestPlacesViewModel>(It.IsAny<Dictionary<string, List<BestPlace>>>(), It.IsAny<Action<IMappingOperationOptions>>())).Returns(
                new BestPlacesViewModel
                {
                    BestPlaces = new Dictionary<string, List<BestPlace>>
                    {{
                        place.Type.GetDescription(), new List<BestPlace> { new BestPlace
                        {
                            Place = place,
                            CrowdPercentage = new Percentage(10),
                            Distance = new Distance(20)
                        } } }
                    }
                });

            var placesRepository = new Mock<IPlacesRepository>();
            placesRepository.Setup(repo => repo.GetPlaces()).Returns(Task.FromResult(new List<Place> { place }));

            var getBestPlacesQuery = new GetBestPlacesQuery();
            var getBestPlacesHandler = new GetBestPlacesHandler(mapper.Object, placesRepository.Object);

            var result = await getBestPlacesHandler.Handle(getBestPlacesQuery, default(CancellationToken));
            result.BestPlaces.TryGetValue(place.Type.GetDescription(), out var bestPlaces);
            bestPlaces?[0].Place.Should().Be(place);
        }

        [Fact]
        public async void Get_meeting_point()
        {
            var place = new Place
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Meeting point bij Apollo",
                Type = PlaceType.MeetingPoint,
                Location = new LocationCoordinate(3.4, 4.4)
            };

            var mapper = new Mock<IMapper>();
            var placesRepository = new Mock<IPlacesRepository>();
            placesRepository.Setup(repo => repo.GetPlacesByType(PlaceType.MeetingPoint)).Returns(Task.FromResult(new List<Place> { place }));

            var getMeetingPointQuery = new GetMeetingPointQuery {LocationOfUser = new LocationCoordinate(3.3, 4.4)};
            var getMeetingPointHandler = new GetMeetingPointHandler(mapper.Object, placesRepository.Object);

            var result = await getMeetingPointHandler.Handle(getMeetingPointQuery, default(CancellationToken));
            result.Should().Be(place);
        }

        [Fact]
        public async void Get_places()
        {
            var place = new Place
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            var mapper = new Mock<IMapper>();
            var placesRepository = new Mock<IPlacesRepository>();
            placesRepository.Setup(repo => repo.GetPlaces()).Returns(Task.FromResult(new List<Place> {place}));

            var getPlacesQuery = new GetPlacesQuery();
            var getPlacesHandler = new GetPlacesHandler(mapper.Object, placesRepository.Object);

            var result = await getPlacesHandler.Handle(getPlacesQuery, default(CancellationToken));
            result[0].Should().Be(place);
        }

        [Fact]
        public async void Update_place()
        {
            var place = new Place
            {
                Id = ObjectId.GenerateNewId(DateTime.Now).ToString(),
                CrowdPercentage = new Percentage(80),
                UsersOnPlace = new HashSet<string>(),
                Name = "Vegan burgers",
                Type = PlaceType.Food,
                Location = new LocationCoordinate(3.3, 4.4)
            };

            var mapper = new Mock<IMapper>();

            var updatePlaceCommand = new UpdatePlaceCommand
            {
                PlaceToBeUpdated = place
            };

            var placesRepository = new Mock<IPlacesRepository>();

            var updatePlaceHandler = new UpdatePlaceHandler(mapper.Object, placesRepository.Object);
            await updatePlaceHandler.Handle(updatePlaceCommand, default(CancellationToken));

            placesRepository.Verify(mock =>
                mock.UpdatePlaceAsync(It.Is<Place>(placeParam => Equals(place, placeParam))));
        }
    }
}