using System;
using Umsetzung_III.Interface;
using Umsetzung_III.Services;

namespace Umsetzung_III.Controller
{
    public class EJuniorenModusController : TimeSubscriber
    {
        public bool IsSwitchable = true;

        private readonly TimerService _timerStore;
        private readonly SpielanzeigeViewModel _viewModel;
        private readonly TimeDeliverer _spielzeitStore;

        public event Action OnSwitchabilityChanged;

        public EJuniorenModusController(SpielanzeigeViewModel spielanzeigeViewModel, TimeDeliverer spielzeitStore)
        {
            _viewModel = spielanzeigeViewModel;
            _spielzeitStore = spielzeitStore;

            _timerStore = TimerService.GetTimerService();
            _timerStore.AddSubscriber(this);
        }

        public void TimeElapsed()
        {
            CheckIfSwitchable();
        }

        private void CheckIfSwitchable()
        {
            bool newValue = IsModusSwitchPossible();

            if (newValue == IsSwitchable)
            {
                return;
            }

            IsSwitchable = newValue;
            OnSwitchabilityChanged?.Invoke();
        }

        private bool IsModusSwitchPossible()
        {
            return (_viewModel.SpielMinute == _spielzeitStore.GetDurationOfHalfTime()) && _viewModel.SpielSekunde == 0 && _viewModel.Halbzeit == 1;
        }
    }
}
