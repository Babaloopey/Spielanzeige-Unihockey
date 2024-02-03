using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Umsetzung_III;
using Umsetzung_III.Interface;
using Umsetzung_III.Model;
using static Umsetzung_III.Model.Actions;

namespace SpielanzeigeTestNUnit.Stores
{
    [TestFixture]
    public class StrafenStoreTests
    {
        private Mock<TimeDeliverer> spielzeitStoreMock;
        private StrafenStore sut;

        [SetUp]
        public void SetUp()
        {
            spielzeitStoreMock = new Mock<TimeDeliverer>();
            sut = new StrafenStore(spielzeitStoreMock.Object, Team.Gast);
        }

        [Test]
        public void Create_ShouldAddAngezeigteStrafe_WhenStrafeIsTwo()
        {
            // Arrange
            spielzeitStoreMock.Setup(s => s.GetActualSpielMinute()).Returns(10);
            spielzeitStoreMock.Setup(s => s.GetActualSpielSecond()).Returns(30);

            bool strafenChanged = false;
            sut.OnStrafenChanged += () => strafenChanged = true;
            // Act
            sut.Create(Strafe.Zwei);

            // Assert
            Assert.AreEqual(1, sut.Strafen.Count);
            Assert.AreEqual(8, sut.Strafen[0].minute);
            Assert.AreEqual(30, sut.Strafen[0].second);
            Assert.IsTrue(strafenChanged);
        }

        [Test]
        public void Create_ShouldAddTwoAngezeigteStrafen_WhenStrafeIsVier()
        {
            // Arrange
            spielzeitStoreMock.Setup(s => s.GetActualSpielMinute()).Returns(15);
            spielzeitStoreMock.Setup(s => s.GetActualSpielSecond()).Returns(45);

            bool strafenChanged = false;
            sut.OnStrafenChanged += () => strafenChanged = true;
            // Act
            sut.Create(Strafe.Vier);

            // Assert
            Assert.AreEqual(2, sut.Strafen.Count);
            Assert.AreEqual(13, sut.Strafen[0].minute);
            Assert.AreEqual(45, sut.Strafen[0].second);
            Assert.AreEqual(11, sut.Strafen[1].minute);
            Assert.AreEqual(45, sut.Strafen[1].second);
            Assert.IsTrue(strafenChanged);
        }

        [Test]
        public void Create_ShouldAddAngezeigteStrafe_WhenStrafeIsZehn()
        {
            // Arrange
            spielzeitStoreMock.Setup(s => s.GetActualSpielMinute()).Returns(5);
            spielzeitStoreMock.Setup(s => s.GetActualSpielSecond()).Returns(0);

            bool strafenChanged = false;
            sut.OnStrafenChanged += () => strafenChanged = true;

            // Act
            sut.Create(Strafe.Zehn);

            // Assert
            Assert.AreEqual(1, sut.Strafen.Count);
            Assert.AreEqual(15, sut.Strafen[0].minute);
            Assert.AreEqual(0, sut.Strafen[0].second);
            Assert.IsTrue(strafenChanged);
        }

        [Test]
        public void Delete_ShouldRemoveAngezeigteStrafe_WhenStrafeIsNotNull()
        {
            // Arrange
            var angezeigteStrafe = new AngezeigteStrafe(10, 30);
            sut.Strafen.Add(angezeigteStrafe);

            bool strafenChanged = false;
            sut.OnStrafenChanged += () => strafenChanged = true;
            // Act
            sut.Delete(angezeigteStrafe);

            // Assert
            Assert.AreEqual(0, sut.Strafen.Count);
            Assert.IsTrue(strafenChanged);
        }

        [Test]
        public void Delete_ShouldNotRemoveStrafe_WhenStrafeIsNull()
        {
            // Arrange
            var angezeigteStrafe = new AngezeigteStrafe(10, 30);
            sut.Strafen.Add(angezeigteStrafe);

            bool strafenChanged = false;
            sut.OnStrafenChanged += () => strafenChanged = true;
            // Act
            sut.Delete(null);

            // Assert
            Assert.AreEqual(1, sut.Strafen.Count);
            Assert.IsFalse(strafenChanged);
        }

        [Test]
        public void AdjustStrafenAnzeigeGroesse_ShouldSetSchriftToGross_WhenStrafenCountIsOne()
        {
            // Arrange
            sut.Strafen.Add(new AngezeigteStrafe(10, 30));

            // Act
            sut.AdjustStrafenAnzeigeGroesse();

            // Assert
            Assert.AreEqual(Schrift.gross, sut.StrafenAnzeigeGroesse);
        }

        [Test]
        public void AdjustStrafenAnzeigeGroesse_ShouldSetSchriftToMittel_WhenStrafenCountIsTwo()
        {
            // Arrange
            sut.Strafen.Add(new AngezeigteStrafe(10, 30));
            sut.Strafen.Add(new AngezeigteStrafe(12, 45));

            // Act
            sut.AdjustStrafenAnzeigeGroesse();

            // Assert
            Assert.AreEqual(Schrift.mittel, sut.StrafenAnzeigeGroesse);
        }

        [Test]
        public void AdjustStrafenAnzeigeGroesse_ShouldSetSchriftToKlein_WhenStrafenCountIsGreaterThanTwo()
        {
            // Arrange
            sut.Strafen.Add(new AngezeigteStrafe(10, 30));
            sut.Strafen.Add(new AngezeigteStrafe(12, 45));
            sut.Strafen.Add(new AngezeigteStrafe(14, 20));

            // Act
            sut.AdjustStrafenAnzeigeGroesse();

            // Assert
            Assert.AreEqual(Schrift.klein, sut.StrafenAnzeigeGroesse);
        }

        [Test]
        public void Reset_ShouldClearStrafen()
        {
            // Arrange
            sut.Strafen.Add(new AngezeigteStrafe(10, 30));
            sut.Strafen.Add(new AngezeigteStrafe(12, 45));

            bool strafenChanged = false;
            sut.OnStrafenChanged += () => strafenChanged = true;
            // Act
            sut.Reset();

            // Assert
            Assert.AreEqual(0, sut.Strafen.Count);
            Assert.IsTrue(strafenChanged);
        }

        [Test]
        public void IsStrafeRunning_ShouldBeFalse()
        {
            // Act
            var actual = sut.IsStrafeRunning;

            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void IsStrafeRunning_ShouldBeTrue()
        {
            // Arrange
            sut.Strafen.Add(new AngezeigteStrafe(10, 30));

            // Act
            var actual = sut.IsStrafeRunning;

            // Assert
            Assert.IsTrue(actual);
        }
    }
}
