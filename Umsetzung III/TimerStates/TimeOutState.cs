using System;
using Umsetzung_III.Stores;

namespace Umsetzung_III.TimerStates
{
    internal class TimeOutState : State
    {
        public TimeOutState(SpielzeitStore spielzeiStore) : base(spielzeiStore)
        {
            durationMinute = 0;
            durationSecond = 30;
            minute = durationMinute;
            second = durationSecond;

        }

        public override void StartPause()
        {}

        public override void StartTimeOut()
        {}

        public override void TimeRanOut()
        {
            spielzeitStore.Stop();

            buzzer.Buzz();

            Reset();
        }

        protected override void CustomReset()
        {
            spielzeitStore.SetSpielzeitState();
        }
    }
}
