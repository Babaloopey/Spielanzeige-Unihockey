using System;
using Umsetzung_III.Interface;
using Umsetzung_III.Services;
using Umsetzung_III.Stores;

namespace Umsetzung_III.Controller
{
    public class EJuniorenBeepController: TimeSubscriber
    {
        private readonly SpielanzeigeViewModel _viewModel;
        private readonly TimeDeliverer _timeDeliverer;
        private readonly TimerService _timerStore;
        private readonly BuzzerService _buzzerService;

        private int _lastBeepIndex = -1;

        public EJuniorenBeepController(SpielanzeigeViewModel spielanzeigeViewModel, TimeDeliverer timeDeliverer, BuzzerService buzzerservice)
        {
            _timeDeliverer = timeDeliverer;
            _viewModel = spielanzeigeViewModel;
            _buzzerService = buzzerservice;

            _timerStore = TimerService.GetTimerService();
            _timerStore.AddSubscriber(this);
        }

        public void Reset()
        {
            _lastBeepIndex = -1;
        }

        public void TimeElapsed()
        {
            if (_viewModel.IsEJuniorenModus && _timeDeliverer.GetIsTimeRunning())
            {
                PeepIfNeeded();
            }
        }

        private void PeepIfNeeded()
        {
            int remainingSeconds = _timeDeliverer.GetRemainingTimeInSeconds();

            int currentIndex = (int)(remainingSeconds / 90);
            int rest = remainingSeconds % 90;

            if(_lastBeepIndex < 0)
            {
                _lastBeepIndex = currentIndex;
            }

            if (currentIndex < _lastBeepIndex && rest == 0)
            {
                _lastBeepIndex = currentIndex;
                _buzzerService.Peep();
            }  
        }
    }
}
