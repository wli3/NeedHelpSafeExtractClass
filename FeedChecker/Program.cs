using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FeedChecker
{
    public class Feed1
    {
        private string _requestUriString;

        public Feed1(string feed)
        {
            Feed = feed;
            var f = Feed;
            if (!f.EndsWith("/"))
                Feed = f + "/";

            var packageUrl = f + "main/binary-amd64/Packages";
            _requestUriString = packageUrl;
        }

        private string Feed { get; }

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
            foreach (var l in GetAllCoreFxPreview1("http://apt-mo.trafficmanager.net/repos/dotnet/dists/jessie"))
                Console.WriteLine(l);
        }

        public static IEnumerable<string> GetAllCoreFxPreview1(string feed)
        {
            var result = new Feed1(feed).DownloadFeed();
            return new FeedParser(result).Parse();
        }
    }
}