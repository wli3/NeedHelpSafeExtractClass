using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FeedChecker
{
    public static class Feed
    {
        private static string _feed;

        public static IEnumerable<string> GetAllCoreFxPreview1(string feed)
        {
            _feed = feed;
            if (!feed.EndsWith("/"))
                feed = feed + "/";

            var packageUrl = feed + "main/binary-amd64/Packages";
            var request = (HttpWebRequest) WebRequest.Create(packageUrl);

            var result = "";
            var response = request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            var lines = result.Split(
                new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None);

            var onlyCoreFxPreview1ButWithPackgeInFront =
                lines.Where(l => l.StartsWith("Package: dotnet-hostfxr-2.0.0-preview1"));

            // imagine there is more code

            return onlyCoreFxPreview1ButWithPackgeInFront.Select(l => l.Replace("Package: ", ""));
        }
    }
}