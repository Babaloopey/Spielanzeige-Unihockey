using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Stores;
using Moq;
using Umsetzung_III;
using Umsetzung_III.Controller;
using Umsetzung_III.Model;
using Umsetzung_III.Interface;
using static Umsetzung_III.Model.Actions;
using NUnit.Framework.Internal;

namespace SpielanzeigeTestNUnit.Controller
{
    [TestFixture]
    public class EJuniorenModusControllerTest
    {

        [Test]
        public void IsSwitchable_Changed()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(1);
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(20);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(0);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(td => td.GetDurationOfHalfTime()).Returns(20);

                   bool switchabilityChanged = false;
            var sut = new EJuniorenModusController(viewModelMock.Object,timeDelivererMock.Object);
            sut.OnSwitchabilityChanged += () => switchabilityChanged = true;
            sut.IsSwitchable = false;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.AreEqual(true, sut.IsSwitchable);
            Assert.AreEqual(true, switchabilityChanged);
        }

        [Test]
        public void IsSwitchable_NotChanged()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(1);
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(20);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(0);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(td => td.GetDurationOfHalfTime()).Returns(20);

            bool switchabilityChanged = false;
            var sut = new EJuniorenModusController(viewModelMock.Object, timeDelivererMock.Object);
            sut.OnSwitchabilityChanged += () => switchabilityChanged = true;
            sut.IsSwitchable = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.AreEqual(true, sut.IsSwitchable);
            Assert.AreEqual(false, switchabilityChanged);
        }

        [Test]
        public void IsNotSwitchable_Halbzeit()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(2);
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(20);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(0);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(td => td.GetDurationOfHalfTime()).Returns(20);

            bool switchabilityChanged = false;
            var sut = new EJuniorenModusController(viewModelMock.Object, timeDelivererMock.Object);
            sut.OnSwitchabilityChanged += () => switchabilityChanged = true;
            sut.IsSwitchable = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.AreEqual(false, sut.IsSwitchable);
            Assert.AreEqual(true, switchabilityChanged);
        }

        [Test]
        public void IsNotSwitchable_Spielsekunde()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(1);
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(20);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(1);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(td => td.GetDurationOfHalfTime()).Returns(20);

            bool switchabilityChanged = false;
            var sut = new EJuniorenModusController(viewModelMock.Object, timeDelivererMock.Object);
            sut.OnSwitchabilityChanged += () => switchabilityChanged = true;
            sut.IsSwitchable = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.AreEqual(false, sut.IsSwitchable);
            Assert.AreEqual(true, switchabilityChanged);
        }

        [Test]
        public void IsNotSwitchable_Spielminute()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(1);
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(19);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(0);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(td => td.GetDurationOfHalfTime()).Returns(20);

            bool switchabilityChanged = false;
            var sut = new EJuniorenModusController(viewModelMock.Object, timeDelivererMock.Object);
            sut.OnSwitchabilityChanged += () => switchabilityChanged = true;
            sut.IsSwitchable = true;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.AreEqual(false, sut.IsSwitchable);
            Assert.AreEqual(true, switchabilityChanged);
        }

        [Test]
        public void IsNotSwitchable_DurationOfHalftime()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.Halbzeit).Returns(1);
            viewModelMock.SetupGet(vm => vm.SpielMinute).Returns(20);
            viewModelMock.SetupGet(vm => vm.SpielSekunde).Returns(0);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(td => td.GetDurationOfHalfTime()).Returns(24);

            bool switchabilityChanged = false;
            var sut = new EJuniorenModusController(viewModelMock.Object, timeDelivererMock.Object);
            sut.OnSwitchabilityChanged += () => switchabilityChanged = true;
            sut.IsSwitchable = false;

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.AreEqual(false, sut.IsSwitchable);
            Assert.AreEqual(false, switchabilityChanged);
        }
    }
}
