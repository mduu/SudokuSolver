using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SudokuSolver.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardPage : ContentPage
    {
        private BoardViewModel viewModel;
        
        public BoardPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BoardViewModel();
        }

        private void Solve_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}