using System.Windows;

namespace Bickern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            //config file where all folders ip's and hostnames are stored.
            //on start - loop through config to start all sites.
        }
    }
}