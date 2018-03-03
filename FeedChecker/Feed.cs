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
            var lines = FeedParser.SplitResult(result);

            return FeedParser.FindAllCoreFx20Preview1InTheFeedResult(lines);
        }

        private static string GetResultByCallFeed(WebRequest request)
        {
            string result;
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