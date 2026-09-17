using System;
using System.Collections.Generic;
using System.Text;

namespace MiniDiaryMAUI.Models;

public abstract class BaseEntry
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
}
