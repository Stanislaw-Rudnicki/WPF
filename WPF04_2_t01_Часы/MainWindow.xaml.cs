using System;
using System.Windows;

namespace WPF04_2_t01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(500);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Time = DateTime.Now;
        }

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }
        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(DateTime.Now));

    }
}
