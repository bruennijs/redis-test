using NUnit.Framework;

namespace Redis.test
{
  [TestFixture]
  public class AggregateRepositoryTest
  {
    [Test]
    public void When_expect()
    {
      Assert.True(true);
    }

    [Test]
    public void When_false_expect_fail()
    {
      Assert.Fail("YEAH implementing #region mono");
    }
  }
}