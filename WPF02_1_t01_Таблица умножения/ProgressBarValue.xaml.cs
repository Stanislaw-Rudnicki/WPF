using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF02_1_t01
{
    /// <summary>
    /// Interaction logic for ProgressBarValue.xaml
    /// </summary>
    public partial class ProgressBarValue : UserControl
    {
        public ProgressBarValue()
        {
            InitializeComponent();
            DataContext = this;
        }
        public double Value1
        {
            get { return (double)GetValue(Value1Property); }
            set { SetValue(Value1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Value1Property =
            DependencyProperty.Register("Value1", typeof(double), typeof(ProgressBarValue), new FrameworkPropertyMetadata(ChangeVal1));

        private static void ChangeVal1(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var mypr = (ProgressBarValue)d;
            mypr.prBar.Value = (double)e.NewValue;
        }

        public int Value2
        {
            get { return (int)GetValue(Value2Property); }
            set { SetValue(Value2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Value2Property =
            DependencyProperty.Register("Value2", typeof(int), typeof(ProgressBarValue), new FrameworkPropertyMetadata(0));

    }
}
