using Moq;
using Umsetzung_III;
using Umsetzung_III.Controller;

namespace SpielanzeigeTest
{
    [TestClass]
    public class LogoStoreTest
    {
        [TestMethod]
        public void VisibilityTest()
        {
            var mock = new Mock<SpielanzeigeViewModel>();
            mock.Setup(mock => mock.HeimTeamStrafeRunning).Returns(true);
            mock.Setup(mock => mock.GastTeamStrafeRunning).Returns(false);

            LogoController logoStore = new LogoController(null);


            logoStore.CheckIfLogoMustBeVisible();


        }
    }
}