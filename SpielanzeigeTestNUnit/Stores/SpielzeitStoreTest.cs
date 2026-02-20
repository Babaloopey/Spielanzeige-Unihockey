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

        [Test]
        public void GetDurationOfHalftime()
        {
            // Act
            var actual = sut.GetDurationOfHalfTime();

            // Assert
            Assert.AreEqual(20, actual);
        }

        [Test]
        public void GetAbsoluteSpielZeit_AtTheStart()
        {
            // Act
            var actualMinute = sut.GetAbsoluteSpielMinute();
            var actualSecond = sut.GetAbsoluteSpielSecond();

            // Assert
            Assert.AreEqual(0, actualMinute);
            Assert.AreEqual(0, actualSecond);
        }

        [Test]
        public void GetAbsoluteSpielZeit_SecondHalbzeit()
        {
            // Arrange
            this.viewModel.Halbzeit = 2;

            // Act
            var actualMinute = sut.GetAbsoluteSpielMinute();
            var actualSecond = sut.GetAbsoluteSpielSecond();

            // Assert
            Assert.AreEqual(20, actualMinute);
            Assert.AreEqual(0, actualSecond);
        }

        [Test]
        public void GetAbsoluteSpielZeit_SecondHalbzeitAndTwoMinutesDown()
        {
            // Arrange
            this.viewModel.Halbzeit = 2;
            ElapseTimeByMinutes(2);

            // Act
            var actualMinute = sut.GetAbsoluteSpielMinute();
            var actualSecond = sut.GetAbsoluteSpielSecond();

            // Assert
            Assert.AreEqual(22, actualMinute);
            Assert.AreEqual(0, actualSecond);
        }


        [Test]
        public void GetAbsoluteSpielZeit_TwoMinutesAndTwoSecondsDown()
        {
            // Arrange
            ElapseTimeByMinutes(2);
            ElapseTimeBySeconds(2);

            // Act
            var actualMinute = sut.GetAbsoluteSpielMinute();
            var actualSecond = sut.GetAbsoluteSpielSecond();

            // Assert
            Assert.AreEqual(2, actualMinute);
            Assert.AreEqual(2, actualSecond);
        }


        [Test]
        public void GetAbsoluteSpielZeit_TwoMinutesAndFityNineSecondsDown()
        {
            // Arrange
            ElapseTimeByMinutes(2);
            ElapseTimeBySeconds(59);

            // Act
            var actualMinute = sut.GetAbsoluteSpielMinute();
            var actualSecond = sut.GetAbsoluteSpielSecond();

            // Assert
            Assert.AreEqual(2, actualMinute);
            Assert.AreEqual(59, actualSecond);
        }

        [Test]
        public void GetAbsoluteSpielZeit_TwoMinutesAndSixtySecondsDown()
        {
            // Arrange
            ElapseTimeByMinutes(2);
            ElapseTimeBySeconds(60);

            // Act
            var actualMinute = sut.GetAbsoluteSpielMinute();
            var actualSecond = sut.GetAbsoluteSpielSecond();

            // Assert
            Assert.AreEqual(3, actualMinute);
            Assert.AreEqual(00, actualSecond);
        }

        [Test]
        public void GetRemainingTimeInSeconds_TwentyMinutes()
        {
            // Act
            var result = sut.GetRemainingTimeInSeconds();

            // Assert
            Assert.AreEqual(1200, result);
        }

        [Test]
        public void GetRemainingTimeInSeconds_TwoMinutesAndThirtyFiveSecondsDown()
        {
            // Arrange
            ElapseTimeByMinutes(2);
            ElapseTimeBySeconds(35);

            // Act
            var result = sut.GetRemainingTimeInSeconds();

            // Assert
            Assert.AreEqual(1045, result);
        }

        [Test]
        public void GetRemainingTimeInSeconds_TwentyMinutesDown()
        {
            // Arrange
            ElapseTimeByMinutes(20);

            // Act
            var result = sut.GetRemainingTimeInSeconds();

            // Assert
            Assert.AreEqual(0, result);
        }

        public void ElapseTimeByMinutes(int minutes)
        {
            for(int i = 0; i < minutes; i++)
            {
                sut.MinuteMinusOne();
            }
        }

        public void ElapseTimeBySeconds(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                sut.SecondMinusOne();
            }
        }
    }
}
