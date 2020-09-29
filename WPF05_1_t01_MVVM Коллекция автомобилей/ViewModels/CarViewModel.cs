using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WPF05_1_t01.Models;
using WPF05_1_t01.Services;

namespace WPF05_1_t01.ViewModels
{
    class CarViewModel : DependencyObject
    {
        public CarViewModel()
        {
            FilterByName = "";
            ItemsCars = CollectionViewSource.GetDefaultView(cars);
            ItemsCars.Filter = FilterMethod;
            TempCarCopy = new Car();
            SaveCarCommand = new MyCommand(SaveCarExecute, SaveCarCanExecute);
            AddNewCarCommand = new MyCommand(AddNewCarExecute, AddNewCarCanExecute);
            DeleteSelectedItemsCommand = new MyCommand(DeleteSelectedItemsExecute, DeleteSelectedItemsCanExecute);
            SelectionChangedCommand = new MyCommand(SelectionChangedMethod, x => true);
        }


        public string FilterByName
        {
            get => ((string)GetValue(FilterByNameProperty)).ToLower();
            set => SetValue(FilterByNameProperty, value);
        }
        public static readonly DependencyProperty FilterByNameProperty =
            DependencyProperty.Register("FilterByName", typeof(string), typeof(CarViewModel), new PropertyMetadata(String.Empty, FilterChange));

