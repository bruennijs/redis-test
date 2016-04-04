using NUnit.Framework;

namespace Redis.test
{
  [TestFixture]
  public class AggregateRepositoryTest
  {
    [Test]
    public void When_insert_expect_get_by_id_succeeds()
    {
      Smgw smgw = new Smgw("1");
      AggregateRepository sut = new AggregateRepository();
      sut.Insert(smgw);

      Assert.AreEqual("1", sut.GetById(smgw.Id));
    }

    [Test]
    [Ignore("executing not now")]
    public void When_get_expect_connection_can_do_that()
    {
      Smgw gateway = new AggregateRepository().GetById("2");
      Assert.AreEqual("2", gateway.Id);
    }
  }
}