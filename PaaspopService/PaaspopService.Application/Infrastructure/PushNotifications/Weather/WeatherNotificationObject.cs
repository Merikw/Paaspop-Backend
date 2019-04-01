using System;
using System.Collections.Generic;
using System.Text;

namespace PaaspopService.Application.Infrastructure.PushNotifications.Weather
{
    public class WeatherNotificationObject
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private WeatherNotificationObject() { }

        public WeatherNotificationObject(int descriptionId, int temperature, int timeInMilliseconds)
        {
            Title = $"Om {GetHourFromMilliSeconds(timeInMilliseconds)} uur wordt het {Convert.ToString(temperature)} graden met {GetDescriptionFromId(descriptionId)}";
            Description = GetTitleFromIdAndTemperature(descriptionId, temperature);
        }

        private string GetHourFromMilliSeconds(int milliseconds)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(milliseconds).ToLocalTime();
            return dtDateTime.Hour.ToString();
        }

        private string GetTitleFromIdAndTemperature(int id, int temperature)
        {
            var firstDiget = Convert.ToString(id)[0];
            string title;

            if (temperature < 14)
            {
                title = "Kleed je warm aan";
            } else if (temperature >= 14 && temperature < 20)
            {
                title = "Houd warme kleren bij de hand";
            }
            else
            {
                title = "Het wordt warm";
            }

            switch (firstDiget)
            {
                case '2':
                    return title + ", kijk goed uit en pak je regenjas erbij!";
                case '3':
                case '5':
                case '6':
                    return title + " en pak je regenjas erbij!";
                case '7':
                    return title + " en kijk goed uit";
                case '8':
                    return title + " en smeer je in tegen het mogelijke verbranden bij zon";
                default:
                    return title + ".";
            }
        }

        private string GetDescriptionFromId(int id)
        {
            switch (id)
            {
                case 200:
                    return "onweersbuien en lichte regen";
                case 201:
                    return "onweersbuien en regen";
                case 202:
                    return "onweersbuien en hevige regen";
                case 210:
                    return "lichte onweersbuien";
                case 211:
                case 221:
                    return "onweersbuien";
                case 212:
                    return "hevige onweersbuien";
                case 230:
                    return "onweersbuien en lichte motregen";
                case 231:
                    return "onweersbuien en motregen";
                case 232:
                    return "onweersbuien en hevige motregen";
                case 300:
                case 310:
                    return "lichte motregen";
                case 301:
                case 311:
                    return "motregen";
                case 302:
                case 312:
                case 313:
                case 314:
                case 321:
                    return "hevige motregen";
                case 500:
                    return "lichte regen";
                case 501:
                case 520:
                    return "matige regen";
                case 502:
                case 522:
                case 531:
                    return "hevige regen";
                case 503:
                    return "zeer hevige regen";
                case 504:
                    return "extreme regen";
                case 511:
                    return "ijskoude regen";
                case 600:
                    return "lichte sneeuw";
                case 601:
                    return "sneeuw";
                case 602:
                    return "hevige sneeuw";
                case 611:
                    return "ijzel";
                case 612:
                    return "lichte regen en ijzel";
                case 613:
                    return "regen en ijzel";
                case 615:
                case 620:
                    return "lichte regen en sneeuw";
                case 616:
                case 621:
                    return "regen en sneeuw";
                case 622:
                    return "hevige regen en sneeuw";
                case 701:
                case 741:
                    return "mist";
                case 711:
                    return "rook in de lucht";
                case 721:
                    return "nevel";
                case 731:
                    return "zand of stof wervelingen";
                case 751:
                    return "zand in de lucht";
                case 761:
                    return "stof in de lucht";
                case 762:
                    return "vulkanisch as in de lucht";
                case 771:
                    return "rukwinden";
                case 781:
                    return "een tornado";
                case 800:
                    return "blauwe lucht";
                case 801:
                    return "een paar wolkjes";
                case 802:
                    return "hier en daar een wolk";
                case 803:
                    return "wolken en af en toe zon";
                case 804:
                    return "een bewolkte lucht";
                default:
                    return "een bewolkte lucht";
            }
        }
    }
}
