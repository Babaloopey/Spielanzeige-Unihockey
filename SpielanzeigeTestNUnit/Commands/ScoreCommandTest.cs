using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III;
using Umsetzung_III.Commands;
using Moq;
using static Umsetzung_III.Model.Actions;

namespace SpielanzeigeTestNUnit.Commands
{

    [TestFixture]
    public class ScoreCommandTests
    {
        private Mock<SpielanzeigeViewModel> spielanzeigeMock;
        private ScoreCommand scoreCommand;

        [SetUp]
        public void SetUp()
        {
            spielanzeigeMock = new Mock<SpielanzeigeViewModel>();
        }

        [Test]
        public void Execute_ShouldIncrementHeimTeamScore_WhenStandVeraenderungIsHoch()
        {
            // Arrange
            spielanzeigeMock.SetupGet(s => s.HeimTeamScore).Returns(5); 
            scoreCommand = new ScoreCommand(spielanzeigeMock.Object, Team.Heim, StandVeraenderung.Hoch);

            // Act
            scoreCommand.Execute(null);

            // Assert
            spielanzeigeMock.VerifySet(s => s.HeimTeamScore = 6);
            spielanzeigeMock.Verify(s => s.OnPropertyChanged("HeimTeamScore"), Times.Once);
        }

        [Test]
        public void Execute_ShouldDecrementHeimTeamScore_WhenStandVeraenderungIsRunterAndHeimTeamScoreIsGreaterThanZero()
        {
            // Arrange
            spielanzeigeMock.SetupGet(s => s.HeimTeamScore).Returns(3);

            // Act
            scoreCommand = new ScoreCommand(spielanzeigeMock.Object, Team.Heim, StandVeraenderung.Runter);
            scoreCommand.Execute(null);

            // Assert
            spielanzeigeMock.VerifySet(s => s.HeimTeamScore = 2);
            spielanzeigeMock.Verify(s => s.OnPropertyChanged("HeimTeamScore"), Times.Once);
        }

        [Test]
        public void Execute_ShouldNotDecrementHeimTeamScore_WhenStandVeraenderungIsRunterAndHeimTeamScoreIsZero()
        {
            // Arrange
            spielanzeigeMock.SetupGet(s => s.HeimTeamScore).Returns(0);

            // Act
            scoreCommand = new ScoreCommand(spielanzeigeMock.Object, Team.Heim, StandVeraenderung.Runter);
            scoreCommand.Execute(null);

            // Assert
            spielanzeigeMock.VerifySet(s => s.HeimTeamScore = It.IsAny<int>(), Times.Never);
            spielanzeigeMock.Verify(s => s.OnPropertyChanged("HeimTeamScore"), Times.Once);
        }

        [Test]
        public void Execute_ShouldIncrementGastTeamScore_WhenStandVeraenderungIsHoch()
        {
            // Arrange
            spielanzeigeMock.SetupGet(s => s.GastTeamScore).Returns(8);
            scoreCommand = new ScoreCommand(spielanzeigeMock.Object, Team.Gast, StandVeraenderung.Hoch);

            // Act
            scoreCommand.Execute(null);

            // Assert
            spielanzeigeMock.VerifySet(s => s.GastTeamScore = 9);
            spielanzeigeMock.Verify(s => s.OnPropertyChanged("GastTeamScore"), Times.Once);
        }

        [Test]
        public void Execute_ShouldDecrementGastTeamScore_WhenStandVeraenderungIsRunterAndGastTeamScoreIsGreaterThanZero()
        {
            // Arrange
            spielanzeigeMock.SetupGet(s => s.GastTeamScore).Returns(4);

            // Act
            scoreCommand = new ScoreCommand(spielanzeigeMock.Object, Team.Gast, StandVeraenderung.Runter);
            scoreCommand.Execute(null);

            // Assert
            spielanzeigeMock.VerifySet(s => s.GastTeamScore = 3);
            spielanzeigeMock.Verify(s => s.OnPropertyChanged("GastTeamScore"), Times.Once);
        }

        [Test]
        public void Execute_ShouldNotDecrementGastTeamScore_WhenStandVeraenderungIsRunterAndGastTeamScoreIsZero()
        {
            // Arrange
            spielanzeigeMock.SetupGet(s => s.GastTeamScore).Returns(0);

            // Act
            scoreCommand = new ScoreCommand(spielanzeigeMock.Object, Team.Gast, StandVeraenderung.Runter);
            scoreCommand.Execute(null);

            // Assert
            spielanzeigeMock.VerifySet(s => s.GastTeamScore = It.IsAny<int>(), Times.Never);
            spielanzeigeMock.Verify(s => s.OnPropertyChanged("GastTeamScore"), Times.Once);
        }

    }
}
