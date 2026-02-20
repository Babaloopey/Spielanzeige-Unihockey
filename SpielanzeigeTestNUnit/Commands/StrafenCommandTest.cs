using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Umsetzung_III;
using Umsetzung_III.Interface;
using Umsetzung_III.Commands;
using Umsetzung_III.Model;
using static Umsetzung_III.Model.Actions;



namespace SpielanzeigeTestNUnit.Commands
{
    [TestFixture]
    public class StrafenCommandTest
    {
        private Mock<StrafenStore> strafenStoreMock;
        private StrafenCommand scoreCommand;

        [SetUp]
        public void SetUp()
        {
            var timeDelivererMock = new Mock<TimeDeliverer>();
            strafenStoreMock = new Mock<StrafenStore>(timeDelivererMock.Object, Team.Heim);
        }

        [Test]
        public void Execute_ShouldDeleteStrafe_WhenStrafenCommandIsDelete()
        {
            // Arrange
            var strafe = new AngezeigteStrafe(2, 0);
            scoreCommand = new StrafenCommand(strafenStoreMock.Object, Strafe.Delete);

            // Act
            scoreCommand.Execute(strafe);

            // Assert
            strafenStoreMock.Verify(s => s.Delete(strafe), Times.Once);
        }

        [Test]
        public void Execute_ShouldNotDeleteStrafe_WhenStrafenCommandIsDeleteButParamEmpty()
        {
            // Arrange
            scoreCommand = new StrafenCommand(strafenStoreMock.Object, Strafe.Delete);

            // Act
            scoreCommand.Execute(null);

            // Assert
            strafenStoreMock.Verify(s => s.Delete(null), Times.Never);
        }

        [Test]
        public void Execute_ShouldCreateStrafe_WhenStrafenCommandIsNotDelete()
        {
            // Arrange
            var strafe = Strafe.Zwei;
            scoreCommand = new StrafenCommand(strafenStoreMock.Object, strafe);

            // Act
            scoreCommand.Execute(null);

            // Assert
            strafenStoreMock.Verify(s => s.Create(strafe), Times.Once);
        }
    }
}
