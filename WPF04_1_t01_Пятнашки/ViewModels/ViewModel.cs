using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WPF04_1_t01.Models;
using WPF04_1_t01.Services;

namespace WPF04_1_t01.ViewModels
{
    class ViewModel : DependencyObject
    {
        private Game game;
        private ObservableCollection<Piece> puzzle;

        //Commands
        public ICommand Click { get; private set; }
        
        public ViewModel()
        {
            game = new Game();
            puzzle = new ObservableCollection<Piece>(game.turn(0));
            ItemsPuzzle = CollectionViewSource.GetDefaultView(puzzle);
            Click = new MyCommand(buttonClick, x=>true);
        }

        public ICollectionView ItemsPuzzle
        {
            get { return (ICollectionView)GetValue(ItemsPuzzleProperty); }
            set { SetValue(ItemsPuzzleProperty, value); }
        }
        public static readonly DependencyProperty ItemsPuzzleProperty =
            DependencyProperty.Register("ItemsPuzzle", typeof(ICollectionView), typeof(ViewModel), new PropertyMetadata(null));

        private void buttonClick(object obj)
        {
            puzzle.Clear();
            game.turn((int)obj).ForEach(puzzle.Add);
        }
    }
}
