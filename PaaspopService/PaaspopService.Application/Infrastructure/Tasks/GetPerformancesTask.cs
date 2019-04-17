﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using PaaspopService.Application.Infrastructure.Repositories;
using PaaspopService.Domain.Entities;
using PaaspopService.Domain.ValueObjects;
using static System.String;

namespace PaaspopService.Application.Infrastructure.Tasks
{
    public static class GetPerformancesTask
    {
        private static readonly List<string> genres = new List<string> {"rap", "trap", "hip hop"};
        private const string REGEX = @"[^a-zA-Z]+";

        public static async Task<IWebHost> GetPerformances(this IWebHost webHost)
        {
            var serviceScopeFactory = (IServiceScopeFactory) webHost.Services.GetService(typeof(IServiceScopeFactory));
            var accessToken = await GetSpotifyAccesstoken();
            genres.AddRange(await GetGenres(accessToken));
            var summariesAndArtists = GetSummariesOfArtists();

            using (var scope = serviceScopeFactory.CreateScope())
            {
                var services = scope.ServiceProvider;
                var performancesRepository = services.GetRequiredService<IPerformancesRepository>();

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync("https://www.paaspop.nl/json/timetable");
                    response.EnsureSuccessStatusCode();
                    dynamic jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                    dynamic[] days =
                        {jsonObject.timetable.Vrijdag, jsonObject.timetable.Zaterdag, jsonObject.timetable.Zondag};
                    foreach (var day in days)
                    foreach (var stage in day)
                    foreach (var performance in stage.acts)
                    {
                        if (performance.id == null) continue;
                        if (await performancesRepository.GetPerformanceByPerformanceId((string) performance.id) !=
                            null) continue;
                        var dayOfWeek = 0;
                        var startTime = (string) performance.start;
                        var endTime = (string) performance.end;
                        var photo = (string) performance.photo;
                        var stageName = (string) stage.title;
                        var artistNameStrangeChars = (string) performance.title;
                        var artistName = artistNameStrangeChars.Contains("&#8217;")
                            ? artistNameStrangeChars.Replace("&#8217;", "'")
                            : artistNameStrangeChars.Contains("&amp;")
                                ? artistNameStrangeChars.Replace("&amp;", "&")
                                : artistNameStrangeChars;
                        summariesAndArtists.TryGetValue(Regex.Replace(artistName.ToLowerInvariant().Trim(), REGEX,""),
                            out var summary);
                        if (artistName.Contains("Rage"))
                        {
                            var test = Regex.Replace(artistName.ToLowerInvariant().Trim(), REGEX, "");
                        }
                    
                        if (IsNullOrEmpty(summary))
                            foreach (var (artistNameText, summaryText) in summariesAndArtists)
                            {
                                if (!Regex.Replace(artistNameText.ToLowerInvariant().Trim(), REGEX, "")
                                        .Contains(Regex.Replace(artistName.ToLowerInvariant().Trim(), REGEX,
                                            ""))) continue;
                                summary = summaryText;
                                break;
                            }

                        switch ((string) performance.day)
                        {
                            case "Vrijdag":
                                dayOfWeek = 5;
                                break;
                            case "Zaterdag":
                                dayOfWeek = 6;
                                break;
                            case "Zondag":
                                dayOfWeek = 7;
                                break;
                            default:
                                dayOfWeek = 5;
                                break;
                        }

                        var newPerformance = new Performance
                        {
                            PerformanceId = performance.id,
                            Artist = new Artist
                            {
                                Genres = new HashSet<string>(),
                                ImageLink = photo != null
                                    ? new UrlLink("https:" + (string) performance.photo)
                                    : null,
                                Name = artistName,
                                Summary = IsNullOrEmpty(summary)
                                    ? "Er is geen beschrijving voor deze artiest gevonden."
                                    : summary
                            },
                            InterestPercentage = new Percentage(0),
                            PerformanceTime = IsNullOrEmpty(startTime) || IsNullOrEmpty(endTime)
                                ? null
                                : new PerformanceTime(dayOfWeek, startTime, endTime),
                            Stage = new Stage
                            {
                                Name = stageName.Contains("&#8217;") ? stageName.Replace("&#8217;", "'") : stageName
                            }
                        };

                        newPerformance = await SetGenresOfArtist(newPerformance, accessToken);

                        await performancesRepository.InsertPerformance(newPerformance);
                    }
                }
            }

