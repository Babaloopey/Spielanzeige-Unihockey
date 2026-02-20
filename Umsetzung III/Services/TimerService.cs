using System.Collections.Generic;
using System.Timers;
using Umsetzung_III.Interface;

namespace Umsetzung_III.Services
{
    public class TimerService
    {
        private static TimerService timerService;

        private readonly Timer _timer;

        private List<TimeSubscriber> timeSubscribers = new List<TimeSubscriber>();

        public static TimerService GetTimerService()
        {
            if (timerService == null)
            {
                timerService = new TimerService();
            }

            return timerService;
        }

        private TimerService()
        {
            _timer = new Timer(100);
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }
        public void AddSubscriber(TimeSubscriber subscriber)
        {
            timeSubscribers.Add(subscriber);
        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (TimeSubscriber subscriber in timeSubscribers)
            {
                subscriber.TimeElapsed();
            }
        }

    }
}
