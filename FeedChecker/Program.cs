﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FeedChecker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (var l in GetAllCoreFxPreview1("http://apt-mo.trafficmanager.net/repos/dotnet/dists/jessie"))
                Console.WriteLine(l);
        }

        public static IEnumerable<string> GetAllCoreFxPreview1(string feed)
        {
            var result = DownloadFeed(feed);
            var allCoreFxPreview = FeedParser(result);
            return allCoreFxPreview;
        }

        private static IEnumerable<string> FeedParser(string result)
        {
            var lines = result.Split(
                new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None);

            var onlyCoreFxPreview1ButWithPackgeInFront =
                lines.Where(l => l.StartsWith("Package: dotnet-hostfxr-2.0.0-preview1"));

            // imagine there is more code

            var allCoreFxPreview = onlyCoreFxPreview1ButWithPackgeInFront.Select(l => l.Replace("Package: ", ""));
            return allCoreFxPreview;
        }

        private static string DownloadFeed(string feed)
        {
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

            return result;
        }
    }
}