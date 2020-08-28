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

namespace WPF01_2_t01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string[] arr = ("Navy, Blue, Aqua, Teal, Olive, Green, Lime, Yellow, Orange, " +
                "Red, Maroon, Fuchsia, Purple, Black, Silver, Gray, White")
                .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in arr)
            {
                var button = new Button { Content = item };
                button.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString(item);
                button.Margin = new Thickness(2);
                wrapPanel1.Children.Add(button);
            }
        }
    }
}