            return webHost;
        }

        private static async Task<string> GetSpotifyAccesstoken()
        {
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", "234cb87a85c94b959117b117cae713ce"),
                new KeyValuePair<string, string>("client_secret",
                    Environment.GetEnvironmentVariable("SPOTIFY_WEB_API_CLIENT_SECRET")),
                new KeyValuePair<string, string>("refresh_token",
                    Environment.GetEnvironmentVariable("SPOTIFY_WEB_API_REFRESH_TOKEN")),
                new KeyValuePair<string, string>("redirect_uri", "https://example.com/callback")
            };
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
                    {Content = new FormUrlEncodedContent(postData)};
                var response = await client.SendAsync(request);
                dynamic jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                return jsonObject.access_token;
            }
        }

        private static async Task<List<string>> GetGenres(string accessToken)
        {
            var genres = new List<string>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response =
                    await client.GetAsync("https://api.spotify.com/v1/recommendations/available-genre-seeds");
                if (response.StatusCode != HttpStatusCode.OK) return genres;
                dynamic jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                foreach (var genre in jsonObject.genres) genres.Add((string) genre);
            }

            return genres;
        }

        private static async Task<Performance> SetGenresOfArtist(Performance performance, string accessToken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var response = await client.GetAsync("https://api.spotify.com/v1/search?q=" +
                                                         performance.Artist.Name + "&type=artist&limit=1");
                    if (response.StatusCode != HttpStatusCode.OK) return performance;
                    dynamic jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());
                    var items = jsonObject.artists.items;
                    foreach (var item in items)
                    foreach (var genre in item.genres)
                    {
                        if (performance.Artist.Genres.Count >= 3) continue;
                        var genreText = (string) genre;
                        var foundGenres = genres.Where(g => genreText.Contains(g)).ToList();
                        if (foundGenres.Count <= 0) continue;
                        if (foundGenres.Count == 1)
                            performance.Artist.Genres.Add(char.ToUpper(foundGenres[0][0]) +
                                                          foundGenres[0].Substring(1));
                        else
                            foreach (var foundGenre in foundGenres)
                                performance.Artist.Genres.Add(char.ToUpper(foundGenre[0]) + foundGenre.Substring(1));

                        if (genreText.Contains("dutch")) performance.Artist.Genres.Add("Nederlands");
                    }

                    return performance;
                }
            }
            catch (HttpRequestException exception)
            {
                if (exception.InnerException.GetType() == typeof(IOException))
                    return await SetGenresOfArtist(performance, accessToken);
                return performance;
            }
        }

        private static Dictionary<string, string> GetSummariesOfArtists()
        {
            var path = "";
            try
            {
                path = Path.Combine(Directory.GetCurrentDirectory(),
                    "../PaaspopService.Application/Infrastructure/assets/artiesten.docx");
                var wordPackage = Package.Open(path, FileMode.Open, FileAccess.Read);
                var artistsWithSummary = new Dictionary<string, string>();
                using (var wordDocument = WordprocessingDocument.Open(wordPackage))
                {
                    var paragraphs = wordDocument.MainDocumentPart.Document.Body.Elements<Paragraph>();
                    var first = true;
                    var artistName = "";
                    foreach (var paragraph in paragraphs)
                    {
                        var text = paragraph.InnerText;
                        if (IsNullOrEmpty(text)) continue;
                        if (first)
                        {
                            artistName = text;
                            first = false;
                            continue;
                        }

                        if (artistsWithSummary.ContainsKey(Regex.Replace(artistName.ToLowerInvariant().Trim(), REGEX, "")))
                        {
                            first = true;
                            continue;
                        }

                        artistsWithSummary.Add(Regex.Replace(artistName.ToLowerInvariant().Trim(), REGEX, ""), text);
                        first = true;
                    }
                }

                return artistsWithSummary;
            }
            catch (Exception e)
            {
                var dict = new Dictionary<string, string>();
                var subdirstring = Directory.GetDirectories(Directory.GetCurrentDirectory()).Aggregate("", (current, subdir) => current + " - " + subdir);
                dict.Add("kraantjepappie", "Parent: " + Directory.GetParent(Directory.GetCurrentDirectory()) + " parent parent " + Directory.GetParent(Directory.GetCurrentDirectory()).Parent + " subs: " + subdirstring);
                return dict;
            }
        }
    }
}