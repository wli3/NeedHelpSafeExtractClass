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
        private readonly string _requestUriString;

        public Feed(string feed)
        {
            if (!feed.EndsWith("/"))
                feed = feed + "/";

            var packageUrl = feed + "main/binary-amd64/Packages";
            _requestUriString = packageUrl;
        }

        public string DownloadFeed()
        {
            var request = (HttpWebRequest) WebRequest.Create(_requestUriString);

            var result = "";
            var response = request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                result = reader.ReadToEnd();
            }

            return result;
        }
    }

    public class FeedParser
    {
        public FeedParser(string result)
        {
            Result = result;
        }

        public string Result { get; private set; }

        public IEnumerable<string> Parse()
        {
            var lines = Result.Split(
                new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None);

            var onlyCoreFxPreview1ButWithPackgeInFront =
                lines.Where(l => l.StartsWith("Package: dotnet-hostfxr-2.0.0-preview1"));

            // imagine there is more code

            var allCoreFxPreview = onlyCoreFxPreview1ButWithPackgeInFront.Select(l => l.Replace("Package: ", ""));
            return allCoreFxPreview;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var result = new Feed("http://apt-mo.trafficmanager.net/repos/dotnet/dists/jessie").DownloadFeed();
            foreach (var l in new FeedParser(result).Parse())
                Console.WriteLine(l);
        }
    }
}