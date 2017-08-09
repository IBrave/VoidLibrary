using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoidLibrary.Utils.Concurrent
{
    public class TimeoutUtil
    {
        private bool hasDeadline;
        private DateTime deadlineDateTime;

        public TimeoutUtil()
        {
        }

        private TimeoutUtil DeadlineDateTime(DateTime deadlineDateTime)
        {
            hasDeadline = true;
            this.deadlineDateTime = deadlineDateTime;
            return this;
        }

        public TimeoutUtil Deadline(long durationMilliseconds)
        {
            return DeadlineDateTime(DateTime.Now.AddMilliseconds(durationMilliseconds));
        }

        public TimeoutUtil Deadline(DateTime dateTime, long durationMilliseconds)
        {
            return DeadlineDateTime(dateTime.AddMilliseconds(durationMilliseconds));
        }

        public bool HasDeadline()
        {
            return hasDeadline;
        }

        public TimeoutUtil ClearDeadline()
        {
            if (hasDeadline)
            {
                hasDeadline = false;
            }
            return this;
        }

        public long LeftMillisTimeToDeadline()
        {
            if (hasDeadline)
            {
                return (long)(deadlineDateTime - DateTime.Now).TotalMilliseconds;
            }
            return 0;
        }

        public bool IsReached()
        {
            return hasDeadline && deadlineDateTime <= DateTime.Now;
        }

    }
}
