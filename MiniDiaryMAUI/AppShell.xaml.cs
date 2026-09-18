using MiniDiaryMAUI.Views;

namespace MiniDiaryMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ListPage), typeof(ListPage));
            Routing.RegisterRoute(nameof(WeightStatsPage), typeof(WeightStatsPage));


        }
    }
}
