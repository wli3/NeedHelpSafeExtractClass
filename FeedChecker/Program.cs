using System;

namespace FeedChecker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (var l in new Feed("http://apt-mo.trafficmanager.net/repos/dotnet/dists/jessie")
                .GetAllCoreFxPreview1())
                Console.WriteLine(l);
        }
    }
}