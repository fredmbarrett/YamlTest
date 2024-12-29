using System.Runtime.Serialization;

namespace YamlTest
{
    public enum ScheduleActionType
    {
        PulseRelay = 0x01,
        EngageRelay = 0x02,
        DisengageRelay = 0x03
    }

    /// <summary>
    /// The frequency of a schedule determines how often the event occurs.
    /// Options are once daily at a specific time, or at hourly, minute, or second intervals.
    /// </summary>
    public enum ScheduleFrequencyType
    {
        /// <summary>
        /// Event occurs every nn hours
        /// </summary>
        [EnumMember(Value = "Hours")]
        Hours = 1,
        /// <summary>
        /// Event occurs every nn minutes
        /// </summary>
        [EnumMember(Value = "Minutes")]
        Minutes = 2,
        /// <summary>
        /// Event occurs every nn seconds
        /// </summary>
        [EnumMember(Value = "Seconds")]
        Seconds = 3,
        /// <summary>
        /// Event occurs daily at a specific time
        /// </summary>
        [EnumMember(Value = "Daily")]
        Daily = 4,

        [EnumMember(Value = "Temperature")]
        Temperature = 5,
    }
}
