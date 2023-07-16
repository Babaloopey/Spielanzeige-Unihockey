using Moq;
using Umsetzung_III.TimerStates;
using Umsetzung_III;
using Umsetzung_III.Stores;

namespace SpielanzeigeIntegrationsTest.TimerStatesIntegrationTest
{
    [TestFixture]
    public class StateTest
    {
        State sut;
        [SetUp]
        public void SetUp()
        {
            var ViewModelMock = new Mock<SpielanzeigeViewModel>();
            var SpielzeitStoreMock = new Mock<SpielzeitStore>(ViewModelMock.Object);
            sut = new PausenState(SpielzeitStoreMock.Object);
        }

        [Test]
        public void TimeElapsed_OneSecondPasses()
        {
            CallTimeElapsed(10);
            Assert.AreEqual(59, sut.GetSecond());
        }

        [Test]
        public void TimeElapsed_SomeSecondPass()
        {
            CallTimeElapsed(9);
            Assert.AreEqual(0, sut.GetSecond());
            CallTimeElapsed();
            Assert.AreEqual(59, sut.GetSecond());
            CallTimeElapsed(9);
            Assert.AreEqual(59, sut.GetSecond());
            CallTimeElapsed();
            Assert.AreEqual(58, sut.GetSecond());
        }

        [Test]
        public void CountOneSecondTest()
        {
            SecondsMinus(59);
            CallTimeElapsed(10);
            Assert.AreEqual(4, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());

            CallTimeElapsed(10);
            Assert.AreEqual(3, sut.GetMinute());
            Assert.AreEqual(59, sut.GetSecond());
        }

        [Test]
        public void CountOneSecond_TimeRanOut()
        {
            MinuteMinus(5);
            sut.SecondPlusOne();
            CallTimeElapsed(10);
            Assert.AreEqual(5, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());
        }

        [Test]
        public void ResetTest()
        {
            MinuteMinus(2);
            SecondsMinus(32);
            CallTimeElapsed(9);
            sut.Reset();
            Assert.AreEqual(5, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());

            CallTimeElapsed(9);
            Assert.AreEqual(5, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());
        }
        [Test]
        public void MinutePlusOneTest()
        {
            sut.MinutePlusOne();
            Assert.AreEqual(6, sut.GetMinute());
        }
        [Test]
        public void MinuteMinusOneTest()
        {
            sut.MinuteMinusOne();
            Assert.AreEqual(4, sut.GetMinute());
        }
        [Test]
        public void MinuteMinusOne_AlreadyZero()
        {
            MinuteMinus(20);
            Assert.AreEqual(0, sut.GetMinute());
        }
        [Test]
        public void SecondPlusOneTest()
        {
            sut.SecondPlusOne();
            Assert.AreEqual(1, sut.GetSecond());
        }
        [Test]
        public void SecondPlusOneTest_FiftyNineSeconds()
        {
            sut.SecondMinusOne();
            Assert.AreEqual(4, sut.GetMinute());
            Assert.AreEqual(59, sut.GetSecond());

            sut.SecondPlusOne(); 
            Assert.AreEqual(5, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());
        }
        [Test]
        public void SecondMinusOneTest()
        {
            sut.SecondMinusOne();
            Assert.AreEqual(4, sut.GetMinute());
            Assert.AreEqual(59, sut.GetSecond());

            sut.SecondMinusOne();
            Assert.AreEqual(4, sut.GetMinute());
            Assert.AreEqual(58, sut.GetSecond());
        }
        [Test]
        public void SecondMinusOne_AlreadyZero()
        {
            MinuteMinus(5);
            Assert.AreEqual(0, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());

            sut.SecondMinusOne();
            Assert.AreEqual(0, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());
        }

        [Test]
        public void GetDurationTest()
        {
            Assert.AreEqual(5, sut.GetDuration());
        }

        private void CallTimeElapsed(int count = 1)
        {
            for(int i = 0; i < count; i++)
            {
                sut.TimeElapsed();
            }
        }
        private void MinuteMinus(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                sut.MinuteMinusOne();
            }
        }
        private void SecondsMinus(int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                sut.SecondMinusOne();
            }
        }

    }
}
