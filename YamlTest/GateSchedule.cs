using Meadow;
using System;
using System.Text;

namespace YamlTest
{
    public class GateSchedule
    {
        public int Id { get; set; }
        public int ScheduleType { get; set; }
        public int ActionDays { get; set; }
        public int ActionHour { get; set; }
        public int ActionMinute { get; set; }
        public bool TimeIsUtc { get; set; } = false;
        public ScheduleActionType ActionType { get; set; }
        public int ActionRelay { get; set; }
        public ScheduleFrequencyType FrequencyType { get; set; }

        public GateSchedule() { }

        /// <summary>
        /// Returns true if the Data Log Schedule instance
        /// ActionDays is valid for the current day of the week
        /// </summary>
        public bool IsValidToday
        {
            get
            {
                var config = (WigateConfig)Resolver.Services.Get<WigateConfig>();
                if (config == null) return false;

                var now = DateTime.UtcNow.AddMinutes(config.TimezoneOffset);
                var today = 0x01 << (int)now.DayOfWeek;
                return (ActionDays & today) != 0;
            }
        }

        public override string ToString()
        {
            string days = ((ActionDays & 0x01) == 0x01) ? "S" : "-";
            days += ((ActionDays & 0x02) == 0x02) ? "M" : "-";
            days += ((ActionDays & 0x04) == 0x04) ? "T" : "-";
            days += ((ActionDays & 0x08) == 0x08) ? "W" : "-";
            days += ((ActionDays & 0x10) == 0x10) ? "T" : "-";
            days += ((ActionDays & 0x20) == 0x20) ? "F" : "-";
            days += ((ActionDays & 0x40) == 0x40) ? "S" : "-";

            string time = $"Occurs at {ActionHour:D2}:{ActionMinute:D2}";


            var sb = new StringBuilder()
                .Append($"Id: {Id}, ")
                .Append($"Days: {days}, ")
                .Append($"When: {time}, ")
                .Append($"TimeIsUtc: {TimeIsUtc},")
                .Append($"Action: {ActionType}, ")
                .Append($"Frequency: {FrequencyType}");

            return sb.ToString();
        }
    }
}
