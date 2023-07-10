using Moq;
using NUnit.Framework;
using Umsetzung_III.Stores;
using Umsetzung_III.Commands;
using Umsetzung_III;
using static Umsetzung_III.Model.Actions;

namespace SpielanzeigeTestNUnit.Commands
{

    [TestFixture]
    public class TimeCommandTests
    {
        private Mock<SpielzeitStore> spielzeitStoreMock;
        private TimeCommand timeCommand;

        [SetUp]
        public void SetUp()
        {
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            spielzeitStoreMock = new Mock<SpielzeitStore>(viewModelMock.Object);
        }

        [Test]
        public void Execute_ShouldInvokeStart_WhenZeitAktionIsStart()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.Start);
            
            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.Start(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokeStop_WhenZeitAktionIsStop()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.Stop);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.Stop(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokeReset_WhenZeitAktionIsReset()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.Reset);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.Reset(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokePlusOne_WhenZeitAktionIsPlusOne()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.PlusOne);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.MinutePlusOne(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokeMinusOne_WhenZeitAktionIsMinusOne()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.MinusOne);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.MinuteMinusOne(), Times.Once);
        }
        [Test]
        public void Execute_ShouldInvokePlusOne_WhenZeitAktionIsSecondPlusOne()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.SecondPlusOne);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.SecondPlusOne(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokeMinusOne_WhenZeitAktionIsSecondMinusOne()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.SecondMinusOne);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.SecondMinusOne(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokeStart_WhenZeitAktionIsSpace()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.Space);
            spielzeitStoreMock.SetupGet(sm => sm.IsStartButtonVisible).Returns(true);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.Start(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokeStop_WhenZeitAktionIsSpace()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.Space);
            spielzeitStoreMock.SetupGet(sm => sm.IsStartButtonVisible).Returns(false);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.Stop(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokeStartPause_WhenZeitAktionIsStartPausenzeit()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.StartPausenzeit);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.StartPause(), Times.Once);
        }

        [Test]
        public void Execute_ShouldInvokeStartTimeOut_WhenZeitAktionIsStartTimeOut()
        {
            // Arrange
            timeCommand = new TimeCommand(spielzeitStoreMock.Object, ZeitAktion.StartTimeOut);

            // Act
            timeCommand.Execute(null);

            // Assert
            spielzeitStoreMock.Verify(s => s.StartTimeOut(), Times.Once);
        }
    }

}
