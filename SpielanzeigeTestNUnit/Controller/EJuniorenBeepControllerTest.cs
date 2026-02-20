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
using NUnit.Framework.Internal;
using Umsetzung_III.Services;

namespace SpielanzeigeTestNUnit.Controller
{
    [TestFixture]
    public class EJuniorenBeepControllerTest
    {

        [Test]
        public void Beeps16TimesThrough24Minutes()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.IsEJuniorenModus).Returns(true);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(ss => ss.GetIsTimeRunning()).Returns(true);

            var sequence = timeDelivererMock
                .SetupSequence(ss => ss.GetRemainingTimeInSeconds());

            for (int i = 24 * 60; i >= 0; i--)
            {
                sequence = sequence.Returns(i);
            }

            int buzzes = 0;
            var buzzerService = new Mock<BuzzerService>();
            buzzerService.Setup(b => b.Peep()).Callback(() => buzzes += 1);

            var sut = new EJuniorenBeepController(viewModelMock.Object,timeDelivererMock.Object, buzzerService.Object);

            // Act
            for (int i = 24 * 60; i >= 0; i--)
            {
                sut.TimeElapsed();
            }
                

            // Assert
            Assert.AreEqual(16, buzzes);
        }

        [Test]
        public void NoBeep_FirstTime()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.IsEJuniorenModus).Returns(true);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(ss => ss.GetIsTimeRunning()).Returns(true);

            timeDelivererMock.Setup(ss => ss.GetRemainingTimeInSeconds()).Returns(90);

            int buzzes = 0;
            var buzzerService = new Mock<BuzzerService>();
            buzzerService.Setup(b => b.Peep()).Callback(() => buzzes += 1);

            var sut = new EJuniorenBeepController(viewModelMock.Object, timeDelivererMock.Object, buzzerService.Object);

            // Act
            sut.TimeElapsed();

            // Assert
            Assert.AreEqual(0, buzzes);
        }


        [Test]
        public void BeepOnce()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.IsEJuniorenModus).Returns(true);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(ss => ss.GetIsTimeRunning()).Returns(true);

            int buzzes = 0;
            var buzzerService = new Mock<BuzzerService>();
            buzzerService.Setup(b => b.Peep()).Callback(() => buzzes += 1);

            var sut = new EJuniorenBeepController(viewModelMock.Object, timeDelivererMock.Object, buzzerService.Object);

            // Act
            FirstSilentBeepAndSecondBeep(timeDelivererMock, sut);

            // Assert
            Assert.AreEqual(1, buzzes);
        }

        [Test]
        public void NoBeep_TimeIsNotRunning()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.IsEJuniorenModus).Returns(true);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(ss => ss.GetIsTimeRunning()).Returns(false);

            int buzzes = 0;
            var buzzerService = new Mock<BuzzerService>();
            buzzerService.Setup(b => b.Peep()).Callback(() => buzzes += 1);

            var sut = new EJuniorenBeepController(viewModelMock.Object, timeDelivererMock.Object, buzzerService.Object);

            // Act
            FirstSilentBeepAndSecondBeep(timeDelivererMock, sut);

            // Assert
            Assert.AreEqual(0, buzzes);
        }

        [Test]
        public void NoBeep_NoEJuniorenModus()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.IsEJuniorenModus).Returns(false);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(ss => ss.GetIsTimeRunning()).Returns(true);


            int buzzes = 0;
            var buzzerService = new Mock<BuzzerService>();
            buzzerService.Setup(b => b.Peep()).Callback(() => buzzes += 1);

            var sut = new EJuniorenBeepController(viewModelMock.Object, timeDelivererMock.Object, buzzerService.Object);

            // Act
            FirstSilentBeepAndSecondBeep(timeDelivererMock, sut);

            // Assert
            Assert.AreEqual(0, buzzes);
        }

        [Test]
        public void NoBeep_AfterTimeIsPlayedBack()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.IsEJuniorenModus).Returns(true);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(ss => ss.GetIsTimeRunning()).Returns(true);

            int buzzes = 0;
            var buzzerService = new Mock<BuzzerService>();
            buzzerService.Setup(b => b.Peep()).Callback(() => buzzes += 1);

            var sut = new EJuniorenBeepController(viewModelMock.Object, timeDelivererMock.Object, buzzerService.Object);

            // Act
            FirstSilentBeepAndSecondBeep(timeDelivererMock, sut);

            // Assert
            Assert.AreEqual(1, buzzes);

            // Arrange
            buzzes = 0;

            // Act
            FirstSilentBeepAndSecondBeep(timeDelivererMock, sut);

            // Assert
            Assert.AreEqual(0, buzzes);
        }

        [Test]
        public void BeepOnce_AfterReset()
        {
            // Arrange
            var viewModelMock = new Mock<SpielanzeigeViewModel>();
            viewModelMock.SetupGet(vm => vm.IsEJuniorenModus).Returns(true);

            var timeDelivererMock = new Mock<TimeDeliverer>();
            timeDelivererMock.Setup(ss => ss.GetIsTimeRunning()).Returns(true);

            int buzzes = 0;
            var buzzerService = new Mock<BuzzerService>();
            buzzerService.Setup(b => b.Peep()).Callback(() => buzzes += 1);

            var sut = new EJuniorenBeepController(viewModelMock.Object, timeDelivererMock.Object, buzzerService.Object);

            // Act
            FirstSilentBeepAndSecondBeep(timeDelivererMock, sut);

            // Assert
            Assert.AreEqual(1, buzzes);

            // Arrange
            sut.Reset();
            buzzes = 0;

            // Act
            FirstSilentBeepAndSecondBeep(timeDelivererMock, sut);

            // Assert
            Assert.AreEqual(1, buzzes);
        }

        public void FirstSilentBeepAndSecondBeep(Mock<TimeDeliverer> timeDelivererMock, EJuniorenBeepController sut)
        {
            // Arrange
            timeDelivererMock.Setup(ss => ss.GetRemainingTimeInSeconds()).Returns(180);

            // Act
            sut.TimeElapsed();

            // Arrange
            timeDelivererMock.Setup(ss => ss.GetRemainingTimeInSeconds()).Returns(90);

            // Act
            sut.TimeElapsed();
        }
    }
}
