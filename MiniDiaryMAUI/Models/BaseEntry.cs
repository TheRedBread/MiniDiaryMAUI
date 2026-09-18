using SQLite;

namespace MiniDiaryMAUI.Models;

public abstract class BaseEntry
{
    [PrimaryKey, AutoIncrement, NotNull]
    public int Id { get; set; }
    [NotNull]
    public DateTime DateTime { get; set; }
}
