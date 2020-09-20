using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace WPF04_3_t01
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        public event EventHandler PropertyChanged;
        public event EventHandler ValueChanged;
        
        public NumericUpDown()
        {
            InitializeComponent();

            DependencyPropertyDescriptor.FromProperty(ValueProperty, typeof(NumericUpDown)).AddValueChanged(this, PropertyChanged);
            DependencyPropertyDescriptor.FromProperty(ValueProperty, typeof(NumericUpDown)).AddValueChanged(this, ValueChanged);
            DependencyPropertyDescriptor.FromProperty(DecimalsProperty, typeof(NumericUpDown)).AddValueChanged(this, PropertyChanged);
            DependencyPropertyDescriptor.FromProperty(MinValueProperty, typeof(NumericUpDown)).AddValueChanged(this, PropertyChanged);
            DependencyPropertyDescriptor.FromProperty(MaxValueProperty, typeof(NumericUpDown)).AddValueChanged(this, PropertyChanged);
            
            PropertyChanged += (x, y) => validate();
        }

        public decimal Value
        {
            get { return (decimal)GetValue(ValueProperty); }
            set
            {
                if (value < MinValue)
                    value = MinValue;
                if (value > MaxValue)
                    value = MaxValue;
                SetValue(ValueProperty, value);
            }
        }
        public readonly static DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(new decimal(0)));


        public decimal Step
        {
            get { return (decimal)GetValue(StepProperty); }
            set
            {
                SetValue(StepProperty, value);
            }
        }
        public readonly static DependencyProperty StepProperty = DependencyProperty.Register(
             "Step", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(new decimal(0.1)));


        public int Decimals
        {
            get { return (int)GetValue(DecimalsProperty); }
            set
            {
                SetValue(DecimalsProperty, value);
            }
        }
        public readonly static DependencyProperty DecimalsProperty = DependencyProperty.Register(
            "Decimals", typeof(int), typeof(NumericUpDown), new PropertyMetadata(2));


        public decimal MinValue
        {
            get { return (decimal)GetValue(MinValueProperty); }
            set
            {
                if (value > MaxValue)
                    MaxValue = value;
                SetValue(MinValueProperty, value);
            }
        }
        public readonly static DependencyProperty MinValueProperty = DependencyProperty.Register(
            "MinValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(decimal.MinValue));


        public decimal MaxValue
        {
            get { return (decimal)GetValue(MaxValueProperty); }
            set
            {
                if (value < MinValue)
                    value = MinValue;
                SetValue(MaxValueProperty, value);
            }
        }
        public readonly static DependencyProperty MaxValueProperty = DependencyProperty.Register(
            "MaxValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(decimal.MaxValue));


        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            Value += Step;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            Value -= Step;
        }

        private void validate()
        {
            // Logically, This is not needed at all... as it's handled within other properties...
            if (MinValue > MaxValue) MinValue = MaxValue;
            if (MaxValue < MinValue) MaxValue = MinValue;
            if (Value < MinValue) Value = MinValue;
            if (Value > MaxValue) Value = MaxValue;

            Value = decimal.Round(Value, Decimals);
        }
    }
}
