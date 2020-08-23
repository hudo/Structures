using System;
using System.Text.Json.Serialization;

namespace Structures.Time
{
    /// <summary>
    /// Time represents point in time of a day
    /// </summary>
    [JsonConverter(typeof(TimeJsonConverter))]
    public struct Time : IComparable, IComparable<Time>, IEquatable<Time>
    {
        private int _hour;
        private int _minute;
        private int _seconds;

        public Time(int hour = 0, int minute = 0, int seconds = 0)
        {
            _hour = Validate(hour, 24, nameof(Hour));
            _minute = Validate(minute, 60, nameof(Minute));
            _seconds = Validate(seconds, 60, nameof(Seconds));
        }

        /// <summary>
        /// Sets hours, 0 to 24
        /// </summary>
        public int Hour 
        { 
            get => _hour;
            set => _hour = Validate(value, 24, nameof(Hour));
        }

        /// <summary>
        /// Sets minutes, 0 to 60
        /// </summary>
        public int Minute 
        { 
            get => _minute;
            set => _minute = Validate(value, 60, nameof(Minute));
        }

        /// <summary>
        /// Sets seconds, 0 to 60
        /// </summary>
        public int Seconds 
        { 
            get => _seconds;
            set => _seconds = Validate(value, 60, nameof(Seconds));
        }

        private static int Validate(int value, int maxValue, string fieldName) =>
            value >= 0 && value <= maxValue ? value : throw new ArgumentOutOfRangeException($"{fieldName} out of range");


        public static implicit operator Time(int hour) => new Time(hour, 0);

        public static implicit operator Time(double time)
        {
            var span = TimeSpan.FromHours(time);
            return new Time(span.Hours, span.Minutes, span.Seconds);
        }

        public static implicit operator Time(DateTime time) => new Time(time.Hour, time.Minute, time.Second);

        /// <summary>
        /// Format hours:minutes:seconds
        /// </summary>
        /// <param name="time">Hour:Minute</param>
        public static implicit operator Time(string time)
        {
            if (string.IsNullOrWhiteSpace(time)) return new Time();

            var parts = time.Split(':');

            return new Time(
                Convert.ToInt32(parts[0]),
                parts.Length > 1 ? Convert.ToInt32(parts[1]) : 0,
                parts.Length > 2 ? Convert.ToInt32(parts[2]) : 0);
        }

        public static bool operator >(Time first, Time second) =>
            first.Hour > second.Hour || first.Hour == second.Hour &&
            (first.Minute > second.Minute || first.Minute == second.Minute && first.Seconds > second.Seconds);

        public static bool operator <(Time first, Time second) => !(first > second);
        public static bool operator ==(Time first, Time second) => first.Hour == second.Hour && first.Minute == second.Minute && first.Seconds == second.Seconds;
        public static bool operator !=(Time first, Time second) => !(first == second);
        public static bool operator >=(Time first, Time second) => first == second || first > second;
        public static bool operator <=(Time first, Time second) => first == second || first < second;

        public int TotalSeconds { get => Seconds + Minute * 60 + Hour * 60 * 60; }
        public double TotalMinutes { get => Hour * 60d + Minute + Seconds / 60d; }
        public double TotalHours { get => Hour + Minute / 60d + Seconds / 60d / 60d; }

        public override string ToString() => $"{Hour}:{Minute}:{Seconds}";

        public void Deconstruct(out int hour, out int minute, out int seconds)
        {
            hour = Hour;
            minute = Minute;
            seconds = Seconds;
        }

        public override bool Equals(object obj) => obj is Time time && TotalSeconds == time.TotalSeconds;
        public override int GetHashCode() => -321205630 + TotalSeconds.GetHashCode();

        public int CompareTo(object value)
        {
            if (value == null) return 1;
            if (!(value is Time)) throw new ArgumentException("Value must be Time");

            var time = (Time)value;

            if (this > time) return 1;
            if (this < time) return -1;
            return 0;
        }

        public int CompareTo(Time time)
        {
            if (this > time) return 1;
            if (this < time) return -1;
            return 0;
        }

        public bool Equals(Time other)
        {
            return other == this;
        }
    }
}