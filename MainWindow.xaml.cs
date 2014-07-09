using System.Windows;
using CCSubscriptionViewer.Auth;

namespace CCSubscriptionViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public AuthResult Authorization;

        public MainWindow()
        {

            InitializeComponent();
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var authManager = new AuthManager();
            var authResult = await authManager.Authorize();

            if (!authResult)
                Close();
            else
                Authorization = authResult;
        }
    }
}
