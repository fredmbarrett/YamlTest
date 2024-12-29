using Meadow;
using Meadow.Logging;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace YamlTest
{
    public static class ConfigFileManager
    {
        private static Logger Log { get; } = Resolver.Log;

        const string WIGATE_CONFIG_FILENAME = "test.config.yaml";

        #region Envizor Config File

        public static WigateConfigFile CreateWigateConfigFile(int deviceId)
        {
            try
            {
                var configFile = new WigateConfigFile();
                var config = new WigateConfig
                {
                    Id = deviceId,
                    SiteName = "WiGate Test",
                    TimezoneName = "US/Pacific",
                    TimezoneOffset = -28800,
                    TemperatureUnits = "Farenheit",
                    PressureUnits = "Mercury",
                    Relay1Name = "Relay 1",
                    Relay2Name = "Relay 2",
                    WifiSsid = "AllynTech-IoT",
                    WifiPassword = "allyntech5510"
                };
                config.GateSchedules.Add(new GateSchedule
                {
                    Id = 1,
                    ScheduleType = 0,
                    ActionDays = 0x7F,
                    ActionHour = 0,
                    ActionMinute = 1,
                    TimeIsUtc = true,
                    ActionType = ScheduleActionType.PulseRelay,
                    ActionRelay = 1,
                    FrequencyType = ScheduleFrequencyType.Minutes
                });
                config.GateSchedules.Add(new GateSchedule
                {
                    Id = 2,
                    ScheduleType = 0,
                    ActionDays = 0x7F,
                    ActionHour = 0,
                    ActionMinute = 3,
                    TimeIsUtc = true,
                    ActionType = ScheduleActionType.PulseRelay,
                    ActionRelay = 2,
                    FrequencyType = ScheduleFrequencyType.Minutes
                });
                configFile.WigateConfig = config;

                WriteConfigFile(configFile, WIGATE_CONFIG_FILENAME);
                return configFile;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error creating Wigate config file: {ex.Message}");
                throw;
            }
        }

        public static WigateConfigFile ReadWigateConfigFile()
        {
            try
            {
                return ReadConfigFile<WigateConfigFile>(WIGATE_CONFIG_FILENAME);
            }
            catch (FileNotFoundException)
            {
                Log.Warn($"Wigate config file not found, creating default...");
                return CreateWigateConfigFile(0);
            }
            catch (Exception ex)
            {
                Log.Warn($"Error reading Wigate config file: {ex.Message}");
                return null;
            }
        }

        public static WigateConfigFile UpdateWigateConfigFile(WigateConfigFile config)
        {
            try
            {
                WriteConfigFile(config, WIGATE_CONFIG_FILENAME);
                return config;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error updating Wigate config file: {ex.Message}");
                throw;
            }
        }

        #endregion Envizor Config File

        #region Private Methods

        private static void WriteConfigFile(object config, string filename)
        {
            var serializer = new SerializerBuilder().Build();
            var yaml = serializer.Serialize(config);

            var configPath = Path.Combine(MeadowOS.FileSystem.UserFileSystemRoot, filename);
            using var fs = File.CreateText(configPath);
            fs.WriteLine(yaml);
        }

        private static T ReadConfigFile<T>(string filename) where T : class
        {
            var configPath = Path.Combine(MeadowOS.FileSystem.UserFileSystemRoot, filename);

            if (!File.Exists(configPath))
                throw new FileNotFoundException($"Config file {filename} not found.");

            var deserializer = new DeserializerBuilder().Build();
            var yaml = File.ReadAllText(configPath);

            return deserializer.Deserialize<T>(yaml);
        }

        private static void DeleteConfigFile(string filename)
        {
            var configPath = Path.Combine(MeadowOS.FileSystem.UserFileSystemRoot, filename);

            if (File.Exists(configPath))
                File.Delete(configPath);
            else
                Log.Info($"{filename} not found.");
        }

        #endregion Private Methods
    }
}
