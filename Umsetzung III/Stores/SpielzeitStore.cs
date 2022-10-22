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
        private readonly int _duration;
        private int _timerIterations = 0;

        public bool EffektiveSpielzeitVisibility;
        public string Spielzeit => _minute.ToString("00") + ":" + _sekunde.ToString("00");
        public int SpielMinute => _minute;
        public int SpielSekunde => _sekunde;

        public event Action OnSpielzeitChanged;
        public event Action OnSpielzeitAbgelaufen;
        public event Action EffektiveSpielzeitVisibilityChanged;

        public SpielzeitStore(int duration, SpielanzeigeViewModel viewModel)
        {
            Console.Beep(350, 1000);

            _viewModel = viewModel;
            _duration = duration;

            _sekunde = 0;
            _minute = _duration;
        }

        public void Reset()
        {
            _minute = _duration;
            _sekunde = 0;
            SpielzeitChanged();
            CheckEffektiveSpielzeit();

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

                Reset();
                _viewModel.Halbzeit += 1;
                wartezeit.Dispose();
            };
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

    }
}
