using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms;

namespace Bickern.Windows
{
    /// <summary>
    /// Interaction logic for AddSiteDialog.xaml
    /// </summary>
    public partial class AddSiteDialog : Window, INotifyPropertyChanged
    {
        private string path;

        private string url;

        public AddSiteDialog()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Path { get => path; set { path = value; RaisePropertyChanged("Path"); } }

        public string Url { get => url; set { url = value; RaisePropertyChanged("Url"); } }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void FindFolder_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Path = dialog.SelectedPath;
            }
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Url_TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Url = Url_TextBox.Text;
        }
    }
}