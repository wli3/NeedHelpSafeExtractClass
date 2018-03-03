using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FeedChecker
{
    public class Feed
    {
        private readonly string _feed;

        public Feed(string feed)
        {
            _feed = feed;
        }

        public IEnumerable<string> GetAllCoreFxPreview1()
        {
            var packageUrl = FormPackageUrl();
            var request = (HttpWebRequest) WebRequest.Create(packageUrl);

            var result = GetResultByCallFeed(request);
            var lines = SplitResult(result);

            return FindAllCoreFx20Preview1InTheFeedResult(lines);
        }

        private static IEnumerable<string> FindAllCoreFx20Preview1InTheFeedResult(string[] lines)
        {
            var onlyCoreFxPreview1ButWithPackgeInFront =
                lines.Where(l => l.StartsWith("Package: dotnet-hostfxr-2.0.0-preview1"));

            // imagine there is more code

            return onlyCoreFxPreview1ButWithPackgeInFront.Select(l => l.Replace("Package: ", ""));
        }

        private static string[] SplitResult(string result)
        {
            var lines = result.Split(
                new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None);
            return lines;
        }

        private static string GetResultByCallFeed(HttpWebRequest request)
        {
            var result = "";
            var response = request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            return result;
        }

        private string FormPackageUrl()
        {
            var feed = _feed;
            if (!feed.EndsWith("/"))
                feed = feed + "/";

            var packageUrl = feed + "main/binary-amd64/Packages";
            return packageUrl;
        }
    }
}