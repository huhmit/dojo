using Dojo.ViewModels;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;



namespace Dojo.Views.UserControls
{
    /// <summary>
    /// Interaction logic for TextArea.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public Editor()
        {
            DataContext = new MainWindowViewModel();
            InitializeComponent();
            Viewer.Background = Brushes.White;
        }
        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }
    }
}
