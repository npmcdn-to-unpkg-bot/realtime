namespace Tests.Domain
{
    using Allors.Domain;

    using global::Domain;

    using NUnit.Framework;

    [TestFixture]
    public class Persons_SaveConnectionId_TestFixture : BaseTestFixture
    {
        private People people;

        public override void SetUp()
        {
            base.SetUp();
            this.people = new People(this.Session);
        }

        [Test]
        public void Test_SaveConnectionId_Valid()
        {
            var person = this.people.SaveConnectionId("patrick@inxin.com", "ConnectionId1", "Firefox");
            Assert.That(person, Is.Not.Null);
            Assert.That(person.Id, Is.Not.Null);
            Assert.That(person.OnlineStatus.Connections, Has.Count.EqualTo(1));
        }

        [Test]
        public void Test_SaveConnectionId_WithUnknownUsername()
        {
            var person = this.people.SaveConnectionId("whoami@inxin.com", "ConnectionId1", "Firefox");
            Assert.That(person, Is.Null);
        }

        [Test]
        public void Test_SaveConnectionId_WithNullUsername()
        {
            var person = this.people.SaveConnectionId(null, "ConnectionId1", "Firefox");
            Assert.That(person, Is.Null);
        }
    }
}
