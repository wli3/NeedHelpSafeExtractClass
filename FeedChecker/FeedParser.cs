using System;
using System.Collections.Generic;
using System.Linq;

static internal class FeedParser
{
    public static IEnumerable<string> FindAllCoreFx20Preview1InTheFeedResult(IEnumerable<string> lines)
    {
        var onlyCoreFxPreview1ButWithPackgeInFront =
            lines.Where(l => l.StartsWith("Package: dotnet-hostfxr-2.0.0-preview1"));

        // imagine there is more code

        return onlyCoreFxPreview1ButWithPackgeInFront.Select(l => l.Replace("Package: ", ""));
    }

    public static IEnumerable<string> SplitResult(string result)
    {
        var lines = result.Split(
            new[] {"\r\n", "\r", "\n"},
            StringSplitOptions.None);
        return lines;
    }
}