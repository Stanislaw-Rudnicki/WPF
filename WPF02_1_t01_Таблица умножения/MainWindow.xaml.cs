using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = this;
            ButtonStyles.Add(this.FindResource("Color0") as Style);
            ButtonStyles.Add(this.FindResource("Color1") as Style);
            ButtonStyles.Add(this.FindResource("Color2") as Style);
            ButtonStyles.Add(this.FindResource("Color3") as Style);
            ButtonStyles.Add(this.FindResource("Color4") as Style);

            StringStyle = this.FindResource("ColorBlue") as Style;

            NumberOfQuestions = 25;
            DialogString = "Обери складність і натисни «Старт»!";
            NewExercise();
        }

        public List<int> ItemSource { get; } = Enumerable.Range(1, 20).ToList();
        public int SelectedIndex1 { get; set; } = 1;
        public int SelectedIndex2 { get; set; } = 9;
        private int x;
        private int y;
        private int res;

        Random rnd = new Random();

        public bool IsStarted
        {
            get { return (bool)GetValue(IsStartedProperty); }
            set { SetValue(IsStartedProperty, value); }
        }
        public static readonly DependencyProperty IsStartedProperty =
            DependencyProperty.Register("IsStarted", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        public int Points
        {
            get { return (int)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        public int IncorrectAnswers
        {
            get { return (int)GetValue(IncorrectAnswersProperty); }
            set { SetValue(IncorrectAnswersProperty, value); }
        }
        public static readonly DependencyProperty IncorrectAnswersProperty =
            DependencyProperty.Register("IncorrectAnswers", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        public int CorrectAnswers
        {
            get { return (int)GetValue(CorrectAnswersProperty); }
            set { SetValue(CorrectAnswersProperty, value); }
        }
        public static readonly DependencyProperty CorrectAnswersProperty =
            DependencyProperty.Register("CorrectAnswers", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        public int Prompts
        {
            get { return (int)GetValue(PromptsProperty); }
            set { SetValue(PromptsProperty, value); }
        }
        public static readonly DependencyProperty PromptsProperty =
            DependencyProperty.Register("Prompts", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        public List<int> AnswerButtons
        {
            get { return (List<int>)GetValue(AnswerButtonsProperty); }
            set { SetValue(AnswerButtonsProperty, value); }
        }
        public static readonly DependencyProperty AnswerButtonsProperty =
            DependencyProperty.Register("AnswerButtons", typeof(List<int>), typeof(MainWindow), new PropertyMetadata(new List<int>()));

        public List<Style> ButtonStyles
        {
            get { return (List<Style>)GetValue(ButtonStylesProperty); }
            set { SetValue(ButtonStylesProperty, value); }
        }
        public static readonly DependencyProperty ButtonStylesProperty =
            DependencyProperty.Register("ButtonStyles", typeof(List<Style>), typeof(MainWindow), new PropertyMetadata(new List<Style>()));

        public Style StringStyle
        {
            get { return (Style)GetValue(StringStyleProperty); }
            set { SetValue(StringStyleProperty, value); }
        }
        public static readonly DependencyProperty StringStyleProperty =
            DependencyProperty.Register("StringStyle", typeof(Style), typeof(MainWindow), new PropertyMetadata(new Style()));

        public string DialogString
        {
            get { return (string)GetValue(DialogStringProperty); }
            set { SetValue(DialogStringProperty, value); }
        }
        public static readonly DependencyProperty DialogStringProperty =
            DependencyProperty.Register("DialogString", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public string ExerciseString
        {
            get { return (string)GetValue(ExerciseStringProperty); }
            set { SetValue(ExerciseStringProperty, value); }
        }
        public static readonly DependencyProperty ExerciseStringProperty =
            DependencyProperty.Register("ExerciseString", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public int NumberOfAnswers
        {
            get { return (int)GetValue(NumberOfAnswersProperty); }
            set { SetValue(NumberOfAnswersProperty, value); }
        }
        public static readonly DependencyProperty NumberOfAnswersProperty =
            DependencyProperty.Register("NumberOfAnswers", typeof(int), typeof(MainWindow), new PropertyMetadata(0));

        public int NumberOfQuestions
        {
            get { return (int)GetValue(NumberOfQuestionsProperty); }
            set { SetValue(NumberOfQuestionsProperty, value); }
        }
        public static readonly DependencyProperty NumberOfQuestionsProperty =
            DependencyProperty.Register("NumberOfQuestions", typeof(int), typeof(MainWindow), new PropertyMetadata(0));


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (IsStarted)
            {
                DialogString = "Вибери вірну відповідь:";
                Prompts = CorrectAnswers = IncorrectAnswers = Points = NumberOfAnswers = 0;
                NewExercise();
            }
            else
            {
                StringStyle = this.FindResource("ColorBlue") as Style;
                DialogString = "Обери складність і натисни «Старт»!";
            }
            //IsStarted = !IsStarted;
        }

        private void ButtonAnswer_Click(object sender, RoutedEventArgs e)
        {
            int answer = (int)(sender as Button).Content;
            if (answer == res)
            {
                //DialogString = $"Вірно! {x} × {y} = {res} \r\n Наступний приклад:";
                StringStyle = this.FindResource("ColorGreen") as Style;
                DialogString = $"Вірно! {x} × {y} = {res}";
                CorrectAnswers++;
                Points += 10;
                NewExercise();
            }
            else
            {
                StringStyle = this.FindResource("ColorRed") as Style;
                DialogString = $"Не вірно! Правильне рішення: {x} × {y} = {res}";
                IncorrectAnswers++;
                Points -= 10;
                NewExercise();
            }
            NumberOfAnswers++;
            IsCompleted();
        }

        private void ButtonPrompt_Click(object sender, RoutedEventArgs e)
        {
            StringStyle = this.FindResource("ColorBlue") as Style;
            DialogString = $"Запам'ятай вірну відповідь: {x} × {y} = {res}";
            Prompts++;
            Points -= 5;
            NewExercise();
            NumberOfAnswers++;
            IsCompleted();
        }

        private void NewExercise()
        {
            x = rnd.Next(1, 11);
            y = SelectedIndex1 == 1 ? rnd.Next(1, ItemSource[SelectedIndex2] + 1) : ItemSource[SelectedIndex2];
            res = x * y;
            
            if (rnd.Next(2) == 0)
                ExerciseString = $"{x} × {y} = ?";
            else
                ExerciseString = $"{y} × {x} = ?";
            
            List<int> tmp = new List<int>();
            while (tmp.Count < 5)
            {
                int num = rnd.Next(1, ItemSource[SelectedIndex2] * 10 + 1);
                if (!tmp.Contains(num)) tmp.Add(num);
            }

            if (!tmp.Contains(res))
                tmp[rnd.Next(0, tmp.Count)] = res;
            AnswerButtons = tmp;
            List<Style> tmpStyle = new List<Style>(ButtonStyles);
            tmpStyle.Shuffle();
            ButtonStyles = tmpStyle;
        }

        private void IsCompleted()
        {
            if (NumberOfAnswers == NumberOfQuestions)
            {
                DialogString += "\r\nОбери складність і натисни «Старт»!";
                IsStarted = false;
            }
        }
    }

    public static class CollectionsExtension
    {
        static public void Shuffle<T>(this List<T> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < list.Count - 1; ++i)
            {
                int k = rnd.Next(i, list.Count);
                T tmp = list[i];
                list[i] = list[k];
                list[k] = tmp;
            }
        }
    }

    public class NumberedTickBar : TickBar
    {
        protected override void OnRender(DrawingContext dc)
        {

            Size size = new Size(base.ActualWidth, base.ActualHeight);
            int tickCount = (int)((this.Maximum - this.Minimum) / this.TickFrequency) + 1;
            if ((this.Maximum - this.Minimum) % this.TickFrequency == 0)
                tickCount -= 1;
            Double tickFrequencySize;
            // Calculate tick's setting
            tickFrequencySize = (size.Width * this.TickFrequency / (this.Maximum - this.Minimum));
            string text = "";
            FormattedText formattedText = null;
            double num = this.Maximum - this.Minimum;
            int i = 0;
            // Draw each tick text
            for (i = 0; i <= tickCount; i++)
            {
                text = Convert.ToString(Convert.ToInt32(this.Minimum + this.TickFrequency * i), 10);

                formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Arial"), 8, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);
                dc.DrawText(formattedText, new Point((tickFrequencySize * i), 14));
            }
        }
    }
}
