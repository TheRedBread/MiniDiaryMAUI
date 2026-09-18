namespace MiniDiaryMAUI.Services;

public class DateTimeFormatterService : IDateTimeFormatterService
{
    public string FormatRelative(DateTime dateTime)
    {
        var today = DateTime.Today;
        var date = dateTime.Date;
        var time = dateTime.ToString("HH:mm");

        if (date == today)
            return $"Dzisiaj, {time}";

        if (date == today.AddDays(-1))
            return $"Wczoraj, {time}";

        return $"{date:dd.MM.yyyy}, {time}";
    }
}