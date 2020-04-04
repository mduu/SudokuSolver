using SimpleInjector;
using SudokuSolver.Core.Domain;
using SudokuSolver.Core.Infrastructure;

namespace SudokuSolverCLI.Infra
{
    internal static class ContainerFactory
    {
        internal static Container Create()
        {
            var container = new Container();
            
            container.Register<IFeedback, ConsoleFeedback>();
            container.Register<IBoardPrinter, ConsoleBoardPrinter>();
            container.Register<ISolver, Solver>();
            container.Register<ISolveStrategy, ExcludeStrategy>();

            return container;
        }
    }
}