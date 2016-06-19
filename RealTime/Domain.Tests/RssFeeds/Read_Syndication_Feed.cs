using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Allors;
using Allors.Domain;
using Allors.Domain.Philarion.Extensions;
using Allors.Meta;
using Domain;
using NUnit.Framework;

namespace Tests.RssFeeds
{
    [TestFixture]
    public class Read_Syndication_Feed : BaseTestFixture
    {
        public override void SetUp()
        {
            this.Setup(false);
        }

        [Test]
        public void News_CreateItem_TestFixture()
        {
            //BuildNewsFeed("http://feeds.bbci.co.uk/news/rss.xml");

            //BuildNewsFeed("http://www.standaard.be/rss/section/1f2838d4-99ea-49f0-9102-138784c7ea7c");

            BuildNewsFeed("http://www.lesoir.be/feed/Actualit%C3%A9/Fil%20Info/Fil%20info%20Sport/destination_principale_block");

            //BuildNewsFeed("http://rss.cnn.com/rss/edition.rss");

//            BuildNewsFeed("http://www.tijd.be/rss/top_stories.xml");

  //          BuildNewsFeed("http://www.telegraaf.nl/rss/");   
            
    //        BuildNewsFeed("https://www.goethe.de/de/feed/kul.rss"); 
            //

        }

        private void BuildNewsFeed(string url)
        {
            var languages = new Languages(this.Session);


            var reader = XmlReader.Create(url);
            var feed = SyndicationFeed.Load(reader);
            reader.Close();


            //var feedUrl = feed.BaseUri.AbsoluteUri;
            TextSyndicationContent descriptionContent = feed.Description;
            var titleContent = feed.Title;


            var newsFeedBuilder = new NewsFeedBuilder(this.Session).WithUrl(url)
                .WithDescription(descriptionContent.GetText())
                .WithTitle(titleContent.GetText());

            Uri imageUrl = feed.ImageUrl;
            if (imageUrl != null)
            {
                newsFeedBuilder.WithImage(this.Session.SetupImage(imageUrl));
            }

            foreach (SyndicationCategory syndicationCategory in feed.Categories)
            {
                var name = syndicationCategory.Name;
                var label = syndicationCategory.Label;
                var scheme = syndicationCategory.Scheme;
            }

            var isoCode = feed.Language;
            if (isoCode != null)
            {
                var culture = CultureInfo.GetCultureInfo(isoCode);

                var query = this.Session.Extent<Language>();
                query.Filter.AddEquals(M.Language.IsoCode, culture.TwoLetterISOLanguageName);
                var language = query.FirstOrDefault();

                newsFeedBuilder.WithLanguage(language);
            }


            var lastUpdateTime = feed.LastUpdatedTime;
            foreach (SyndicationLink link in feed.Links)
            {
                //application/rss+xml
                var baseUri = link.BaseUri;
                var length = link.Length;
                var mediaType = link.MediaType;
                var title= link.Title;
                var relationshipType = link.RelationshipType;
                var uri = link.Uri;                
            }

            var newsFeed = newsFeedBuilder.Build();


            foreach (var item in feed.Items)
            {
                var newsItem = new NewsItemBuilder(this.Session)
                    .WithKey(item.Id)
                    .WithTitle(item.Title.GetText())
                    .WithSummary(item.Summary.GetText())
                    .WithPublishDate(item.PublishDate.LocalDateTime)
                    .Build();


                // Images by Links
                foreach (var link in item.Links.Where(x => x.MediaType!= null && x.MediaType.StartsWith("image")))
                {
                    newsItem.AddImage(this.Session.SetupImage(link.Uri));
                }
                // Images by ElementExtensions
                foreach (SyndicationElementExtension extension in item.ElementExtensions.Where(x => x.OuterName == "thumbnail"))
                {
                    XElement ele = extension.GetObject<XElement>();
                    var imageInfo = ele.GetImageInfo();

                    newsItem.AddImage(this.Session.SetupImageFromUrl(imageInfo.Url));
                }

                // Url
                var alternateUrl = item.Links.FirstOrDefault(x => x.RelationshipType != null && x.RelationshipType.Equals("alternate"));
                if (alternateUrl != null)
                {
                    newsItem.Url = alternateUrl.Uri.AbsoluteUri;
                }
                
                // Other
                foreach (var link in item.Links.Where(x=> x.MediaType == null || ! x.MediaType.StartsWith("image")))
                {
                    //application/rss+xml
                    var baseUri = link.BaseUri;
                    var length = link.Length;
                    var mediaType = link.MediaType;
                    var title = link.Title;
                    var relationshipType = link.RelationshipType;
                    var uri = link.Uri;
                }

                // Other
                foreach (SyndicationElementExtension extension in item.ElementExtensions.Where(x => x.OuterName != "thumbnail"))
                {
                    XElement ele = extension.GetObject<XElement>();
                    foreach (var attribute in ele.Attributes())
                    {
                    }
                }

                newsFeed.AddNewsItem(newsItem);
            }

            Session.Commit();
        }
    }

    public static class TextSyndicationContentExtension
    {
        public static String GetText(this TextSyndicationContent content)
        {
            switch (content.Type.ToLower())
            {
                case "text":
                    return content.Text;
                default:
                    throw new Exception(content.Type);
            }
        }

    }

  
}
