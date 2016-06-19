namespace Tests.Domain
{
    using Allors.Domain;

    using global::Domain;

    using NUnit.Framework;

    [TestFixture]
    public class Persons_ClearConnectionId_TestFixture : BaseTestFixture
    {
        private People people;
        
        public override void SetUp()
        {
            base.SetUp();
            this.people = new People(this.Session);

            var person = this.people.SaveConnectionId("patrick@inxin.com", "ConnectionId1", "Firefox");
        }

        [Test]
        public void Test_ClearConnectionId_Valid()
        {
            var person = this.people.ClearConnectionId("patrick@inxin.com", "ConnectionId1");

            Assert.That(person, Is.Not.Null);
            Assert.That(person.Id, Is.Not.Null);
            Assert.That(person.OnlineStatus.Connections, Has.Count.EqualTo(0));
        }

        [Test]
        public void Test_ClearConnectionId_OtherConnectionId()
        {
            var person = this.people.ClearConnectionId("patrick@inxin.com", "ConnectionId2");
            Assert.That(person, Is.Null);
        }

        [Test]
        public void Test_ClearConnectionId_WithUnknownUsername()
        {

            var person = this.people.ClearConnectionId("whoami@inxin.com", "ConnectionId1");
            // TODO Person is equal to supplied person..
        }

        [Test]
        public void Test_ClearConnectionId_WithUsernameNull()
        {
            var person = this.people.ClearConnectionId(null, "ConnectionId1");
            Assert.That(person, Is.Null);
        }
    }
}