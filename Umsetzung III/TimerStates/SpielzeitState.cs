using System;
using Umsetzung_III.Stores;

namespace Umsetzung_III.TimerStates
{
    public class SpielzeitState : State
    {
        protected SpielanzeigeViewModel spielanzeige;

        public SpielzeitState(SpielzeitStore spielzeiStore, SpielanzeigeViewModel spielanzeigeViewModel) : base(spielzeiStore)
        {
            durationMinute = 20;
            durationSecond = 0;
            minute = durationMinute;
            second = durationSecond;

            spielanzeige = spielanzeigeViewModel;
        }

        public override void StartPause()
        {
            spielzeitStore.SetPausenState();

        }

        public override void StartTimeOut()
        {
            spielzeitStore.SetTimeOutState();
        }

        public override void TimeRanOut()
        {
            spielzeitStore.Stop();

            buzzer.Buzz();
            spielanzeige.Halbzeit++;
            Reset();
        }

        protected override void CustomReset()
        {}
    }
}
