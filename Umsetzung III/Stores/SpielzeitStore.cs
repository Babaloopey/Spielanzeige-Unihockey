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

        public bool IsTimeRunning = false;

        public virtual bool IsStartButtonVisible { get; set; } = true;

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

        public virtual void Start()
        {
            IsTimeRunning = true;
            IsStartButtonVisible = false;
            StartButtonVisibilityChanged();
        }
        public virtual void Stop()
        {
            IsTimeRunning = false;
            IsStartButtonVisible = true;
            StartButtonVisibilityChanged();
        }
        public virtual void Reset()
        {
            timerState.Reset();
        }
        public virtual void MinuteMinusOne()
        {
            timerState.MinuteMinusOne();
            SpielzeitChanged();
        }
        public virtual void MinutePlusOne()
        {
            timerState.MinutePlusOne();
            SpielzeitChanged();
        }
        public virtual void SecondMinusOne()
        {
            timerState.SecondMinusOne();
            SpielzeitChanged();
        }
        public virtual void SecondPlusOne()
        {
            timerState.SecondPlusOne();
            SpielzeitChanged();
        }

        public virtual void StartPause()
        {
            timerState.StartPause();
        }
        public virtual void StartTimeOut()
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

        public virtual void SetSpielzeitState()
        {
            timerState = spielzeitState;
            TimeModeChanged();
        }
        public virtual void SetPausenState()
        {
            timerState = pausenState;
            TimeModeChanged();
        }
        public virtual void SetTimeOutState()
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

        public virtual int GetAbsoluteSpielMinute()
        {
            return spielzeitState.GetAbsoluteMinute();
        }
        public int GetAbsoluteSpielSecond()
        {
            return spielzeitState.GetAbsoluteSecond();
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
