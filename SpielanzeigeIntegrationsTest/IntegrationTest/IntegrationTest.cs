using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III;

namespace IntegrationTest
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
        public void checkSpielanzeigeAfterInitializationForCorrectValues()
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

            Assert.AreEqual(0, sut.HeimTeamScore); // --
            Assert.AreEqual(0, sut.GastTeamScore);
        }

        [Test]
        public void inputTeamNames()
        {
            string heimTeam = "Beromünster";
            string gastTeam = "Hildisrieden";
            sut.HeimTeamName = heimTeam;
            sut.GastTeamName = gastTeam;
            Assert.AreEqual(heimTeam, sut.HeimTeamName);
            Assert.AreEqual(gastTeam, sut.GastTeamName);
        }

        [Test]
        public void StartGameAndCheckForStopButton()
        {
            Assert.IsTrue(sut.ButtonVisibilityStart);
            Assert.IsFalse(sut.ButtonVisibilityStop);


            sut.StartTime.Execute(null);
            Assert.IsFalse(sut.ButtonVisibilityStart);
            Assert.IsTrue(sut.ButtonVisibilityStop);

            sut.StopTime.Execute(null);
            Assert.IsTrue(sut.ButtonVisibilityStart);
            Assert.IsFalse(sut.ButtonVisibilityStop);
        }

        [Test]
        public void ChangeHeimTeamScoreAndCheck()
        {
            Assert.AreEqual(0, sut.HeimTeamScore);

            sut.HeimScoreDown.Execute(null);
            Assert.AreEqual(0, sut.HeimTeamScore);

            sut.HeimScoreUp.Execute(null);
            Assert.AreEqual(1, sut.HeimTeamScore);

            sut.HeimScoreDown.Execute(null);
            Assert.AreEqual(0, sut.HeimTeamScore);
        }

        [Test]
        public void ChangeGastTeamScoreAndCheck()
        {
            Assert.AreEqual(0, sut.GastTeamScore);

            sut.GastScoreDown.Execute(null);
            Assert.AreEqual(0, sut.GastTeamScore);

            sut.GastScoreUp.Execute(null);
            Assert.AreEqual(1, sut.GastTeamScore);

            sut.GastScoreDown.Execute(null);
            Assert.AreEqual(0, sut.GastTeamScore);
        }

        [Test]
        public void AddStrafenTeamHeimAndCheck()
        {
            sut.HeimStrafeZwei.Execute(null);
            Assert.AreEqual(1, sut.HeimTeamStrafe.Count);
            Assert.IsTrue(sut.HeimTeamStrafeRunning);
            Assert.IsFalse(sut.LogoVisibility);

            sut.HeimStrafeDelete.Execute(sut.HeimTeamStrafe[0]);
            Assert.AreEqual(0, sut.HeimTeamStrafe.Count);
            Assert.IsFalse(sut.HeimTeamStrafeRunning);
            Assert.IsTrue(sut.LogoVisibility);


            sut.HeimStrafeZwei.Execute(null);
            sut.HeimStrafeVier.Execute(null);
            sut.HeimStrafeZehn.Execute(null);
            Assert.AreEqual(4, sut.HeimTeamStrafe.Count);
            Assert.IsTrue(sut.HeimTeamStrafeRunning);
            Assert.AreEqual(55, sut.HeimStrafenAnzeigeGroesse);
            Assert.AreEqual("110,-80,0,0", sut.HeimStrafeMargin);
            Assert.AreEqual(0, sut.GastTeamStrafe.Count);
            Assert.IsFalse(sut.GastTeamStrafeRunning);
            Assert.IsFalse(sut.LogoVisibility);
        }
        [Test]
        public void AddStrafenTeamGastAndCheck()
        {
            sut.GastStrafeZwei.Execute(null);
            Assert.AreEqual(1, sut.GastTeamStrafe.Count);
            Assert.IsTrue(sut.GastTeamStrafeRunning);
            Assert.IsFalse(sut.LogoVisibility);


            sut.GastStrafeDelete.Execute(sut.GastTeamStrafe[0]);
            Assert.AreEqual(0, sut.GastTeamStrafe.Count);
            Assert.IsFalse(sut.GastTeamStrafeRunning);
            Assert.IsTrue(sut.LogoVisibility);

            sut.GastStrafeZwei.Execute(null);
            sut.GastStrafeVier.Execute(null);
            sut.GastStrafeZehn.Execute(null);
            Assert.AreEqual(4, sut.GastTeamStrafe.Count);
            Assert.IsTrue(sut.GastTeamStrafeRunning);
            Assert.AreEqual(55, sut.GastStrafenAnzeigeGroesse);
            Assert.AreEqual("0,-80,110,0", sut.GastStrafeMargin);
            Assert.AreEqual(0, sut.HeimTeamStrafe.Count);
            Assert.IsFalse(sut.HeimTeamStrafeRunning);
            Assert.IsFalse(sut.LogoVisibility);
        }

        [Test]
        public void ChangeTimeAndCheck()
        {
            sut.TimeMinusOne.Execute(null);
            sut.TimeMinusOne.Execute(null);
            sut.SecondMinusOne.Execute(null);
            Assert.AreEqual(17, sut.SpielMinute);
            Assert.AreEqual(59, sut.SpielSekunde);
            Assert.AreEqual("17:59", sut.Spielzeit);

            sut.TimePlusOne.Execute(null);
            sut.SecondPlusOne.Execute(null);
            sut.SecondPlusOne.Execute(null);
            Assert.AreEqual(19, sut.SpielMinute);
            Assert.AreEqual(1, sut.SpielSekunde);
            Assert.AreEqual("19:01", sut.Spielzeit);

            TimeMinusTwenty();
            SecondsMinusTwenty();
            Assert.AreEqual(0, sut.SpielMinute);
            Assert.AreEqual(0, sut.SpielSekunde);
            Assert.AreEqual("00:00", sut.Spielzeit);
        }

        [Test]
        public void ChangeToPauseAndCheck()
        {
            sut.StartPausenzeit.Execute(null);
            Assert.AreEqual(5, sut.SpielMinute);
            Assert.AreEqual(0, sut.SpielSekunde);
            Assert.AreEqual("05:00", sut.Spielzeit);

            sut.ResetTime.Execute(null);
            Assert.AreEqual(20, sut.SpielMinute);
            Assert.AreEqual(0, sut.SpielSekunde);
            Assert.AreEqual("20:00", sut.Spielzeit);
        }

        [Test]
        public void ChangeToTimeoutAndCheck()
        {
            sut.StartTimeOut.Execute(null);
            Assert.AreEqual(0, sut.SpielMinute);
            Assert.AreEqual(30, sut.SpielSekunde);
            Assert.AreEqual("00:30", sut.Spielzeit);

            sut.ResetTime.Execute(null);
            Assert.AreEqual(20, sut.SpielMinute);
            Assert.AreEqual(0, sut.SpielSekunde);
            Assert.AreEqual("20:00", sut.Spielzeit);
        }


        [Test]
        public void ChangeTimeAndReset()
        {
            sut.TimeMinusOne.Execute(null);
            Assert.AreEqual(19, sut.SpielMinute);

            sut.ResetTime.Execute(null);
            Assert.AreEqual(20, sut.SpielMinute);
        }

        [Test]
        public void FillEveryFieldAndResetAllTest()
        {
            sut.TimeMinusOne.Execute(null);
            sut.HeimScoreUp.Execute(null);
            sut.GastScoreUp.Execute(null);
            sut.HeimTeamName = "Beromünster";
            sut.GastTeamName = "Beromünster";
            sut.HeimStrafeZwei.Execute(null);
            sut.GastStrafeZwei.Execute(null);

            sut.ResetAll.Execute(null);
            checkSpielanzeigeAfterInitializationForCorrectValues();
        }

        [Test]
        public void PressBuzzer()
        {
            sut.BuzzerPressed.Execute(null);
        }

        public void TimeMinusTwenty()
        {
            for (int i = 0; i <= 20; i++)
            {
                sut.TimeMinusOne.Execute(null);
            }
        }
        public void SecondsMinusTwenty()
        {
            for (int i = 0; i <= 20; i++)
            {
                sut.SecondMinusOne.Execute(null);
            }
        }
    }
}
