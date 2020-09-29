using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF05_1_t01.Models
{
    class Car : ICloneable, INotifyPropertyChanged
    {
        public Car()
        {
            ReleaseDate = DateTime.Now;
        }

        private string _make;
        public string Make
        {
            get { return _make; }
            set
            {
                _make = value;
                OnPropertyChanged();
            }
        }

        private string _model;
        public string Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        private double _engine;
        public double Engine
        {
            get { return _engine; }
            set
            {
                _engine = value;
                OnPropertyChanged();
            }
        }

        private DateTime _releaseDate;
        public DateTime ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                _releaseDate = value;
                OnPropertyChanged();
            }
        }

        private string _regNumber;
        public string RegNumber
        {
            get { return _regNumber; }
            set
            {
                _regNumber = value;
                OnPropertyChanged();
            }
        }

        private string _vinNumber;
        public string VINNumber
        {
            get { return _vinNumber; }
            set
            {
                _vinNumber = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override bool Equals(object obj)
        {
            if ((object)this == obj)
                return true;

            var other = obj as Car;

            if ((object)other == null)
                return false;

            return EqualsHelper(this, other);
        }

        protected static bool EqualsHelper(Car first, Car second) =>
            first.Make == second.Make &&
            first.Model == second.Model &&
            first.Engine == second.Engine &&
            first.ReleaseDate.Date == second.ReleaseDate.Date &&
            first.RegNumber == second.RegNumber &&
            first.VINNumber == second.VINNumber;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
