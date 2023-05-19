using Moq;
using Umsetzung_III;
using Umsetzung_III.Controller;
using NUnit.Framework;

namespace SpielanzeigeTestNUnit.Controller
{

    [TestFixture]
    public class LogoControllerTests
    {
        [Test]
        public void CheckIfLogoMustBeVisible_BothStrafeNotRunning_LogoVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.GastTeamStrafeRunning).Returns(false);
            viewModelMock.SetupGet(vm => vm.HeimTeamStrafeRunning).Returns(false);

            var sut = new LogoController(viewModelMock.Object);

            bool logoVisibilityChanged = false;
            sut.OnLogoVisibilityChanged += () => logoVisibilityChanged = true;

            // Act
            sut.CheckIfLogoMustBeVisible();

            // Assert
            Assert.IsTrue(sut.IsLogoVisible);
            Assert.IsTrue(logoVisibilityChanged);
        }

        [Test]
        public void CheckIfLogoMustBeVisible_GastStrafeRunning_LogoNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.GastTeamStrafeRunning).Returns(true);
            viewModelMock.SetupGet(vm => vm.HeimTeamStrafeRunning).Returns(false);

            var sut = new LogoController(viewModelMock.Object);

            bool logoVisibilityChanged = false;
            sut.OnLogoVisibilityChanged += () => logoVisibilityChanged = true;

            // Act
            sut.CheckIfLogoMustBeVisible();

            // Assert
            Assert.IsFalse(sut.IsLogoVisible);
            Assert.IsTrue(logoVisibilityChanged);
        }

        [Test]
        public void CheckIfLogoMustBeVisible_HeimStrafeRunning_LogoNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.GastTeamStrafeRunning).Returns(false);
            viewModelMock.SetupGet(vm => vm.HeimTeamStrafeRunning).Returns(true);

            var sut = new LogoController(viewModelMock.Object);

            bool logoVisibilityChanged = false;
            sut.OnLogoVisibilityChanged += () => logoVisibilityChanged = true;

            // Act
            sut.CheckIfLogoMustBeVisible();

            // Assert
            Assert.IsFalse(sut.IsLogoVisible);
            Assert.IsTrue(logoVisibilityChanged);
        }

        [Test]
        public void CheckIfLogoMustBeVisible_BothStrafeRunning_LogoNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.GastTeamStrafeRunning).Returns(true);
            viewModelMock.SetupGet(vm => vm.HeimTeamStrafeRunning).Returns(true);

            var sut = new LogoController(viewModelMock.Object);

            bool logoVisibilityChanged = false;
            sut.OnLogoVisibilityChanged += () => logoVisibilityChanged = true;

            // Act
            sut.CheckIfLogoMustBeVisible();

            // Assert
            Assert.IsFalse(sut.IsLogoVisible);
            Assert.IsTrue(logoVisibilityChanged);
        }
    }
}