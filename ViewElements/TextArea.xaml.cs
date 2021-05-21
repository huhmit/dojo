using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;

namespace Dojo.ViewElements
{
    /// <summary>
    /// Interaction logic for TextArea.xaml
    /// </summary>
    public partial class TextArea : UserControl
    {
        public TextArea()
        {
            InitializeComponent();
            Viewer.Background = Brushes.White;
        }
        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }
    }
}
