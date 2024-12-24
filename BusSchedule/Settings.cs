using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusSchedule;
public static class Settings
{
    private static readonly string settingsFilePath = "settings.json";

    static Settings()
    {
        LoadSettings();
    }

    public static TimeSpan StartTime { get; set; } = new TimeSpan(17, 0, 0);
    public static TimeSpan EndTime { get; set; } = new TimeSpan(19, 0, 0);
    public static bool ShowNotification { get; set; } = true;

    public static void SaveSettings()
    {
        var settings = new
        {
            StartTime = StartTime.ToString(),
            EndTime = EndTime.ToString(),
            ShowNotification = ShowNotification.ToString()
        };

        var json = JsonSerializer.Serialize(settings);
        File.WriteAllText(settingsFilePath, json);
    }

    public static void LoadSettings()
    {
        if (File.Exists(settingsFilePath))
        {
            var json = File.ReadAllText(settingsFilePath);
            var settings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            if (settings != null)
            {
                StartTime = TimeSpan.Parse(settings["StartTime"]);
                EndTime = TimeSpan.Parse(settings["EndTime"]);
                ShowNotification = bool.Parse(settings["ShowNotification"]);
            }
        }
    }
}
