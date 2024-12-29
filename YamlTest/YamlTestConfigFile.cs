using System.Collections.Generic;

namespace YamlTest
{
    public class YamlTestConfigFile
    {
        public YamlTestConfig YamlTestConfig { get; set; } = new YamlTestConfig();

        public YamlTestConfigFile() { }

        public YamlTestConfigFile(int deviceId)
        {
            YamlTestConfig.DeviceId = deviceId;
        }
    }

    public class YamlTestConfig
    {
        public int DeviceId { get; set; } = 0;
        public List<SensorCalibration> SensorCalibrations { get; set; } = [];

        public YamlTestConfig()
        {
            SensorCalibrations.Add(new SensorCalibration("PPB", SupportedSensorType.IONSCI_MINIPID2_VOC, 0, 1));
            SensorCalibrations.Add(new SensorCalibration("PPB_WR", SupportedSensorType.IONSCI_MINIPID2_VOC, 0, 1));
            SensorCalibrations.Add(new SensorCalibration("PPM", SupportedSensorType.IONSCI_MINIPID2_VOC, 0, 1));
            SensorCalibrations.Add(new SensorCalibration("PPM_WR", SupportedSensorType.IONSCI_MINIPID2_VOC, 0, 1));
            SensorCalibrations.Add(new SensorCalibration("P9300", SupportedSensorType.PARTICLES_PLUS_9300, 1, 3, 2.5F));
            SensorCalibrations.Add(new SensorCalibration("PLAB", SupportedSensorType.ENVIZOR_BME688, 1, 3, 2.5F));
            SensorCalibrations.Add(new SensorCalibration("WindSonic60", SupportedSensorType.GILL_WINDSONIC_60, 1, 3, 40.6F));
        }

        public YamlTestConfig(int deviceId) : this()
        {
            DeviceId = deviceId;
        }
    }

    public class SensorCalibration
    {
        public string SensorTypeName { get; set; } = "";
        public int SensorTypeCode { get; set; } = 0;
        public int SampleIntervalSeconds { get; set; } = 1;
        public int StartupDelaySeconds { get; set; } = 0;
        public float CalibrationFactor { get; set; } = 0.0F;
        public float ReferenceHigh { get; set; } = 10.0F;
        public float ReferenceLow { get; set; } = 0.0F;
        public float RawHigh { get; set; } = 10.0F;
        public float RawLow { get; set; } = 0.0F;

        public SensorCalibration() { }
        public SensorCalibration(
            string name,
            SupportedSensorType sensorType,
            int startupDelay = 0,
            int sampleSeconds = 1,
            float calFactor = 0.0F,
            float referenceHigh = 10.0F,
            float referenceLow = 0.0F,
            float rawHigh = 10.0F,
            float rawLow = 0.0F)
        {
            SensorTypeName = name;
            SensorTypeCode = (int)sensorType;
            SampleIntervalSeconds = sampleSeconds;
            StartupDelaySeconds = startupDelay;
            CalibrationFactor = calFactor;
            ReferenceHigh = referenceHigh;
            ReferenceLow = referenceLow;
            RawHigh = rawHigh;
            RawLow = rawLow;
        }
    }

    public enum SupportedSensorType
    {
        ENVIZOR_ONBOARD = 0x00,
        ENVIZOR_WIND = 0x31,
        ENVIZOR_RAIN = 0x32,
        ENVIZOR_BME688 = 0x33,
        IONSCI_MINIPID2_VOC = 0x61,
        PARTICLES_PLUS_9300 = 0x71,
        GILL_WINDSONIC_60 = 0x81,
    }
}
