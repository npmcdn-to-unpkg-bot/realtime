using Allors.Domain;
using Domain;
using NUnit.Framework;

namespace Tests.Domain
{
    [TestFixture]
    public class Persons_GetAllBuddies_TestFixture : BaseTestFixture
    {
        private IPersons _persons;

        public override void SetUp()
        {
            base.SetUp();
            this._persons = new Persons(this.Session);
        }

        [Test]
        public void Test_GetTotalCountOfBuddies()
        {
            var buddies = _persons.GetAllBuddies();
            Assert.That(buddies, Has.Count.GreaterThan(0));
        }
    }
}