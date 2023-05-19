using Moq;
using Umsetzung_III;
using Umsetzung_III.Controller;
using Umsetzung_III.Stores;
using Umsetzung_III.Services;

namespace SpielanzeigeTestNUnit.Controller
{
    [TestFixture]
    public class PauseOrTimeOutButtonControllerTest
    {

        [Test]
        public void TimeElapsed_BeginningOfMatch_ButtonVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(20);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(0);

            var spielzeitStoreMock = new Mock<SpielzeitStore>(viewModelMock.Object);
            spielzeitStoreMock.SetupSequence(td => td.GetDurationOfHalfTime()).Returns(20);

            var sut = new PauseOrTimeOutButtonController(viewModelMock.Object, spielzeitStoreMock.Object);

            bool buttonVisibilityChanged = false;
            sut.OnButtonVisibilityChanged += () => buttonVisibilityChanged = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.IsTrue(sut.IsPauseButtonVisible);
            Assert.IsTrue(buttonVisibilityChanged);
        }

        [Test]
        public void TimeElapsed_EndOfHalbzeit_ButtonVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(0);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(0);


            var spielzeitStoreMock = new Mock<SpielzeitStore>(viewModelMock.Object);
            spielzeitStoreMock.SetupSequence(td => td.GetDurationOfHalfTime()).Returns(20);

            var sut = new PauseOrTimeOutButtonController(viewModelMock.Object, spielzeitStoreMock.Object);

            bool buttonVisibilityChanged = false;
            sut.OnButtonVisibilityChanged += () => buttonVisibilityChanged = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.IsTrue(sut.IsPauseButtonVisible);
            Assert.IsTrue(buttonVisibilityChanged);
        }

        [Test]
        public void TimeElapsed_MatchAlreadyStarted_ButtonNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(19);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(59);


            var spielzeitStoreMock = new Mock<SpielzeitStore>(viewModelMock.Object);
            spielzeitStoreMock.SetupSequence(td => td.GetDurationOfHalfTime()).Returns(20);

            var sut = new PauseOrTimeOutButtonController(viewModelMock.Object, spielzeitStoreMock.Object);
            sut.IsPauseButtonVisible = true;

            bool buttonVisibilityChanged = false;
            sut.OnButtonVisibilityChanged += () => buttonVisibilityChanged = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.IsFalse(sut.IsPauseButtonVisible);
            Assert.IsTrue(buttonVisibilityChanged);
        }

        [Test]
        public void TimeElapsed_ButtonAlreadyNotVisible_ButtonNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(00);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(01);


            var spielzeitStoreMock = new Mock<SpielzeitStore>(viewModelMock.Object);
            spielzeitStoreMock.SetupSequence(td => td.GetDurationOfHalfTime()).Returns(20);

            var sut = new PauseOrTimeOutButtonController(viewModelMock.Object, spielzeitStoreMock.Object);
            sut.IsPauseButtonVisible = false;

            bool buttonVisibilityChanged = false;
            sut.OnButtonVisibilityChanged += () => buttonVisibilityChanged = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.IsFalse(sut.IsPauseButtonVisible);
            Assert.IsFalse(buttonVisibilityChanged);
        }
    }
}
