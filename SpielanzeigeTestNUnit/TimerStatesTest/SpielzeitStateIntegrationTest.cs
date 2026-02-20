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
    public class SpielzeitStateIntegrationTest
    {
        SpielanzeigeViewModel spielanzeigeViewModel;
        Mock<SpielzeitStore> spielzeitStore;
        State sut;
        [SetUp]
        public void SetUp()
        {
            spielanzeigeViewModel = new SpielanzeigeViewModel();
            spielzeitStore = new Mock<SpielzeitStore>(spielanzeigeViewModel);
            sut = new SpielzeitState(spielzeitStore.Object, spielanzeigeViewModel);
        }

        [Test]
        public void InitializationTest()
        {
            Assert.AreEqual(20, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());
        }

        [Test]
        public void TimeRanOutTest()
        {
            sut.SecondMinusOne();
            sut.MinutePlusOne();

            sut.TimeRanOut();

            Assert.AreEqual(20, sut.GetMinute());
            Assert.AreEqual(0, sut.GetSecond());
            spielzeitStore.Verify(mock => mock.Stop(), Times.Once);
            Assert.AreEqual(2, spielanzeigeViewModel.Halbzeit);
        }

        [Test]
        public void TimeRanOutInvokesAction()
        {
            // Arrange
            bool isReset = false;
            ((SpielzeitState)sut).OnSpielzeitRanOut += () => isReset = true;

            // Act
            sut.TimeRanOut();

            // Arrange
            Assert.IsTrue(isReset);
        }
    }
}
