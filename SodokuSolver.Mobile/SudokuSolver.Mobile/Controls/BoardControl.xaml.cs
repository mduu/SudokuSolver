using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver.Core.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SudokuSolver.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardControl : Grid
    {
        public BoardControl()
        {
            BindingContext = this;
            InitializeComponent();
        }
        
        public static readonly BindableProperty BoardProperty = BindableProperty.Create(nameof(Board), typeof(Board), typeof(BoardControl));
        public Board Board
        {
            get => (Board) GetValue(BoardProperty);
            set => SetValue(BoardProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == nameof(Board))
            {
                RebuildBoard();
            }
            
            base.OnPropertyChanged(propertyName);
        }

        private void RebuildBoard()
        {
            Children.Clear();
            ColumnDefinitions.Clear();
            RowDefinitions.Clear();

            for (var row = 0; row < Board.NumberOfSections; row++)
            {
                RowDefinitions.Add(new RowDefinition());
                ColumnDefinitions.Add(new ColumnDefinition());

                for (var col = 0; col < Board.NumberOfSections; col++)
                {
                    var section = new SectionControl
                    {
                        Section = Board.Sections[col, row]
                    };
                    
                    Children.Add(section);
                    SetColumn(section, col);
                    SetRow(section, row);
                }
                
            }
        }
    }
}