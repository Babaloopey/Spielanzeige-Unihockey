﻿using System;
using Umsetzung_III.Stores;

namespace Umsetzung_III.TimerStates
{
    public class PausenState : State
    {
        public PausenState(SpielzeitStore spielzeiStore) : base(spielzeiStore)
        {
            durationMinute = 5;
            durationSecond = 0;
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

        public override int GetAbsoluteMinute()
        {
            return 0;
        }

        public override int GetAbsoluteSecond()
        {
            return 0;
        }
    }
}
