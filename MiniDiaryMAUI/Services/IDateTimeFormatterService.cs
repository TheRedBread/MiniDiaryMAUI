namespace MiniDiaryMAUI.Services;

public interface IDateTimeFormatterService
{
    /// <summary>
    /// Formats a DateTime as a relative label ("Dzisiaj", "Wczoraj",
    /// or a date) followed by the time, e.g. "Wczoraj, 14:32".
    /// </summary>
    string FormatRelative(DateTime dateTime);
}