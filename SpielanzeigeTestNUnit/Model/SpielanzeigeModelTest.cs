using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Model;

namespace SpielanzeigeTestNUnit.Model
{
    [TestFixture]
    public class SpielanzeigeModelTests
    {
        [Test]
        public void Constructor_ShouldSetDefaultPropertyValues()
        {
            // Arrange & Act
            var spielanzeigeModel = new SpielanzeigeModel();

            // Assert
            Assert.AreEqual("Heim", spielanzeigeModel.HeimTeamName);
            Assert.AreEqual("Gast", spielanzeigeModel.GastTeamName);
            Assert.AreEqual(0, spielanzeigeModel.HeimTeamScore);
            Assert.AreEqual(0, spielanzeigeModel.GastTeamScore);
            Assert.AreEqual(1, spielanzeigeModel.Halbzeit);
        }

        [Test]
        public void ResetModel_ShouldResetPropertyValuesToDefaults()
        {
            // Arrange
            var spielanzeigeModel = new SpielanzeigeModel();
            spielanzeigeModel.HeimTeamName = "Updated Heim";
            spielanzeigeModel.GastTeamName = "Updated Gast";
            spielanzeigeModel.HeimTeamScore = 10;
            spielanzeigeModel.GastTeamScore = 5;
            spielanzeigeModel.Halbzeit = 2;

            // Act
            spielanzeigeModel.ResetModel();

            // Assert
            Assert.AreEqual("Heim", spielanzeigeModel.HeimTeamName);
            Assert.AreEqual("Gast", spielanzeigeModel.GastTeamName);
            Assert.AreEqual(0, spielanzeigeModel.HeimTeamScore);
            Assert.AreEqual(0, spielanzeigeModel.GastTeamScore);
            Assert.AreEqual(1, spielanzeigeModel.Halbzeit);
        }
    }
}
