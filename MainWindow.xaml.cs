using Dojo.ViewElements;
using System.Windows;
using System.Windows.Input;

namespace Dojo
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
                var point = PointToScreen(e.MouseDevice.GetPosition(this));

                if (point.X <= RestoreBounds.Width / 2)
                    Left = 0;

                else if (point.X >= RestoreBounds.Width)
                    Left = point.X - (RestoreBounds.Width - (ActualWidth - point.X));

                else
                    Left = point.X - (RestoreBounds.Width / 2);

                Top = point.Y - (((FrameworkElement)sender).ActualHeight / 2);
                WindowState = WindowState.Normal;
            }
            DragMove();
        }
    }
}