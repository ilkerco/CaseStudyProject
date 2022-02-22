using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.Infrastructure
{
    public static class TimerService
    {
        public static int Time = 0;



        public static void AddTime(int time)
        {
            Time += time;
            Time %= 24;
        }
        public static int GetTime()
        {
            return Time;
        }
        public static string GetTimeStr()
        {
            if (Time < 10)
                return $"0{Time}.00";

            return $"{Time}.00";
        }
    }
}
