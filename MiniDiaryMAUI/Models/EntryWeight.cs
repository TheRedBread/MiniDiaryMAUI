using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MiniDiaryMAUI.Models;

public class EntryWeight : BaseEntry
{
    [NotNull, Range(20, 300)]
    public double Weight { get; set; }
}
