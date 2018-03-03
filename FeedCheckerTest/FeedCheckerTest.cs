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
            var result = Feed.GetAllCoreFxPreview1("http://apt-mo.trafficmanager.net/repos/dotnet/dists/jessie");
            result.Should().HaveCountGreaterOrEqualTo(115);
        }

        [Fact]
        public void WhenFeedUrlIsProvidedItListCorefxpreview1VersionWithFollowingFormat()
        {
            var result = Feed.GetAllCoreFxPreview1("http://apt-mo.trafficmanager.net/repos/dotnet/dists/jessie");
            foreach (var l in result)
                l.Should().StartWith("dotnet-hostfxr-2.0.0-preview1");
        }
    }
}