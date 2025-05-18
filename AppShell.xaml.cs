using Avatab.View;

namespace Avatab
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(Routes.PersonEditPage, typeof(PersonEditPage));
        }
    }
}
