namespace Tests.Domain
{
    using Allors.Domain;

    using global::Domain;

    using NUnit.Framework;

    [TestFixture]
    public class Person_StartCommunication_TestFixture : BaseTestFixture
    {
        private People people;

        private Person owner;
        private Person subscriber;

        public override void SetUp()
        {
            base.SetUp();
            this.people = new People(this.Session);

            this.owner = this.people.FindPersonByUsername("patrick@inxin.com");
            this.subscriber = this.people.FindPersonByUsername("koen@inxin.com");
        }


        //[Test]
        //public void Test_RequestTextCommunication_Valid()
        //{
        //    var communication = _owner.RequestTextCommunication(_subscriber);
        //    Assert.That(communication, Is.Not.Null);
        //}

        //[Test]
        //public void Test_RequestTextCommunication_Valid()
        //{
        //    var communication = this.owner.RequestTextCommunication(this.subscriber);
        //    Assert.That(communication, Is.Not.Null);
        //}

        //[Test]
        //public void Test_RequestVideoCommunication_Valid()
        //{
        //    var communication = _owner.RequestVideoCommunication(_subscriber);
        //    Assert.That(communication, Is.Not.Null);
        //}


    }
}
