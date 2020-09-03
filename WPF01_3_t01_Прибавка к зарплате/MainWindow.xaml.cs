using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace WPF01_3_t01
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

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;
        private const int WS_MAXIMIZEBOX = 0x10000;

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper((Window)sender).Handle;
            var value = GetWindowLong(hwnd, GWL_STYLE);
            SetWindowLong(hwnd, GWL_STYLE, (int)(value & ~WS_MAXIMIZEBOX));
        }

        double xYes, yYes;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MinWidth = textBlock1.ActualWidth + 70;
            MinHeight = 220;
            Canvas.SetLeft(buttonYes, canvas1.ActualWidth / 2 - 140);
            xYes = canvas1.ActualWidth / 2 - (double)buttonYes.GetValue(Canvas.LeftProperty);
            Canvas.SetTop(buttonYes, canvas1.ActualHeight / 2 - buttonYes.ActualHeight / 2);
            yYes = canvas1.ActualHeight / 2 - (double)buttonYes.GetValue(Canvas.TopProperty);
            Canvas.SetLeft(buttonNo, canvas1.ActualWidth / 2 + 40);
            Canvas.SetTop(buttonNo, canvas1.ActualHeight / 2 - buttonYes.ActualHeight / 2);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.SetLeft(buttonYes, canvas1.ActualWidth / 2 - xYes);
            Canvas.SetTop(buttonYes, canvas1.ActualHeight / 2 - yYes);

            if ((double)buttonYes.GetValue(Canvas.LeftProperty) < 10)
                Canvas.SetLeft(buttonYes, 10);
            if ((double)buttonYes.GetValue(Canvas.LeftProperty) > canvas1.ActualWidth - buttonYes.ActualWidth - 10)
                Canvas.SetLeft(buttonYes, canvas1.ActualWidth - buttonYes.ActualWidth - 10);
            if ((double)buttonYes.GetValue(Canvas.TopProperty) > canvas1.ActualHeight - buttonYes.ActualHeight - 10)
                Canvas.SetTop(buttonYes, canvas1.ActualHeight - buttonYes.ActualHeight - 10);
            if ((double)buttonYes.GetValue(Canvas.TopProperty) < 10)
                Canvas.SetTop(buttonYes, 10);

            Canvas.SetLeft(buttonNo, canvas1.ActualWidth / 2 + 40);
            Canvas.SetTop(buttonNo, canvas1.ActualHeight / 2 - buttonYes.ActualHeight * 0.5);

            xYes = canvas1.ActualWidth / 2 - (double)buttonYes.GetValue(Canvas.LeftProperty);
            yYes = canvas1.ActualHeight / 2 - (double)buttonYes.GetValue(Canvas.TopProperty);
        }

        private void buttonYes_MouseEnter(object sender, MouseEventArgs e)
        {
            Random rnd = new Random();
            double x = (double)buttonNo.GetValue(Canvas.LeftProperty);
            double y = (double)buttonNo.GetValue(Canvas.TopProperty);
            while ((x > (double)buttonNo.GetValue(Canvas.LeftProperty) - buttonNo.ActualWidth &&
                    x < (double)buttonNo.GetValue(Canvas.LeftProperty) + buttonNo.ActualWidth) &&
                   (y > (double)buttonNo.GetValue(Canvas.TopProperty) - buttonNo.ActualHeight &&
                    y < (double)buttonNo.GetValue(Canvas.TopProperty) + buttonNo.ActualHeight))
            {
                x = rnd.Next(10, (int)canvas1.ActualWidth - 111);
                y = rnd.Next(10, (int)(canvas1.ActualHeight - buttonYes.ActualHeight - 10));
            }
            Canvas.SetLeft(buttonYes, x);
            Canvas.SetTop(buttonYes, y);
            xYes = canvas1.ActualWidth / 2 - (double)buttonYes.GetValue(Canvas.LeftProperty);
            yYes = canvas1.ActualHeight / 2 - (double)buttonYes.GetValue(Canvas.TopProperty);
        }

        private void buttonNo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дякую за економію!", "", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
