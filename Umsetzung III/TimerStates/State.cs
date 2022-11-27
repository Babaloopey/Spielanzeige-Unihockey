using Umsetzung_III.Stores;

namespace Umsetzung_III.TimerStates
{
    public abstract class State
    {
        protected int second;
        protected int minute;
        protected int durationMinute;
        protected int durationSecond;
        private int _timerIterations = 0;

        protected SpielzeitStore spielzeitStore;

        public State(SpielzeitStore spielzeitStore)
        {
            this.spielzeitStore = spielzeitStore;
        }

        public void TimeElapsed()
        {
            _timerIterations++;

            if (_timerIterations >= 10)
            {
                _timerIterations = 0;
                CountOneSecond();
            }

            

        }
        private void CountOneSecond()
        {
            if (minute == 0 && second == 0)
            {
                TimeRanOut();
            }
            else if (second == 0)
            {
                MinuteMinusOne();
                second = 59;
            }
            else
            {
                second--;
            }
        }
        public abstract void StartPause();
        public abstract void StartTimeOut();
        public abstract void TimeRanOut();
        public void Reset()
        {
            minute = durationMinute;
            second = durationSecond;
            _timerIterations = 0;
            CustomReset();
        }
        protected abstract void CustomReset();
        public void MinutePlusOne()
        {
            minute++;
        }

        public void MinuteMinusOne()
        {
            if (minute > 0)
            {
                minute--;
            }
        }


        public int GetMinute()
        {
            return minute;
        }
        public int GetSecond()
        {
            return second;
        }
    }
}