        private static void FilterChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CarViewModel THIS)
            {
                THIS.ItemsCars.Filter = THIS.FilterMethod;
            }
        }


        public Car TempCar
        {
            get { return (Car)GetValue(TempCarProperty); }
            set { SetValue(TempCarProperty, value); }
        }
        public static readonly DependencyProperty TempCarProperty =
            DependencyProperty.Register("TempCar", typeof(Car), typeof(CarViewModel), new PropertyMetadata(new Car()));

        public Car TempCarCopy
        {
            get { return (Car)GetValue(TempCarCopyProperty); }
            set { SetValue(TempCarCopyProperty, value); }
        }
        public static readonly DependencyProperty TempCarCopyProperty =
            DependencyProperty.Register("TempCarCopy", typeof(Car), typeof(CarViewModel), new PropertyMetadata(new Car(), TempCarCopyChange));

        private static void TempCarCopyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                ((INotifyPropertyChanged)e.OldValue).PropertyChanged -=
                    (sender1, args1) => MainWindow_PropertyChanged(sender1, args1, d as CarViewModel);
            }
            if (e.NewValue != null)
            {
                ((INotifyPropertyChanged)e.NewValue).PropertyChanged +=
                    (sender1, args1) => MainWindow_PropertyChanged(sender1, args1, d as CarViewModel);
            }
        }

        static void MainWindow_PropertyChanged(object sender, PropertyChangedEventArgs e, CarViewModel wm)
        {
            wm.SaveCarCommand.RaiseCanExecuteChanged();
            wm.AddNewCarCommand.RaiseCanExecuteChanged();
        }

        public ICollectionView ItemsCars
        {
            get { return (ICollectionView)GetValue(ItemsCarsProperty); }
            set { SetValue(ItemsCarsProperty, value); }
        }
        public static readonly DependencyProperty ItemsCarsProperty =
            DependencyProperty.Register("ItemsCars", typeof(ICollectionView), typeof(CarViewModel), new PropertyMetadata(null));

        ObservableCollection<Car> cars = new ObservableCollection<Car> {
            new Car {Make = "Toyota", Model = "Camry", Engine = 2.8, ReleaseDate = new DateTime(2005, 11, 26), RegNumber = "AM2525CT", VINNumber = "JTEHT05J542053195" },
            new Car {Make = "Suzuki", Model = "Jimny", Engine = 1.4, ReleaseDate = new DateTime(2019, 10, 20), RegNumber = "AM5896CH", VINNumber = "JS3TE944274204707" },
            new Car {Make = "Ford", Model = "Kuga", Engine = 2.0, ReleaseDate = new DateTime(2017, 12, 28), RegNumber = "AA9510AK", VINNumber = "WF0VXXBDFV5R52017" },
            new Car {Make = "Renault", Model = "Duster", Engine = 1.6, ReleaseDate = new DateTime(2020, 06, 18), RegNumber = "AB3355BB", VINNumber = "4S4BP6102B7333733" },
            new Car {Make = "Mitsubishi", Model = "Carisma", Engine = 1.8, ReleaseDate = new DateTime(2010, 08, 25), RegNumber = "AM7894BP", VINNumber = "ML32A5HJ7HH015319" },
        };


        private bool SaveCarCanExecute(object obj)
        {
            if (cars.IndexOf(TempCar) < 0)
                return false;

            if (String.IsNullOrEmpty(TempCarCopy.Make) ||
                String.IsNullOrEmpty(TempCarCopy.Model) ||
                TempCarCopy.Engine == 0 ||
                String.IsNullOrEmpty(TempCarCopy.RegNumber) ||
                String.IsNullOrEmpty(TempCarCopy.VINNumber))
                return false;

            if (cars.Any(i => !i.Equals(TempCar) && i.RegNumber == TempCarCopy.RegNumber))
                return false;
            if (cars.Any(i => !i.Equals(TempCar) && i.VINNumber == TempCarCopy.VINNumber))
                return false;

            return !TempCarCopy.Equals(TempCar);
        }

        private void SaveCarExecute(object obj)
        {
            int index = cars.IndexOf(TempCar);
            cars[index] = (Car)TempCarCopy.Clone();
            TempCar = cars[index];
        }


        private bool AddNewCarCanExecute(object obj)
        {
            if (TempCarCopy == null)
            {
                TempCarCopy = new Car();
                return false;
            }

            if (String.IsNullOrEmpty(TempCarCopy.Make) ||
                String.IsNullOrEmpty(TempCarCopy.Model) ||
                TempCarCopy.Engine == 0 ||
                String.IsNullOrEmpty(TempCarCopy.RegNumber) ||
                String.IsNullOrEmpty(TempCarCopy.VINNumber))
                return false;

            if (cars.Any(i => i.RegNumber == TempCarCopy.RegNumber) ||
                cars.Any(i => i.VINNumber == TempCarCopy.VINNumber))
                return false;

            return true;
        }

        private void AddNewCarExecute(object obj)
        {
            cars.Add((Car)TempCarCopy.Clone());
            int index = cars.IndexOf(TempCarCopy);
            TempCar = cars[index];
        }


        private bool DeleteSelectedItemsCanExecute(object SelectedItems)
        {
            return (SelectedItems as ObservableCollection<object>)?.Count > 0;
        }

        private void DeleteSelectedItemsExecute(object SelectedItems)
        {
            int index = cars.IndexOf(TempCar);
            var itemList = (SelectedItems as ObservableCollection<object>).Cast<Car>().ToList();
            itemList.ForEach(i => cars.Remove(i));
            if (index < cars.Count)
                TempCar = cars[index];
            else if (cars.Count != 0)
                TempCar = cars[cars.Count - 1];
        }


        private void SelectionChangedMethod(object obj)
        {
            DeleteSelectedItemsCommand.RaiseCanExecuteChanged();
            TempCarCopy = (Car)TempCar?.Clone();
            SaveCarCommand.RaiseCanExecuteChanged();
            AddNewCarCommand.RaiseCanExecuteChanged();
        }

        private bool FilterMethod(object obj)
        {
            if (obj is Car car)
            {
                return car.Make.ToLower().Contains(FilterByName) ||
                       car.Model.ToLower().Contains(FilterByName) ||
                       car.Engine.ToString("0.0#", CultureInfo.CreateSpecificCulture("uk-UA")).Contains(FilterByName) ||
                       car.ReleaseDate.ToShortDateString().Contains(FilterByName) ||
                       car.RegNumber.ToLower().Contains(FilterByName) ||
                       car.VINNumber.ToLower().Contains(FilterByName);
            }
            return false;
        }

        //Commands
        public MyCommand SaveCarCommand { get; private set; }
        public MyCommand AddNewCarCommand { get; private set; }
        public MyCommand DeleteSelectedItemsCommand { get; private set; }
        public ICommand SelectionChangedCommand { get; private set; }
    }
}
