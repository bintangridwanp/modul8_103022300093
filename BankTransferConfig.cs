using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace modul8_103022300093
{
    public class TransferConfig
    {
        [JsonPropertyName("threshold")]
        public string Threshold { get; set; }
        [JsonPropertyName("low_fee")]
        public string LowFee { get; set; }
        [JsonPropertyName("high_fee")]
        public string HighFee { get; set; }
    }

    public class BankTransferConfig
    {
        [JsonPropertyName("lang")]
        public string Lang { get; set; }
        [JsonPropertyName("transfer")]
        public TransferConfig Transfer { get; set; }
        [JsonPropertyName("methods")]
        public List<string> Methods { get; set; }
        [JsonPropertyName("confirmation")]
        public Dictionary<string, string> Confirmation { get; set; }
        public static BankTransferConfig ReadConfigFile(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                BankTransferConfig config = JsonSerializer.Deserialize<BankTransferConfig>(jsonString);
                return config ?? CreateDefaultConfig(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading config file: {ex.Message}");
                return CreateDefaultConfig(filePath);
            }
        }
        public static BankTransferConfig CreateDefaultConfig(string filePath)
        {
            var config = new BankTransferConfig();
            config.Lang = "en";
            config.Transfer = new TransferConfig
            {
                Threshold = "25000000",
                LowFee = "6500",
                HighFee = "15000"
            };
            config.Methods = new List<string> { "RTO", "(real-time)", "SKN", "RTGS", "BI", "FAST" };
            config.Confirmation = new Dictionary<string, string>
            {
                { "en", "yes" },
                { "id", "ya" }
            };
            config.SaveConfigFile(filePath);
            return config;
        }
        public void SaveConfigFile(string filePath)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving config file: {ex.Message}");
            }
        }
    }
}