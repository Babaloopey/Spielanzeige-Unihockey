using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Umsetzung_III.Stores
{
    public class SpielzeitStore
    {
        private readonly SpielanzeigeViewModel _viewModel;

        private int _sekunde;
        private int _minute;
        private int _duration;
        private int _timerIterations = 0;
        private int REGULARDURATION = 20;
        private bool PauseRunning = false;

        public bool EffektiveSpielzeitVisibility;
        public bool PauseMoeglich = true;

        public string Spielzeit => _minute.ToString("00") + ":" + _sekunde.ToString("00");
        public int SpielMinute => _minute;
        public int SpielSekunde => _sekunde;

        public event Action OnSpielzeitChanged;
        public event Action OnSpielzeitAbgelaufen;
        public event Action EffektiveSpielzeitVisibilityChanged;
        public event Action StartPausenzeit;

        public SpielzeitStore( SpielanzeigeViewModel viewModel)
        {
            _duration = REGULARDURATION;
            Console.Beep(350, 1000);

            _viewModel = viewModel;

            _sekunde = 0;
            _minute = _duration;
        }

        public void startPausenzeit()
        {
            if (isHalbzeitOver())
            {
                PauseRunning = true;
                this._minute = 5;
                WennPauseGestartet();
                SpielzeitChanged();
            }
        }

        public bool isHalbzeitOver()
        {
            if(_minute == REGULARDURATION && _sekunde == 0)
            {
                return true;
            }
            else { return false; }
        }

        public void Reset()
        {
            _minute = _duration;
            _sekunde = 0;
            SpielzeitChanged();
            CheckEffektiveSpielzeit();
            PauseRunning = false;
        }
        public void MinutePlusOne()
        {
            _minute++;
            SpielzeitChanged();
            CheckEffektiveSpielzeit();
        }
        public void MinuteMinusOne()
        {
            if (_minute > 0)
            {
                _minute--;
                SpielzeitChanged();
                CheckEffektiveSpielzeit();
            }
        }

        public void TimerElapsed()
        {
            _timerIterations++;

            if (_timerIterations >= 10)
            {
                CountOneSecond();
            }
        }

        private void CountOneSecond()
        {
            _timerIterations = 0;

            if (_minute == 0 && _sekunde == 0)
            {
                SpielzeitAbgelaufen();
            }
            else if (_sekunde == 0)
            {
                MinuteMinusOne();
                _sekunde = 59;
            }
            else
            {
                _sekunde--;
            }

            SpielzeitChanged();
        }

        private void CheckEffektiveSpielzeit()
        {
            if (_minute < 3 && _viewModel.Halbzeit == 2)
            {
                EffektiveSpielzeitVisibility = true;
            }
            else
            {
                EffektiveSpielzeitVisibility = false;
            }
            EffektiveSpielzeitChanged();
        }

        private void SpielzeitAbgelaufen()
        {

            WennSpielzeitAbgelaufen();

            Console.Beep(350, 2000);

            Timer wartezeit = new Timer(3000);
            wartezeit.Start();
            wartezeit.Elapsed += (sender, args) =>
            {
                CheckWhetherHalbzeitMustBeIncremented();
                Reset();
               
                
                wartezeit.Dispose();
            };
        }

        private void CheckWhetherHalbzeitMustBeIncremented()
        {
            if (!PauseRunning)
            {
                _viewModel.Halbzeit += 1;
            }
        }

        private void SpielzeitChanged()
        {
            OnSpielzeitChanged?.Invoke();
        }

        private void EffektiveSpielzeitChanged()
        {
            EffektiveSpielzeitVisibilityChanged?.Invoke();
        }

        private void WennSpielzeitAbgelaufen()
        {
            OnSpielzeitAbgelaufen?.Invoke();
        }

        private void WennPauseGestartet()
        {
            StartPausenzeit?.Invoke();
        }
    }
}
