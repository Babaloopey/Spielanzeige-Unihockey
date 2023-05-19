using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Model;

namespace SpielanzeigeTestNUnit.Model
{
    [TestFixture]
    public class AngezeigteStrafeTests
    {
        [Test]
        public void Constructor_ShouldSetMinuteAndSecondProperties()
        {
            // Arrange
            int minute = 5;
            int second = 30;

            // Act
            var angezeigteStrafe = new AngezeigteStrafe(minute, second);

            // Assert
            Assert.AreEqual(minute, angezeigteStrafe.minute);
            Assert.AreEqual(second, angezeigteStrafe.second);
        }

        [Test]
        public void Constructor_ShouldSetDisplayStrafeProperty()
        {
            // Arrange
            int minute = 5;
            int second = 30;
            string expectedDisplayStrafe = "05:30";

            // Act
            var angezeigteStrafe = new AngezeigteStrafe(minute, second);

            // Assert
            Assert.AreEqual(expectedDisplayStrafe, angezeigteStrafe.displayStrafe);
        }
    }
}
