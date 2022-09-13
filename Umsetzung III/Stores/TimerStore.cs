using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Media;

namespace Umsetzung_III
{
    public class TimerStore
    {
        private readonly Timer _timer;

        public bool ButtonVisibilityStart;

        public event Action OnButtonVisibilityChanged;
        public event Action OnTimerElapsed;
        public TimerStore()
        {
            _timer = new Timer(100);
            _timer.Elapsed += Timer_Elapsed;

            ButtonVisibilityStart = true;
        }
        public void Start()
        {
            _timer.Start();

            ButtonVisibilityStart = false;
            ButtonVisibilityChanged();
        }
        public void Stop()
        {
            _timer.Stop();

            ButtonVisibilityStart = true;
            ButtonVisibilityChanged();
        }
        public void Reset()
        {
            _timer.Stop();

            ButtonVisibilityStart = true;
            ButtonVisibilityChanged();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            TimerElapsed();
        }

        private void SpielzeitAbgelaufen()
        {
            Stop();
        }

        private void ButtonVisibilityChanged()
        {
            OnButtonVisibilityChanged?.Invoke();
        }

        private void TimerElapsed()
        {
            OnTimerElapsed?.Invoke();
        }
    }
}
