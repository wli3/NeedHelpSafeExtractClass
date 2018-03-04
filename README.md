# Help needed. How to safe refactoring extract class? 

## In short

Branch [attempt-1](https://github.com/wli3/NeedHelpSafeExtractClass/tree/attempt-1) is the branch I get stuck on extract class.

I have [this](https://github.com/wli3/NeedHelpSafeExtractClass/blob/122b728c201d0241e69de58217ba34c79b61caa7/FeedChecker/Feed.cs) class. And I want to move `FindAllCoreFx20Preview1InTheFeedResult` and `SplitResult` out of the class and form a new class. Because it is all about data process. Also, due to it is a demo, it is pretty simple. In the real world, it should have several methods and a plenty of fields.

After I use _move to another type_, they became [this](https://github.com/wli3/NeedHelpSafeExtractClass/blob/attempt-1/FeedChecker/FeedParser.cs). But I want it to be a non static class. And only one method should be exposed to [Feed class](https://github.com/wli3/NeedHelpSafeExtractClass/blob/attempt-1/FeedChecker/Feed.cs).

## If you have better overall approach

Please start with [master](https://github.com/wli3/NeedHelpSafeExtractClass/tree/master). Note, this is an exercise. In real world, this state is probably the best. According to [this](https://www.sandimetz.com/blog/2017/9/13/breaking-up-the-behemoth), the problem is simple enough and use OOP hammer will make it harder to understand.

However, please treat every methods will do much more and worth it for exercise sake.

## What does this program do

Please assume you don't know. And just use pure refactoring.

-------------------
To make the pull request more clear, I swapped the branch. Master is right-after green.