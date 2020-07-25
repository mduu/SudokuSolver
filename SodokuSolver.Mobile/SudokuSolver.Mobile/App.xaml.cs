using System;
using SudokuSolver.Core.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SudokuSolver.Mobile.Services;
using SudokuSolver.Mobile.Views;

namespace SudokuSolver.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();

            // DependencyService.Register<IFeedback, BlazorFeedback>();
            // DependencyService.Register<IBoardPrinter, BlazorBoardPrinter>();
            DependencyService.Register<ISolver, Solver>();
            DependencyService.Register<ISolveStrategy, ExcludeStrategy>();
            DependencyService.Register<BoardFactory>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
