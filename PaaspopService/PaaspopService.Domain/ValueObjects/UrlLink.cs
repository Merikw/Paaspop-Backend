using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;
using System;
using System.Collections.Generic;

namespace PaaspopService.Domain.ValueObjects
{
    public class UrlLink : ValueObject
    {
        public string UrlText { get; private set; }
        public Uri Url { get; private set; }

        private UrlLink() { }

        public static UrlLink Create(string urlText)
        {
            Uri url;
            if (Uri.TryCreate(urlText, UriKind.Absolute, out url)
                && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps))
            {
                return new UrlLink
                {
                    UrlText = urlText,
                    Url = url
                };
            }
            throw new UrlLinkInvalidException(urlText);
        }

        public static implicit operator string(UrlLink urlLink)
        {
            return urlLink.ToString();
        }

        public static explicit operator UrlLink(string url)
        {
            return Create(url);
        }

        public override string ToString()
        {
            return UrlText;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Url;
            yield return UrlText;
        }
    }
}
