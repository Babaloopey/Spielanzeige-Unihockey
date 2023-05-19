
using Umsetzung_III.Stores;
using static Umsetzung_III.Model.Actions;

namespace Umsetzung_III
{
    // Command, der je nach ZeitAktion die Spielzeit des TimerStores beeinflusst
    public class TimeCommand : CommandBase
    {
        private readonly SpielzeitStore _spielzeitStore;
        private readonly ZeitAktion _zeitAktion;

        public TimeCommand(SpielzeitStore spielzeitStore ,ZeitAktion zeitAktion)
        {
            _spielzeitStore = spielzeitStore;
            _zeitAktion = zeitAktion;
        }

        public override void Execute(object? parameter)
        {
            switch (_zeitAktion)
            {
                case ZeitAktion.Start:
                    _spielzeitStore.Start();
                    break;
                case ZeitAktion.Stop:
                    _spielzeitStore.Stop();
                    break;
                case ZeitAktion.Reset:
                    _spielzeitStore.Reset();
                    break;
                case ZeitAktion.PlusOne:
                    _spielzeitStore.MinutePlusOne();
                    break;
                case ZeitAktion.MinusOne:
                    _spielzeitStore.MinuteMinusOne();
                    break;
                case ZeitAktion.Space:
                    if (_spielzeitStore.IsStartButtonVisible == true)
                    {
                        _spielzeitStore.Start();
                    }
                    else
                    {
                        _spielzeitStore.Stop();
                    }
                    break;
                case ZeitAktion.StartPausenzeit:
                    _spielzeitStore.StartPause();
                    break;
                case ZeitAktion.StartTimeOut:
                    _spielzeitStore.StartTimeOut();
                    break;
            }
        }
    }
}
