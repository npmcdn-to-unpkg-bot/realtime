namespace Tests.Domain
{
    using Allors;
    using Allors.Domain;

    using global::Domain;

    using NUnit.Framework;

    [TestFixture]
    public class Person_RequestBuddy_TestFixture : BaseTestFixture
    {
        private People people;

        private Person person1;
        private Person person2;
        private Person person3;

        //public override void SetUp()
        //{
        //    base.SetUp();
        //    this.people = new People(this.Session);

        //    this.person1 = this.people.FindPersonByUsername("patrick@inxin.com");
        //    this.person2 = this.people.FindPersonByUsername("koen@inxin.com");
        //    this.person3 = this.people.FindPersonByUsername("vanparys.patricia@gmail.com");

        //    this.RemoveAllBuddies(this.person1);
        //    this.RemoveAllBuddies(this.person2);
        //    this.RemoveAllBuddies(this.person3);
        //}

        //private void RemoveAllBuddies(Person person)
        //{
        //    foreach (Link request in person.BuddyRequests)
        //    {
        //        request.Delete();
        //        //person.RemoveBuddyRequest(request);
        //    }

        //    this.Session.Derive();
        //    this.Session.Commit();
        //}

        //[Test]
        //public void Test_RequestBuddy_Simple()
        //{
        //    var buddyRequest = this.person1.RequestBuddy(this.person2);

        //    Assert.That(this.person1.BuddyRequests, Has.Count.EqualTo(1));
        //    Assert.That(this.person2.BuddyRequests, Has.Count.EqualTo(0));

        //    buddyRequest.Accept();
        //    this.Session.Derive();

        //    Assert.That(this.person1.Buddies, Has.Count.EqualTo(1));
        //    Assert.That(this.person2.Buddies, Has.Count.EqualTo(1));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

        //[Test]
        //public void Test_RequestBuddy_PreviousRequestRejected()
        //{
        //    var buddyRequest = this.person1.RequestBuddy(this.person2);

        //    Assert.That(this.person1.BuddyRequests, Has.Count.EqualTo(1));
        //    Assert.That(this.person2.BuddyRequests, Has.Count.EqualTo(0));

        //    buddyRequest.Reject();
        //    this.Session.Derive();

        //    Assert.That(this.person1.Buddies, Has.Count.EqualTo(0));
        //    Assert.That(this.person2.Buddies, Has.Count.EqualTo(0));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));

        //    var buddyRequest2 = this.person1.RequestBuddy(this.person2);

        //    Assert.That(buddyRequest, Is.EqualTo(buddyRequest2));

        //    Assert.That(this.person1.BuddyRequests, Has.Count.EqualTo(1));
        //    Assert.That(this.person2.BuddyRequests, Has.Count.EqualTo(0));

        //    buddyRequest.Accept();
        //    this.Session.Derive();

        //    Assert.That(this.person1.Buddies, Has.Count.EqualTo(1));
        //    Assert.That(this.person2.Buddies, Has.Count.EqualTo(1));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

        //[Test]
        //public void Test_RequestBuddy_BothRequestBuddies()
        //{
        //    var buddyRequest1 = this.person1.RequestBuddy(this.person2);
        //    var buddyRequest2 = this.person2.RequestBuddy(this.person1);

        //    Assert.That(this.person1.BuddyRequests, Has.Count.EqualTo(1));
        //    Assert.That(this.person2.BuddyRequests, Has.Count.EqualTo(1));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));

        //    buddyRequest1.Accept();
        //    this.Session.Derive();

        //    Assert.That(this.person1.Buddies, Has.Count.EqualTo(1));
        //    Assert.That(this.person2.Buddies, Has.Count.EqualTo(1));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

        //[Test]
        //public void Test_RequestBuddy_BothRequestBuddies_Rejected()
        //{
        //    var buddyRequest1 = this.person1.RequestBuddy(this.person2);
        //    var buddyRequest2 = this.person2.RequestBuddy(this.person1);

        //    Assert.That(this.person1.BuddyRequests, Has.Count.EqualTo(1));
        //    Assert.That(this.person2.BuddyRequests, Has.Count.EqualTo(1));

        //    buddyRequest1.Reject();
        //    this.Session.Derive();

        //    Assert.That(this.person1.Buddies, Has.Count.EqualTo(0));
        //    Assert.That(this.person2.Buddies, Has.Count.EqualTo(0));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

        //[Test]
        //public void Test_RequestBuddy_Complex()
        //{
        //    var buddyRequest1To2 = this.person1.RequestBuddy(this.person2);
        //    var buddyRequest2To3 = this.person2.RequestBuddy(this.person3);
        //    var buddyRequest1To3 = this.person1.RequestBuddy(this.person3);
        //    var buddyRequest3To1 = this.person3.RequestBuddy(this.person1);

        //    Assert.That(this.person1.BuddyRequests, Has.Count.EqualTo(2));
        //    Assert.That(this.person2.BuddyRequests, Has.Count.EqualTo(1));
        //    Assert.That(this.person3.BuddyRequests, Has.Count.EqualTo(1));

        //    buddyRequest1To2.Accept();
        //    buddyRequest3To1.Accept();
        //    this.Session.Derive();

        //    Assert.That(this.person1.Buddies, Has.Count.EqualTo(2));
        //    Assert.That(this.person2.Buddies, Has.Count.EqualTo(1));
        //    Assert.That(this.person3.Buddies, Has.Count.EqualTo(1));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(1));
        //    Assert.That(this.person3.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}
    }
}
