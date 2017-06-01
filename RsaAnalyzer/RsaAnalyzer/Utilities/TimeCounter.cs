using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsaAnalyzer.Utilities
{
    class TimeCounter
    {
        public TimeSpan CalculateTimeSpan(TimeSpan fromTimeSpan, TimeSpan toTimeSpan)
        {
            return toTimeSpan.Subtract(fromTimeSpan);
        }

        public long RepresentAsTicks(TimeSpan timeSpanValue)
        {
            return timeSpanValue.Ticks;
        }
    }
}
