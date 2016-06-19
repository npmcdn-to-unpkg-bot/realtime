namespace Tests.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using Allors.Domain;
    using Allors.Meta;

    using global::Domain;

    using NUnit.Framework;

    [TestFixture]
    public class Tips_GetTips_TestFixture : BaseTestFixture
    {
        private Tips _tips;

        private Language _english;
        private Language _german;
        private Language _dutch;

        private List<Language> _multiLanguages;

        private Tip _tip;

        public override void SetUp()
        {
            base.SetUp();

            this._tips = new Tips(this.Session);

            _english = new Languages(this.Session).FindBy(M.Language.IsoCode, "en");
            _german = new Languages(this.Session).FindBy(M.Language.IsoCode, "de");
            _dutch = new Languages(this.Session).FindBy(M.Language.IsoCode, "nl");

            _multiLanguages = new List<Language>() { _english, _german };

            RemoveAllTips();

            _tip = _tips.SetupTip("http://feeds.bbci.co.uk/news/rss.xml", "Title", "Description", _multiLanguages, TipType.Listening);
        }

        private void RemoveAllTips()
        {
            var items = this.Session.Extent<Tip>();
            foreach (Tip item in items)
            {
                item.Delete();
            }
        }

        [Test]
        public void Test_GetTips_Simple()
        {
            var pageSize = 20;

            var tips = _tips.GetTips(_multiLanguages, pageSize, 1);

            Assert.That(tips.Count(), Is.GreaterThan(0));
        }

    }
}
