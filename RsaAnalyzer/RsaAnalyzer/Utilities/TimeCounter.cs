using System;

namespace RsaAnalyzer.Utilities
{
    internal class TimeCounter
    {
        public static TimeSpan CalculateTimeSpan(TimeSpan fromTimeSpan, 
            TimeSpan toTimeSpan)
        {
            return toTimeSpan.Subtract(fromTimeSpan);
        }

        public static long RepresentAsTicks(TimeSpan timeSpanValue)
        {
            return timeSpanValue.Ticks;
        }

        public static TimeSpan RepresentAsTimeSpan(long ticks)
        {
            return TimeSpan.FromTicks(ticks);
        }
    }
}
