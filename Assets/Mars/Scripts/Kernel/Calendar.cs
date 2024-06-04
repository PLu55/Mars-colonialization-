using System;
using UnityEngine;
 
namespace PLu.Mars.Kernel
{
    public class Calendar : MonoBehaviour
    {
        // 687 days = 1 year, a day is called sol
        // 24 months = 1 year, 668.6 days a year, 28 sols a month
        // Week is 7 sols
        // day is 24 hours, 39 minutes, and 35.244 seconds or 88775,244 seconds
        // Months: Phobos, Deimos, Ares, Boreas, Tharsis, Elysium, Utopia, Arcadia, Hellas, Isidis, Cimmeria, Amazonis, etc.
        // Days: Solisday, Tharsday, Deimosday, Areoday, Phobosday, Olympday, Valday (or other thematic names).
        
        // Expecting non negative time, no leap years
        // TODO: implement leap days, 
        public const double DayLength = 86400.0; // Earth day, use this for calculations as it simplifies alot to have 24 hour days
        public const double MarsDayLength = 88775.244;
        public const double  MarsSecond = 1.02749125;
        public const double  DaysPerYear = 668.6;
        public const int  DaysPerMonth = 28;
        public const int MonthPerYear = 24;
        public const int WeekLength = 7;

        public static int DaysInMonth(int month) => (month - 1) % 6 == 5 ? 27 : 28;
        // public static double GlobalTime => Time.time + TimeOffset;
        public static float TimeOfDay (double time) => (float)(time % DayLength);
        public static float TimeOfDayNormalized (double globalTime) => TimeOfDay(globalTime) / (float)DayLength;
        // Counting from 0
        public static int DayNumber(double globalTime) => (int)(Math.Floor(globalTime / DayLength) % (int)DaysPerYear);
        //public static int DayNumber(double globalTime) => (int)Math.Floor(globalTime / DayLength % DaysPerYear);
        public static int Month (double  globalTime) => DayNumber(globalTime) / DaysPerMonth % MonthPerYear + 1;
        // TODO: fix Day 
        public static int Day (double  globalTime)=> DayNumber(globalTime) % DaysPerMonth + 1;
        public static int Hour (double  globalTime)=> (int)Math.Floor(globalTime % DayLength / 3600f);
        public static int Minutes(double  globalTime) => (int)Math.Floor(globalTime % 3600f / 60f);
        public static double  Seconds(double  globalTime) => globalTime % 60f;
        public static int Year(double  globalTime) => (int)Math.Floor(DayNumber(globalTime) / DaysPerYear);

    }
}
