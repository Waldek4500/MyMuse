namespace MyMuse.Maui.Helpers;

public static class TimeHelper
{
    /// <summary>
    /// Changes TimeSpan to string with specified format
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string FormatTime(TimeSpan time) =>
        time.Hours > 0
            ? time.ToString(@"hh\:mm\:ss")
            : time.ToString(@"mm\:ss");
}
