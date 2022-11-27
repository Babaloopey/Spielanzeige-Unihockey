using System;
using Umsetzung_III.Interface;
using Umsetzung_III.Services;

namespace Umsetzung_III.Controller
{
    internal class PauseOrTimeOutButtonController : TimeSubscriber
    {
        public bool IsPauseButtonVisible = true;

        TimerService _timerStore;
        SpielanzeigeViewModel _viewModel;

        public event Action OnButtonVisibilityChanged;

        public PauseOrTimeOutButtonController(SpielanzeigeViewModel spielanzeigeViewModel)
        {
            _viewModel = spielanzeigeViewModel;

            _timerStore = new TimerService();
            _timerStore.AddSubscriber(this);
        }

        public void TimeElapsed()
        {
            CheckIfPauseOrTimeOutIsShown();
        }

        private void CheckIfPauseOrTimeOutIsShown()
        {
            if (IsPausePossibleAndNotVisible())
            {
                IsPauseButtonVisible = true;
                ButtonVisibilityChanged();
            }
            else if (IsPauseButtonVisible == true)
            {
                IsPauseButtonVisible = false;
                ButtonVisibilityChanged();
            }
        }

        private bool IsPausePossibleAndNotVisible()
        {
            return (_viewModel.SpielMinute == 0 || _viewModel.SpielMinute == 20) && _viewModel.SpielSekunde == 0;
        }

        private void ButtonVisibilityChanged()
        {
            OnButtonVisibilityChanged.Invoke();
        }
    }
}
