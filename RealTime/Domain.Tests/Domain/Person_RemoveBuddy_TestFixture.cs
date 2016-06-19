namespace Tests.Domain
{
    using Allors;
    using Allors.Domain;

    using global::Domain;

    using NUnit.Framework;

    [TestFixture]
    public class Person_RemoveBuddy_TestFixture : BaseTestFixture
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

        //    var buddyRequest = this.person1.RequestBuddy(this.person2);
        //    buddyRequest.Accept();

        //    this.Session.Derive();
        //    this.Session.Commit();
        //}

        //private void RemoveAllBuddies(Person person)
        //{
        //    foreach (Link request in person.BuddyRequests)
        //    {
        //        request.Delete();
        //    }
        //}

        //[Test]
        //public void Test_RemoveBuddy_Simple_ByRequestor()
        //{
        //    this.person1.RequestRemoveBuddy(this.person2);

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));

        //    base.Session.Derive();

        //    Assert.That(this.person1.Buddies, Has.Count.EqualTo(0));
        //    Assert.That(this.person2.Buddies, Has.Count.EqualTo(0));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

        //[Test]
        //public void Test_RemoveBuddy_Simple_ByAcceptor()
        //{
        //    this.person2.RequestRemoveBuddy(this.person1);

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));

        //    base.Session.Derive();

        //    Assert.That(this.person1.Buddies, Has.Count.EqualTo(0));
        //    Assert.That(this.person2.Buddies, Has.Count.EqualTo(0));

        //    Assert.That(this.person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(this.person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}
        
        //[Test]
        //public void Test_RequestBuddy_PreviousRequestRejected()
        //{
        //    var buddyRequest = person1.RequestBuddy(person2);

        //    Assert.That(person1.Links, Has.Count.EqualTo(1));
        //    Assert.That(person2.Links, Has.Count.EqualTo(0));

        //    buddyRequest.Reject();
        //    base.Session.Derive();

        //    Assert.That(person1.Buddies, Has.Count.EqualTo(0));
        //    Assert.That(person2.Buddies, Has.Count.EqualTo(0));

        //    Assert.That(person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));

        //    var buddyRequest2 = person1.RequestBuddy(person2);

        //    Assert.That(buddyRequest, Is.EqualTo(buddyRequest2));

        //    Assert.That(person1.Links, Has.Count.EqualTo(1));
        //    Assert.That(person2.Links, Has.Count.EqualTo(0));

        //    buddyRequest.Accept();
        //    base.Session.Derive();

        //    Assert.That(person1.Buddies, Has.Count.EqualTo(1));
        //    Assert.That(person2.Buddies, Has.Count.EqualTo(1));

        //    Assert.That(person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

        //[Test]
        //public void Test_RequestBuddy_BothRequestBuddies()
        //{
        //    var buddyRequest1 = person1.RequestBuddy(person2);
        //    var buddyRequest2 = person2.RequestBuddy(person1);

        //    Assert.That(person1.Links, Has.Count.EqualTo(1));
        //    Assert.That(person2.Links, Has.Count.EqualTo(1));

        //    buddyRequest1.Accept();
        //    base.Session.Derive();

        //    Assert.That(person1.Buddies, Has.Count.EqualTo(1));
        //    Assert.That(person2.Buddies, Has.Count.EqualTo(1));

        //    Assert.That(person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

        //[Test]
        //public void Test_RequestBuddy_BothRequestBuddies_Rejected()
        //{
        //    var buddyRequest1 = person1.RequestBuddy(person2);
        //    var buddyRequest2 = person2.RequestBuddy(person1);

        //    Assert.That(person1.Links, Has.Count.EqualTo(1));
        //    Assert.That(person2.Links, Has.Count.EqualTo(1));

        //    buddyRequest1.Reject();
        //    base.Session.Derive();

        //    Assert.That(person1.Buddies, Has.Count.EqualTo(0));
        //    Assert.That(person2.Buddies, Has.Count.EqualTo(0));

        //    Assert.That(person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(person2.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

        //[Test]
        //public void Test_RequestBuddy_Complex()
        //{
        //    var buddyRequest1To2 = person1.RequestBuddy(person2);
        //    var buddyRequest2To3 = person2.RequestBuddy(person3);
        //    var buddyRequest1To3 = person1.RequestBuddy(person3);
        //    var buddyRequest3To1 = person3.RequestBuddy(person1);

        //    Assert.That(person1.Links, Has.Count.EqualTo(2));
        //    Assert.That(person2.Links, Has.Count.EqualTo(1));
        //    Assert.That(person3.Links, Has.Count.EqualTo(1));

        //    buddyRequest1To2.Accept();
        //    buddyRequest3To1.Accept();
        //    base.Session.Derive();

        //    Assert.That(person1.Buddies, Has.Count.EqualTo(2));
        //    Assert.That(person2.Buddies, Has.Count.EqualTo(1));
        //    Assert.That(person3.Buddies, Has.Count.EqualTo(1));

        //    Assert.That(person1.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //    Assert.That(person2.GetOpenBuddyRequest(), Has.Count.EqualTo(1));
        //    Assert.That(person3.GetOpenBuddyRequest(), Has.Count.EqualTo(0));
        //}

    }
}
