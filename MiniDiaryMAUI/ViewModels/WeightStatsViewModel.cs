using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MiniDiaryMAUI.Services;
using SkiaSharp;

namespace MiniDiaryMAUI.ViewModels;

public partial class WeightStatsViewModel : ObservableObject
{
    private readonly IEntriesService _entriesService;

    [ObservableProperty]
    public partial int SelectedRangeDays { get; set; } = 30;

    [ObservableProperty]
    public partial ISeries[] Series { get; set; } = [];

    [ObservableProperty]
    public partial ICartesianAxis[] XAxes { get; set; } = [];

    [ObservableProperty]
    public partial string MinWeightText { get; set; } = "–";

    [ObservableProperty]
    public partial string MaxWeightText { get; set; } = "–";

    [ObservableProperty]
    public partial string AverageWeightText { get; set; } = "–";

    [ObservableProperty]
    public partial bool IsRange7Selected { get; set; }

    [ObservableProperty]
    public partial bool IsRange30Selected { get; set; } = true;

    public WeightStatsViewModel(IEntriesService entriesService)
    {
        _entriesService = entriesService;
    }

    [RelayCommand]
    private async Task SelectRange(string daysParam)
    {
        var days = int.Parse(daysParam);
        if (days == SelectedRangeDays)
            return;

        SelectedRangeDays = days;
        IsRange7Selected = days == 7;
        IsRange30Selected = days == 30;

        await LoadAsync();
    }

    public async Task LoadAsync()
    {
        var weights = await _entriesService.GetWeightsSinceAsync(SelectedRangeDays);

        if (weights.Count == 0)
        {
            Series = [];
            XAxes = [];
            MinWeightText = MaxWeightText = AverageWeightText = "–";
            return;
        }

        Series =
        [
            new LineSeries<double>
        {
            Values = weights.Select(w => w.Weight).ToArray(),
            Stroke = new SolidColorPaint(SKColor.Parse("#BA5A31")) { StrokeThickness = 3 },
            Fill = null,
            GeometryStroke = new SolidColorPaint(SKColor.Parse("#BA5A31")) { StrokeThickness = 2 },
            GeometryFill = new SolidColorPaint(SKColor.Parse("#F0BE9D")),
            GeometrySize = 8,
            LineSmoothness = 0.3
        }
        ];

        XAxes =
        [
            new Axis
        {
            Labels = weights.Select(w => w.DateTime.ToString("dd.MM")).ToArray(),
            LabelsRotation = 0,
            TextSize = 11,
            LabelsPaint = new SolidColorPaint(SKColor.Parse("#0C0C0C"))
        }
        ];

        var values = weights.Select(w => w.Weight).ToList();
        MinWeightText = $"{values.Min():0.0} kg";
        MaxWeightText = $"{values.Max():0.0} kg";
        AverageWeightText = $"{values.Average():0.0} kg";
    }
}