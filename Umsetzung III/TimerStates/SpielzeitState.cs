using System;
using Umsetzung_III.Stores;

namespace Umsetzung_III.TimerStates
{
    public class SpielzeitState : State
    {
        protected SpielanzeigeViewModel spielanzeige;
        public Action OnSpielzeitRanOut;

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

            OnSpielzeitRanOut?.Invoke();
            Reset();
        }

        protected override void CustomReset()
        {}

        public override int GetAbsoluteMinute()
        {
            int vergangeneHalbzeiten = (spielanzeige.Halbzeit - 1) * durationMinute;
            int laufendeZeit = durationMinute - minute - (second > 0 ? 1 : 0);

            return vergangeneHalbzeiten + laufendeZeit;
        }

        public override int GetAbsoluteSecond()
        {
            return second == 0 ? 0 : 60 - second;
        }

        public void SetDurationMinute(int durationMinute)
        {
            this.durationMinute = durationMinute;
            this.minute = this.durationMinute;
        }
    }
}
