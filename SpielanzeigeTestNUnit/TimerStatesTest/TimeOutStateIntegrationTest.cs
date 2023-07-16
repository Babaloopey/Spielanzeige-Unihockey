using Moq;
using Umsetzung_III.TimerStates;
using Umsetzung_III;
using Umsetzung_III.Stores;

namespace SpielanzeigeTestNUnit.TimerStates
{
    [TestFixture]
    public class TimeOutStateIntegrationTest
    {
        Mock<SpielzeitStore> spielzeitStore;
        State sut;
        [SetUp]
        public void SetUp()
        {
            var ViewModelMock = new Mock<SpielanzeigeViewModel>();
            spielzeitStore = new Mock<SpielzeitStore>(ViewModelMock.Object);
            sut = new TimeOutState(spielzeitStore.Object);
        }

        [Test]
        public void InitializationTest()
        {
            Assert.AreEqual(0, sut.GetMinute());
            Assert.AreEqual(30, sut.GetSecond());
        }

        [Test]
        public void TimeRanOutTest()
        {
            sut.SecondMinusOne();
            sut.MinutePlusOne();

            sut.TimeRanOut();

            Assert.AreEqual(0, sut.GetMinute());
            Assert.AreEqual(30, sut.GetSecond());
            spielzeitStore.Verify(mock => mock.Stop(), Times.Once);
            spielzeitStore.Verify(mock => mock.SetSpielzeitState(), Times.Once);
        }
    }
}
