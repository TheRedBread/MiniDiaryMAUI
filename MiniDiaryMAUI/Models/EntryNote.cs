using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MiniDiaryMAUI.Models;

public class EntryNote : BaseEntry
{
    [NotNull, SQLite.MaxLength(500)]
    public string? Text { get; set; }
}
