using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Stores;
using Umsetzung_III.TimerStates;
using Umsetzung_III;

namespace SpielanzeigeTestNUnit.TimerStates
{
    [TestFixture]
    public class PausenStateIntegrationTest
    {
        Mock<SpielzeitStore> spielzeitStore;
        State sut;
        [SetUp]
        public void SetUp()
        {
            var ViewModelMock = new Mock<SpielanzeigeViewModel>();
            spielzeitStore = new Mock<SpielzeitStore>(ViewModelMock.Object);
            sut = new PausenState(spielzeitStore.Object);
        }

        [Test]
        public void InitializationTest()
        {
            Assert.AreEqual(5, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());
        }

        [Test]
        public void TimeRanOutTest()
        {
            sut.SecondMinusOne();
            sut.MinutePlusOne();

            sut.TimeRanOut();

            Assert.AreEqual(5, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());
            spielzeitStore.Verify(mock => mock.Stop(), Times.Once);
            spielzeitStore.Verify(mock => mock.SetSpielzeitState(), Times.Once);
        }
    }

}
