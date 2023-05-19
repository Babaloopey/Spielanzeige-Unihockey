using System;
using Umsetzung_III.Interface;
using Umsetzung_III.Services;
using Umsetzung_III.TimerStates;

namespace Umsetzung_III.Stores
{
    public class SpielzeitStore : TimeSubscriber, TimeDeliverer
    {
        private TimerService _timerStore;
        private State timerState;
        private State timeOutState;
        private State pausenState;
        private State spielzeitState;
        private BuzzerService buzzer = new BuzzerService();

        public int Minute => timerState.GetMinute();
        public int Second => timerState.GetSecond();

        private bool IsTimeRunning = false;

        public bool IsStartButtonVisible = true;

        public event Action OnStartButtonVisibilityChanged;
        public event Action OnSpielzeitChanged;
        public event Action OnTimeModeChanged;

        public SpielzeitStore(SpielanzeigeViewModel viewModel)
        {

            buzzer.Buzz();

            _timerStore = new TimerService();
            _timerStore.AddSubscriber(this);

            spielzeitState = new SpielzeitState(this, viewModel);
            timeOutState = new TimeOutState(this);
            pausenState = new PausenState(this);

            timerState = spielzeitState;
        }

        public void Start()
        {
            IsTimeRunning = true;
            IsStartButtonVisible = false;
            StartButtonVisibilityChanged();
        }
        public void Stop()
        {
            IsTimeRunning = false;
            IsStartButtonVisible = true;
            StartButtonVisibilityChanged();
        }
        public void Reset()
        {
            timerState.Reset();
        }
        public void MinuteMinusOne()
        {
            timerState.MinuteMinusOne();
            SpielzeitChanged();
        }
        public void MinutePlusOne()
        {
            timerState.MinutePlusOne();
            SpielzeitChanged();
        }

        public void StartPause()
        {
            timerState.StartPause();
        }
        public void StartTimeOut()
        {
            timerState.StartTimeOut();
        }

        public void TimeElapsed()
        {
            SpielzeitChanged();
            if (IsTimeRunning)
            {
                timerState.TimeElapsed();
            }
        }

        public void SetSpielzeitState()
        {
            timerState = spielzeitState;
            TimeModeChanged();
        }
        public void SetPausenState()
        {
            timerState = pausenState;
            TimeModeChanged();
        }
        public void SetTimeOutState()
        {
            timerState = timeOutState;
            TimeModeChanged();
        }
        private void StartButtonVisibilityChanged()
        {
            OnStartButtonVisibilityChanged?.Invoke();
        }
        private void SpielzeitChanged()
        {
            OnSpielzeitChanged?.Invoke();
        }
        private void TimeModeChanged()
        {
            OnTimeModeChanged?.Invoke();
        }

        // interface functions
        public virtual int GetActualSpielMinute()
        {
            return spielzeitState.GetMinute();
        }
        public int GetActualSpielSecond()
        {
            return spielzeitState.GetSecond();
        }

        public virtual int GetDurationOfHalfTime()
        {
            return spielzeitState.GetDuration();
        }
        public string GetResetButtonContent()
        {
            if(timerState.GetType() == spielzeitState.GetType())
            {
                return "Reset Zeit";
            }
            else { return "Zur Spielzeit"; }

        }
    }
}
