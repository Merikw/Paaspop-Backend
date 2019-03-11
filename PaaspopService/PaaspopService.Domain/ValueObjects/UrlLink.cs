using System;
using System.Collections.Generic;
using PaaspopService.Domain.Exceptions;
using PaaspopService.Domain.Infrastructure;

namespace PaaspopService.Domain.ValueObjects
{
    public class UrlLink : ValueObject
    {
        private UrlLink()
        {
        }

        public UrlLink(string urlText)
        {
            if (Uri.TryCreate(urlText, UriKind.Absolute, out var url)
                && (url.Scheme == Uri.UriSchemeHttp || url.Scheme == Uri.UriSchemeHttps))
                UrlText = urlText;
            else
                throw new UrlLinkInvalidException(urlText);
        }

        public string UrlText;

        public static implicit operator string(UrlLink urlLink)
        {
            return urlLink.ToString();
        }

        public static explicit operator UrlLink(string url)
        {
            return new UrlLink(url);
        }

        public override string ToString()
        {
            return UrlText;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UrlText;
        }
    }
}