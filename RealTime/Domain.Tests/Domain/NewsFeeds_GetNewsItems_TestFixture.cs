namespace Tests.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Domain;
    using Allors.Meta;

    using global::Domain;

    using NUnit.Framework;

    [TestFixture]
    public class NewsFeeds_GetNewsItems_TestFixture : BaseTestFixture
    {
        private NewsFeeds newsFeeds;

        private Language english;
        private Language german;
        private Language dutch;

        private List<Language> multiLanguages;

        private NewsFeed newsFeed;
        private IList<Topic> topics;

        private Topic newsTopic;
        private Topic worldTopic;
        private Topic noneExistingTopic;

        public override void SetUp()
        {
            base.SetUp();

            this.newsFeeds = new NewsFeeds(this.Session);

            this.english = new Languages(this.Session).FindBy(M.Language.IsoCode, "en");
            this.german = new Languages(this.Session).FindBy(M.Language.IsoCode, "de");
            this.dutch = new Languages(this.Session).FindBy(M.Language.IsoCode, "nl");

            this.multiLanguages = new List<Language>() { this.english, this.german };

            this.newsTopic = new TopicBuilder(this.Session).WithName("News").Build();
            this.worldTopic = new TopicBuilder(this.Session).WithName("World").Build();
            this.noneExistingTopic = new TopicBuilder(this.Session).WithName("None").Build();

            this.topics = new List<Topic> {this.worldTopic, this.newsTopic};

            this.RemoveAllNewsFeeds();

            this.newsFeed = this.newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/news/rss.xml", "BBC Top Stories", this.multiLanguages, this.topics);
            //_newsGermanFeed = newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/world/rss.xml", "BBC Top Stories", multiLanguages, topics);
            //_newsEnglishFeed = newsFeeds.SetupNewsFeed("http://feeds.bbci.co.uk/news/rss.xml", "BBC Top Stories", multiLanguages, topics);
        }

        private void RemoveAllNewsFeeds()
        {
            var items = this.Session.Extent<NewsFeed>();
            foreach (NewsFeed item in items)
            {
                item.Delete();
            }
        }

        [Test]
        public void Test_GetNewsItems_Simple()
        {
            var pageSize = 20;

            var newsItems = this.newsFeeds.GetNewsItemsFor(this.multiLanguages, this.topics, pageSize, 1);

            Assert.That(newsItems.Count(), Is.EqualTo(pageSize));
        }

        [Test]
        public void Test_GetNewsItems_Oversized()
        {
            var pageSize = 2000;

            var newsItems = this.newsFeeds.GetNewsItemsFor(this.multiLanguages, this.topics, pageSize, 1);

            Assert.That(newsItems.Count(), Is.GreaterThan(0));
        }

        [Test]
        public void Test_GetNewsItems_InvalidPage()
        {
            var pageSize = 20;
            int page = 1000;

            var newsItems = this.newsFeeds.GetNewsItemsFor(this.multiLanguages, this.topics, pageSize, page);

            Assert.That(newsItems.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Test_GetNewsItems_NoFeedsForTopic()
        {
            var pageSize = 20;
            int page = 1;

            var newsItems = this.newsFeeds.GetNewsItemsFor(this.multiLanguages, new List<Topic>() {this.noneExistingTopic}, pageSize, page);

            Assert.That(newsItems.Count(), Is.EqualTo(0));
        }
        [Test]
        public void Test_GetNewsItems_NoFeedsForLanguage()
        {
            var pageSize = 20;
            int page = 1;

            var newsItems = this.newsFeeds.GetNewsItemsFor(new List<Language>() {this.dutch}, this.topics, pageSize, page);

            Assert.That(newsItems.Count(), Is.EqualTo(0));
        }
    }
}
