using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using Umsetzung_III;
using Umsetzung_III.Stores;
using Umsetzung_III.TimerStates;


namespace SpielanzeigeTestNUnit.Stores
{


    [TestFixture]
    public class SpielzeitStoreTests
    {
        private SpielanzeigeViewModel viewModel;
        private SpielzeitStore sut;

        [SetUp]
        public void SetUp()
        {
            viewModel = new SpielanzeigeViewModel();
            sut = new SpielzeitStore(viewModel);
        }

        [Test]
        public void Start_ShouldSetIsTimeRunningToTrueAndIsStartButtonVisibleToFalse()
        {
            // Act
            sut.Start();

            // Assert
            Assert.IsTrue(sut.IsTimeRunning);
            Assert.IsFalse(sut.IsStartButtonVisible);
        }

        [Test]
        public void Stop_ShouldSetIsTimeRunningToFalseAndIsStartButtonVisibleToTrue()
        {
            // Arrange
            sut.Start();

            // Act
            sut.Stop();

            // Assert
            Assert.IsFalse(sut.IsTimeRunning);
            Assert.IsTrue(sut.IsStartButtonVisible);
        }

        [Test]
        public void ShouldGetCorrectTime_AtBeginningOfMatch()
        {
           
            // Act
            var actualSeconds = sut.GetActualSpielSecond();
            var actualMinute = sut.GetActualSpielMinute();

            // Assert
            Assert.AreEqual(0, actualSeconds);
            Assert.AreEqual(20, actualMinute);
        }

        [Test]
        public void Reset_ShouldReset()
        {
            // Arrange
            sut.MinuteMinusOne();
            sut.SecondMinusOne();
            Assert.AreEqual(18, sut.GetActualSpielMinute());
            Assert.AreEqual(59, sut.GetActualSpielSecond());

            // Act
            sut.Reset();

            // Assert
            Assert.AreEqual(20, sut.GetActualSpielMinute());
            Assert.AreEqual(0, sut.GetActualSpielSecond());
        }

        [Test]
        public void MinuteMinusOne_ShouldCallMinuteMinusOneAndInvokeOnSpielzeitChanged()
        {

            bool onSpielzeitChanged = false;
            sut.OnSpielzeitChanged += () => onSpielzeitChanged = true;

            // Act
            sut.MinuteMinusOne();

            // Assert
            Assert.AreEqual(19, sut.GetActualSpielMinute());
            Assert.IsTrue(onSpielzeitChanged);
        }

        [Test]
        public void MinutePlusOne_ShouldCallMinutePlusOneAndInvokeOnSpielzeitChanged()
        {
            bool onSpielzeitChanged = false;
            sut.OnSpielzeitChanged += () => onSpielzeitChanged = true;

            // Act
            sut.MinutePlusOne();

            // Assert
            Assert.AreEqual(21, sut.GetActualSpielMinute());
            Assert.IsTrue(onSpielzeitChanged);
        }
        [Test]
        public void SecondMinusOne_ShouldCallSecondMinusOneAndInvokeOnSpielzeitChanged()
        {

            bool onSpielzeitChanged = false;
            sut.OnSpielzeitChanged += () => onSpielzeitChanged = true;

            // Act
            sut.SecondMinusOne();

            // Assert
            Assert.AreEqual(59, sut.GetActualSpielSecond());
            Assert.IsTrue(onSpielzeitChanged);
        }

        [Test]
        public void SecondPlusOne_ShouldCallSecondPlusOneAndInvokeOnSpielzeitChanged()
        {
            bool onSpielzeitChanged = false;
            sut.OnSpielzeitChanged += () => onSpielzeitChanged = true;

            // Act
            sut.SecondPlusOne();

            // Assert
            Assert.AreEqual(01, sut.GetActualSpielSecond());
            Assert.IsTrue(onSpielzeitChanged);
        }

        [Test]
        public void TimeElapsed_ShouldInvokeOnSpielzeitChangedAndCallTimeElapsedOnTimerState_WhenIsTimeRunningIsTrue()
        {
            // Arrange
            bool onSpielzeitChanged = false;
            sut.OnSpielzeitChanged += () => onSpielzeitChanged = true;
            sut.IsTimeRunning = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.IsTrue(onSpielzeitChanged);
        }

        [Test]
        public void TimeElapsed_ShouldInvokeOnSpielzeitChangedAndNotCallTimeElapsedOnTimerState_WhenIsTimeRunningIsFalse()
        {
            // Arrange
            bool onSpielzeitChanged = false;
            sut.OnSpielzeitChanged += () => onSpielzeitChanged = true;
            sut.IsTimeRunning = false;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.IsTrue(onSpielzeitChanged);
        }

        [Test]
        public void SetSpielzeitState_ShouldSetTimerStateToSpielzeitStateAndInvokeOnTimeModeChanged()
        {
            // Arrange
            bool onSpielzeitChanged = false;
            sut.OnTimeModeChanged += () => onSpielzeitChanged = true;

            // Act
            sut.SetSpielzeitState();
            var actualButtonContent = sut.GetResetButtonContent();

            // Assert
            Assert.AreEqual("Reset Zeit", actualButtonContent);
            Assert.IsTrue(onSpielzeitChanged);
        }

        [Test]
        public void SetPausenState_ShouldSetTimerStateToPausenStateAndInvokeOnTimeModeChanged()
        {
            // Arrange
            bool onSpielzeitChanged = false;
            sut.OnTimeModeChanged += () => onSpielzeitChanged = true;

            // Act
            sut.SetPausenState();
            var actualButtonContent = sut.GetResetButtonContent();

            // Assert
            Assert.AreEqual("Zur Spielzeit", actualButtonContent);
            Assert.IsTrue(onSpielzeitChanged);
        }

        [Test]
        public void SetTimeOutState_ShouldSetTimerStateToTimeOutStateAndInvokeOnTimeModeChanged()
        {
            // Arrange
            bool onSpielzeitChanged = false;
            sut.OnTimeModeChanged += () => onSpielzeitChanged = true;

            // Act
            sut.SetTimeOutState();
            var actualButtonContent = sut.GetResetButtonContent();

            // Assert
            Assert.AreEqual("Zur Spielzeit", actualButtonContent);
            Assert.IsTrue(onSpielzeitChanged);
        }

        public void GetDurationOfHalftime()
        {
            // Act
            var actual = sut.GetDurationOfHalfTime();

            // Assert
            Assert.AreEqual(20, actual);
        }
    }
}
