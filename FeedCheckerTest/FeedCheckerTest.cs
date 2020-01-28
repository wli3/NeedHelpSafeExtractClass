using System.Collections.Generic;
using FeedChecker;
using FluentAssertions;
using Xunit;

namespace FeedCheckerTest
{
    public class GivenFeedChecker
    {
        [Fact]
        public void WhenFeedUrlIsProvidedItListAllCorefxpreview1Version()
        {
            var result1 = new Feed("http://apt-mo.trafficmanager.net/repos/dotnet/dists/jessie").DownloadFeed();
            var result = new FeedParser(result1).Parse();
            result.Should().HaveCountGreaterOrEqualTo(115);
        }

        [Fact]
        public void WhenFeedUrlIsProvidedItListCorefxpreview1VersionWithFollowingFormat()
        {
            var result1 = new Feed("http://apt-mo.trafficmanager.net/repos/dotnet/dists/jessie").DownloadFeed();
            var result = new FeedParser(result1).Parse();
            foreach (var l in result)
                l.Should().StartWith("dotnet-hostfxr-2.0.0-preview1");
        }
    }
}