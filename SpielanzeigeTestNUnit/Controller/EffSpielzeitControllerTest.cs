using Moq;
using Umsetzung_III;
using Umsetzung_III.Controller;
using Umsetzung_III.Stores;


namespace SpielanzeigeTestNUnit.Controller
{
    [TestFixture]
    public class EffSpielzeitControllerTest
    {

        [Test]
        public void CheckIfEffektiveSpielzeitMustBeVisible_BeginningOfMath_EffNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(1);
            var timeDeliverer = new Mock<SpielzeitStore>(viewModelMock.Object);
            timeDeliverer.SetupSequence(td => td.GetActualSpielMinute()).Returns(20);

            var sut = new EffSpielzeitController(viewModelMock.Object, timeDeliverer.Object);

            bool effStrafzeitVisibilityChanged = false;
            sut.OnEffektiveSpielzeitVisibilityChanged += () => effStrafzeitVisibilityChanged = true;

            // Act
            sut.CheckIfEffektiveSpielzeitMustBeVisible();

            // Assert
            Assert.IsFalse(sut.IsEffektiveSpielzeitVisible);
            Assert.IsTrue(effStrafzeitVisibilityChanged);
        }

        [Test]
        public void CheckIfEffektiveSpielzeitMustBeVisible_FirstHalfUnderThreeMins_EffNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(1);

            var timeDeliverer = new Mock<SpielzeitStore>(viewModelMock.Object);
            timeDeliverer.SetupSequence(td => td.GetActualSpielMinute()).Returns(2);

            var sut = new EffSpielzeitController(viewModelMock.Object, timeDeliverer.Object);

            bool effStrafzeitVisibilityChanged = false;
            sut.OnEffektiveSpielzeitVisibilityChanged += () => effStrafzeitVisibilityChanged = true;

            // Act
            sut.CheckIfEffektiveSpielzeitMustBeVisible();

            // Assert
            Assert.IsFalse(sut.IsEffektiveSpielzeitVisible);
            Assert.IsTrue(effStrafzeitVisibilityChanged);
        }

        [Test]
        public void CheckIfEffektiveSpielzeitMustBeVisible_AlmostEff_EffNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(2);

            var timeDeliverer = new Mock<SpielzeitStore>(viewModelMock.Object);
            timeDeliverer.SetupSequence(td => td.GetActualSpielMinute()).Returns(3);

            var sut = new EffSpielzeitController(viewModelMock.Object, timeDeliverer.Object);

            bool effStrafzeitVisibilityChanged = false;
            sut.OnEffektiveSpielzeitVisibilityChanged += () => effStrafzeitVisibilityChanged = true;

            // Act
            sut.CheckIfEffektiveSpielzeitMustBeVisible();

            // Assert
            Assert.IsFalse(sut.IsEffektiveSpielzeitVisible);
            Assert.IsTrue(effStrafzeitVisibilityChanged);
        }

        [Test]
        public void CheckIfEffektiveSpielzeitMustBeVisible_EffSpielzeit_EffVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(2);

            var timeDeliverer = new Mock<SpielzeitStore>(viewModelMock.Object);
            timeDeliverer.SetupSequence(td => td.GetActualSpielMinute()).Returns(2);

            var sut = new EffSpielzeitController(viewModelMock.Object, timeDeliverer.Object);

            bool effStrafzeitVisibilityChanged = false;
            sut.OnEffektiveSpielzeitVisibilityChanged += () => effStrafzeitVisibilityChanged = true;

            // Act
            sut.CheckIfEffektiveSpielzeitMustBeVisible();

            // Assert
            Assert.IsTrue(sut.IsEffektiveSpielzeitVisible);
            Assert.IsTrue(effStrafzeitVisibilityChanged);
        }
        [Test]
        public void CheckIfEffektiveSpielzeitMustBeVisible_ThirdHalbzeit_EffNotVisible()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(3);

            var timeDeliverer = new Mock<SpielzeitStore>(viewModelMock.Object);
            timeDeliverer.SetupSequence(td => td.GetActualSpielMinute()).Returns(2);

            var sut = new EffSpielzeitController(viewModelMock.Object, timeDeliverer.Object);

            bool effStrafzeitVisibilityChanged = false;
            sut.OnEffektiveSpielzeitVisibilityChanged += () => effStrafzeitVisibilityChanged = true;

            // Act
            sut.CheckIfEffektiveSpielzeitMustBeVisible();

            // Assert
            Assert.IsFalse(sut.IsEffektiveSpielzeitVisible);
            Assert.IsTrue(effStrafzeitVisibilityChanged);
        }
    }
}
