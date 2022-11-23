using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umsetzung_III.Stores;
using static Umsetzung_III.Actions;

namespace Umsetzung_III
{
    // Command, der je nach ZeitAktion die Spielzeit des TimerStores beeinflusst
    internal class TimeCommand : CommandBase
    {
        private readonly TimerStore _timerStore;
        private readonly SpielzeitStore _spielzeitStore;
        private readonly ZeitAktion _zeitAktion;

        public TimeCommand(TimerStore timerStore, SpielzeitStore spielzeitStore ,ZeitAktion zeitAktion)
        {
            this._timerStore = timerStore;
            this._spielzeitStore = spielzeitStore;
            this._zeitAktion = zeitAktion;
        }

        public override void Execute(object? parameter)
        {
            switch (_zeitAktion)
            {
                case ZeitAktion.Start:
                    _timerStore.Start();
                    break;
                case ZeitAktion.Stop:
                    _timerStore.Stop();
                    break;
                case ZeitAktion.Reset:
                    _timerStore.Reset();
                    _spielzeitStore.Reset();
                    break;
                case ZeitAktion.PlusOne:
                    _spielzeitStore.MinutePlusOne();
                    break;
                case ZeitAktion.MinusOne:
                    _spielzeitStore.MinuteMinusOne();
                    break;
                case ZeitAktion.Space:
                    if (_timerStore.IsStartButtonVisible == true)
                    {
                        _timerStore.Start();
                    }
                    else
                    {
                        _timerStore.Stop();
                    }
                    break;
                case ZeitAktion.StartPausenzeit:
                    _spielzeitStore.startPausenzeit();
                    break;
            }
        }
    }
}
