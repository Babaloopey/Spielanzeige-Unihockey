using System;
using Umsetzung_III.Interface;
using Umsetzung_III.Services;

namespace Umsetzung_III.Controller
{
    public class PauseOrTimeOutButtonController : TimeSubscriber
    {
        public bool IsPauseButtonVisible = true;

        TimerService _timerStore;
        SpielanzeigeViewModel _viewModel;
        TimeDeliverer _spielzeitStore;

        public event Action OnButtonVisibilityChanged;

        public PauseOrTimeOutButtonController(SpielanzeigeViewModel spielanzeigeViewModel, TimeDeliverer spielzeitStore)
        {
            _viewModel = spielanzeigeViewModel;
            _spielzeitStore = spielzeitStore;

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
            return (_viewModel.SpielMinute == 0 || _viewModel.SpielMinute == _spielzeitStore.GetDurationOfHalfTime()) && _viewModel.SpielSekunde == 0;
        }

        private void ButtonVisibilityChanged()
        {
            OnButtonVisibilityChanged.Invoke();
        }
    }
}
