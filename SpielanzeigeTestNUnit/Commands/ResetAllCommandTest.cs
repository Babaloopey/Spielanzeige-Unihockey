using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Commands;
using Umsetzung_III;
using Moq;

namespace SpielanzeigeTestNUnit.Commands
{
    [TestFixture]
    public class ResetAllCommandTests
    {
        private Mock<SpielanzeigeViewModel> viewModelMock;
        private ResetAllCommand resetAllCommand;

        [SetUp]
        public void SetUp()
        {
            viewModelMock = new Mock<SpielanzeigeViewModel>();
            resetAllCommand = new ResetAllCommand(viewModelMock.Object);
        }

        [Test]
        public void Execute_ShouldInvokeResetViewModel()
        {
            // Act
            resetAllCommand.Execute(null);

            // Assert
            viewModelMock.Verify(v => v.ResetViewModel(), Times.Once);
        }
    }
}
