namespace Tests.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Syndication;
    using System.Xml;

    using Allors.Domain;
    using Allors.Meta;

    using global::Domain;

    using NUnit.Framework;

    using Should;

    [TestFixture]
    public class NewsFeeds_SetupNewsFeed_TestFixture : BaseTestFixture
    {
        private NewsFeeds newsFeeds;

        private Language english;
        private Language german;

        private List<Language> englishLanguages;
        private List<Language> multiLanguages;
        
        public override void SetUp()
        {
            base.SetUp();
            this.newsFeeds = new NewsFeeds(this.Session);

            this.english = new Languages(this.Session).FindBy(M.Language.IsoCode, "en");
            this.german = new Languages(this.Session).FindBy(M.Language.IsoCode, "de");

            this.englishLanguages = new List<Language>() { this.english };
            this.multiLanguages = new List<Language>() { this.english, this.german };
        }
        
        [Test]
        public void Test_SetupNewsFeed_Simple()
        {

            var newsFeed = this.newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/news/rss.xml", "BBC Top Stories", this.englishLanguages, "News");

            Assert.That(newsFeed.NewsItems, Has.Count.GreaterThan(0));
        }

        [Test]
        public void Test_SetupNewsFeed_WithReload()
        {

            var newsFeed = this.newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/news/rss.xml", "BBC Top Stories", this.englishLanguages, "News");

            var expectedCount =newsFeed.NewsItems.Count;

            var reader = XmlReader.Create(newsFeed.Url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();

            this.newsFeeds.ReloadNewsItems(newsFeed, feed);
            Assert.That(newsFeed.NewsItems.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Test_SetupNewsFeed_MultipleTopics()
        {
            var newsFeed = this.newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/news/rss.xml", "BBC Top Stories", this.englishLanguages, "News", "Politics","World");

            Assert.That(newsFeed.Topics.Count, Is.EqualTo(3));
        }

        [Test]
        public void Test_SetupNewsFeed_MultipleLanguages()
        {
            var newsFeed = this.newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/news/rss.xml", "BBC Top Stories", this.multiLanguages, "News");

            Assert.That(newsFeed.Languages.Count, Is.EqualTo(2));
        }

        [Test]
        public void Test_SetupNewsFeed_Multi()
        {
            var newsFeed1 = this.newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/news/rss.xml", "BBC Top Stories", this.multiLanguages, "News", "World");
            var newsFeed2 = this.newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/news/world/rss.xml", "BBC World", this.multiLanguages, "News", "World");

            Assert.That(newsFeed1.Languages.Count, Is.EqualTo(2));
            Assert.That(newsFeed2.Languages.Count, Is.EqualTo(2));

            Assert.That(newsFeed1.Topics.Count, Is.EqualTo(2));
            Assert.That(newsFeed2.Topics.Count, Is.EqualTo(2));

            newsFeed1.Topics.Any(x => x.NewsFeedsWhereTopic.Contains(newsFeed1)).ShouldBeTrue();
            newsFeed2.Topics.Any(x => x.NewsFeedsWhereTopic.Contains(newsFeed2)).ShouldBeTrue();

            newsFeed1.Languages.Any(x => x.NewsFeedsWhereLanguage.Contains(newsFeed1)).ShouldBeTrue();
            newsFeed2.Languages.Any(x => x.NewsFeedsWhereLanguage.Contains(newsFeed2)).ShouldBeTrue();
        }
    }
}
