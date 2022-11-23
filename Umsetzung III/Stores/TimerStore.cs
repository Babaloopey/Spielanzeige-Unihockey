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

        public bool IsStartButtonVisible;

        public event Action OnButtonVisibilityChanged;
        public event Action OnTimerElapsed;
        public TimerStore()
        {
            _timer = new Timer(100);
            _timer.Elapsed += Timer_Elapsed;

            IsStartButtonVisible = true;
        }
        public void Start()
        {
            _timer.Start();

            IsStartButtonVisible = false;
            ButtonVisibilityChanged();
        }
        public void Stop()
        {
            _timer.Stop();

            IsStartButtonVisible = true;
            ButtonVisibilityChanged();
        }
        public void Reset()
        {
            _timer.Stop();

            IsStartButtonVisible = true;
            ButtonVisibilityChanged();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnTimerElapsed?.Invoke();
        }

        private void ButtonVisibilityChanged()
        {
            OnButtonVisibilityChanged?.Invoke();
        }
    }
}
