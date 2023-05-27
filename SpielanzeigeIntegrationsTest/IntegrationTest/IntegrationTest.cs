using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III;

namespace SpielanzeigeTestNUnit.IntegrationTest
{
    [TestFixture]
    public class IntegrationTest
    {
        SpielanzeigeViewModel sut;

        [SetUp]
        public void SetUp()
        {
            sut = new SpielanzeigeViewModel();
        }

        [Test]
        public void checkSUTAfterInitializationForCorrectValues()
        {
            Assert.IsNotNull(sut);
            Assert.IsTrue(sut.ButtonVisibilityStart);
            Assert.IsFalse(sut.ButtonVisibilityStop);
            Assert.IsTrue(sut.ButtonVisibilityPause);
            Assert.IsFalse(sut.ButtonVisibilityTimeOut);
            Assert.IsTrue(sut.LogoVisibility);
            Assert.IsFalse(sut.EffektiveSpielzeitVisibility);

            Assert.AreEqual(20, sut.SpielMinute);
            Assert.AreEqual(0, sut.SpielSekunde);
            Assert.AreEqual("20:00", sut.Spielzeit);

            Assert.AreEqual(0, sut.HeimTeamStrafe.Count);
            Assert.AreEqual(0, sut.GastTeamStrafe.Count);
            Assert.IsFalse(sut.HeimTeamStrafeRunning);
            Assert.IsFalse(sut.GastTeamStrafeRunning);

            Assert.AreEqual("Reset Zeit", sut.ResetButtonContent);
            Assert.AreEqual(1, sut.Halbzeit);

            Assert.AreEqual("Heim", sut.HeimTeamName);
            Assert.AreEqual("Gast", sut.GastTeamName);

            Assert.AreEqual(0, sut.HeimTeamScore);
            Assert.AreEqual(0, sut.GastTeamScore);
        }
    }
}
