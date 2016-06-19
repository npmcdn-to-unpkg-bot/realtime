namespace Tests.Domain
{
    using System.Linq;

    using Allors;
    using Allors.Domain;

    using global::Domain;

    using NUnit.Framework;

    using Should;

    class CommunicationsTestFixture : BaseTestFixture
    {
        private Communications _communications;
        private People _people;

        private Person _person;
        private Person _person2;
        private Person _person3;

        public override void SetUp()
        {
            base.SetUp();
            this._communications = new Communications(this.Session);
            this._people = new People(this.Session);

            _person = _people.FindPersonByUsername("patrick@inxin.com");
            _person2 = _people.FindPersonByUsername("koen@inxin.com");
            _person3 = _people.FindPersonByUsername("vanparys.patricia@gmail.com");

            var communication1 = _person.RequestTextCommunication(_person2);
            communication1.StartAndAccept();
            communication1.End();

            var communication2 = _person.RequestTextCommunication(_person2);
            communication2.StartAndAccept();

            var communication3 = _person3.RequestTextCommunication(_person);
            communication3.StartAndAccept();


            Session.Derive();
            Session.Commit();

        }

        [Test]
        public void Test()
        {
            var communications = _communications.FindOpenCommunicationsFor(_person);
            communications.Count().ShouldBeSameAs(2);
        }

    }
}
