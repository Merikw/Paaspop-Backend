using System;

//Source: https://stackoverflow.com/questions/365826/calculate-distance-between-2-gps-coordinates

namespace PaaspopService.Common.DistanceBetweenCoordinates
{
    public class DistanceBetweenCoordinates
    {
        private DistanceBetweenCoordinates()
        {
        }

        public static double GetDistanceInMeters(double lat1, double lon1, double lat2, double lon2)
        {
            var earthRadiusKm = 6371;

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return (earthRadiusKm * c) * 1000;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}