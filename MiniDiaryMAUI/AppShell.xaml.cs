using MiniDiaryMAUI.Views;

namespace MiniDiaryMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("list", typeof(ListPage));

        }
    }
}
