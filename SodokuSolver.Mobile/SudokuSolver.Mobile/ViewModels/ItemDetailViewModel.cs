using System;

using SudokuSolver.Mobile.Models;

namespace SudokuSolver.Mobile.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item
        {
            get; set;
        }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
