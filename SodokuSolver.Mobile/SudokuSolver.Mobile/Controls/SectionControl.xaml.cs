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
    // ReSharper disable once UnusedType.Global
    public partial class SectionControl : Grid
    {
        public SectionControl()
        {
            BindingContext = this;
            InitializeComponent();
            Test = "Hi";
        }
        
        public static readonly BindableProperty SectionProperty = BindableProperty.Create(nameof(Section), typeof(Board.Section), typeof(SectionControl));
        public Board.Section Section
        {
            get => (Board.Section) GetValue(SectionProperty);
            set
            {
                SetValue(SectionProperty, value);
                SetValue(SectionNameProperty, $"Section {value.X}/{value.Y}");
            }
        }

        public static readonly BindableProperty SectionNameProperty = BindableProperty.Create(nameof(SectionName), typeof(string), typeof(SectionControl));
        public string SectionName
        {
            get => (string) GetValue(SectionNameProperty);
            set => SetValue(SectionNameProperty, value);
        }
        
        public static readonly BindableProperty TestProperty = BindableProperty.Create(nameof(Test), typeof(string), typeof(SectionControl));
        public string Test
        {
            get => (string) GetValue(SectionNameProperty);
            set => SetValue(SectionNameProperty, value);
        }
    }
}