using System.Windows;
using System.Windows.Input;

namespace Dojo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                Point point = PointToScreen(e.MouseDevice.GetPosition(this));

                Left = point.X <= RestoreBounds.Width / 2
                    ? 0
                    : point.X >= RestoreBounds.Width
                    ? point.X - (RestoreBounds.Width - (ActualWidth - point.X))
                    : point.X - (RestoreBounds.Width / 2);

                Top = point.Y - (((FrameworkElement)sender).ActualHeight / 2);
                WindowState = WindowState.Normal;
            }
            DragMove();
        }
    }
}