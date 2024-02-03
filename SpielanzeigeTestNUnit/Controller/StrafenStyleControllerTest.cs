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

namespace SpielanzeigeTestNUnit.Controller
{
    [TestFixture]
    public class StrafenStyleControllerTest
    {

        [Test]
        public void GetStrafenMargin_SchriftKlein_Gast()
        {
            // Arrange
            var timeDelivererMock = new Mock<TimeDeliverer>();
            var strafenStoreMock = new Mock<StrafenStore>(timeDelivererMock.Object, Team.Gast);
            strafenStoreMock.SetupGet(vm => vm.StrafenAnzeigeGroesse).Returns(Schrift.klein);

            var sut = new StrafenStyleController();

            var expected = "0,-80,110,0";

            // Act
            var actual = sut.GetStrafenMargin(strafenStoreMock.Object);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetStrafenMargin_SchriftKlein_Heim()
        {
            // Arrange
            var timeDelivererMock = new Mock<TimeDeliverer>();
            var strafenStoreMock = new Mock<StrafenStore>(timeDelivererMock.Object, Team.Heim);
            strafenStoreMock.SetupGet(vm => vm.StrafenAnzeigeGroesse).Returns(Schrift.klein);

            var sut = new StrafenStyleController();

            var expected = "110,-80,0,0";

            // Act
            var actual = sut.GetStrafenMargin(strafenStoreMock.Object);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetStrafenMargin_SchriftGross_Gast()
        {
            // Arrange
            var timeDelivererMock = new Mock<TimeDeliverer>();
            var strafenStoreMock = new Mock<StrafenStore>(timeDelivererMock.Object, Team.Gast);
            strafenStoreMock.SetupGet(vm => vm.StrafenAnzeigeGroesse).Returns(Schrift.gross);

            var sut = new StrafenStyleController();

            var expected = "0,0,0,0";

            // Act
            var actual = sut.GetStrafenMargin(strafenStoreMock.Object);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetStrafenMargin_SchriftGross_Heim()
        {
            // Arrange
            var timeDelivererMock = new Mock<TimeDeliverer>();
            var strafenStoreMock = new Mock<StrafenStore>(timeDelivererMock.Object, Team.Heim);
            strafenStoreMock.SetupGet(vm => vm.StrafenAnzeigeGroesse).Returns(Schrift.gross);

            var sut = new StrafenStyleController();

            var expected = "0,0,0,0";

            // Act
            var actual = sut.GetStrafenMargin(strafenStoreMock.Object);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetStrafenMargin_SchriftNull()
        {
            // Arrange
            var timeDelivererMock = new Mock<TimeDeliverer>();
            var strafenStoreMock = new Mock<StrafenStore>(timeDelivererMock.Object, Team.Gast);
            strafenStoreMock.SetupGet(vm => vm.StrafenAnzeigeGroesse).Returns(null);

            var sut = new StrafenStyleController();

            var expected = "0,-80,110,0";

            // Act
            var actual = sut.GetStrafenMargin(strafenStoreMock.Object);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
