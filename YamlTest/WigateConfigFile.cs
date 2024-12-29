using System.Collections.Generic;

namespace YamlTest
{
    public class WigateConfigFile
    {
        public WigateConfig WigateConfig { get; set; } = new();
    }

    public class WigateConfig
    {
        public int Id { get; set; }
        public string SiteName { get; set; } = string.Empty;
        public string TimezoneName { get; set; } = string.Empty;
        public long TimezoneOffset { get; set; } = 0;
        public string TemperatureUnits { get; set; } = string.Empty;
        public string PressureUnits { get; set; } = string.Empty;
        public string Relay1Name { get; set; } = string.Empty;
        public string Relay2Name { get; set; } = string.Empty;
        public string WifiSsid { get; set; } = string.Empty;
        public string WifiPassword { get; set; } = string.Empty;

        public List<GateSchedule> GateSchedules { get; set; } = new();
    }
}
