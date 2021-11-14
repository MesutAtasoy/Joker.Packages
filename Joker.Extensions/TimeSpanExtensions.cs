namespace Joker.Extensions;

public static class TimeSpanExtensions
{
    public static string Format(this TimeSpan time)
    {
        var days = time.Days > 0 ? $"{time.Days}d" : string.Empty;
        var hours = time.Hours > 0 ? $"{time.Hours}h" : string.Empty;
        var minutes = time.Minutes > 0 ? $"{time.Minutes}m" : string.Empty;
        var seconds = time.Seconds > 0 ? $"{time.Seconds}s" : string.Empty;

        return $"{days} {hours} {minutes} {seconds}".Trim();
    }

    public static TimeSpan NextTime(this TimeSpan time, params TimeSpan[] times)
    {
        return times.Where(x => x.Hours > time.Hours).OrderBy(x => x.Hours).FirstOrDefault();
    }
}